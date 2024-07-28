using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

#pragma warning disable CS8601 // Possible null reference assignment.

namespace HAcomms.Tools;

class GlobalKeyboardHookEventArgs : HandledEventArgs {
    public GlobalKeyboardHook.KeyboardState KeyboardState { get; private set; }
    public GlobalKeyboardHook.LowLevelKeyboardInputEvent KeyboardData { get; private set; }

    public GlobalKeyboardHookEventArgs(
        GlobalKeyboardHook.LowLevelKeyboardInputEvent keyboardData,
        GlobalKeyboardHook.KeyboardState keyboardState
    ) {
        KeyboardData = keyboardData;
        KeyboardState = keyboardState;
    }
}

class GlobalKeyboardKeyCombinationEventArgs(string comboId) : HandledEventArgs {
    public string ComboId { get; private set; } = comboId;
}

//Based on https://gist.github.com/Stasonix
class GlobalKeyboardHook : IDisposable {
    public event EventHandler<GlobalKeyboardHookEventArgs>? KeyboardPressed;
    public event EventHandler<GlobalKeyboardKeyCombinationEventArgs>? KeyboardComboPressed;
    private IntPtr _windowsHookHandle;
    private IntPtr _user32LibraryHandle;
    private HookProc _hookProc;
    private readonly KeyCombination _pressedKeys;
    private readonly Dictionary<string, KeyCombination> _registeredKeyCombos;
    private Keys? _lastPressedKey;

    private readonly Keys[]? _registeredKeys;
    private const int WH_KEYBOARD_LL = 13;

    public enum KeyboardState {
        KeyDown = 0x0100,
        KeyUp = 0x0101,
        SysKeyDown = 0x0104,
        SysKeyUp = 0x0105
    }

    public static bool IsKeyCtrl(Keys key) { return key is Keys.RControlKey or Keys.LControlKey; }

    public static bool IsKeyAlt(Keys key) { return key is Keys.LMenu or Keys.Alt; }

    // EDT: Added an optional parameter (registeredKeys) that accepts keys to restict
    // the logging mechanism.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registeredKeys">Keys that should trigger logging. Pass null for full logging.</param>
    public GlobalKeyboardHook(Keys[]? registeredKeys = null) {
        _registeredKeys = registeredKeys?.Clone() as Keys[];
        _windowsHookHandle = IntPtr.Zero;
        _user32LibraryHandle = IntPtr.Zero;
        _hookProc = LowLevelKeyboardProc; // we must keep alive _hookProc, because GC is not aware about SetWindowsHookEx behaviour.
        _pressedKeys = new KeyCombination("", []);
        _registeredKeyCombos = new Dictionary<string, KeyCombination>();

        _user32LibraryHandle = LoadLibrary("User32");
        if (_user32LibraryHandle == IntPtr.Zero) {
            int errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode,
                $"Failed to load library 'User32.dll'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
        }

        _windowsHookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, _user32LibraryHandle, 0);
        if (_windowsHookHandle == IntPtr.Zero) {
            int errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode,
                $"Failed to adjust keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
        }
    }

    public KeyCombination CurrentKeyCombo => _pressedKeys.Clone();

    public KeyCombination[] RegisteredKeyCombos => _registeredKeyCombos.Values.ToArray();

    public bool AddKeyCombo(string id, IEnumerable<Keys> keys, bool withAlt = false, bool withCtrl = false) {
        if (_registeredKeyCombos.ContainsKey(id)) {
            return false;
        }

        var combo = new KeyCombination(id, keys, withAlt, withCtrl);
        _registeredKeyCombos.Add(id, combo);
        return true;
    }

    public bool AddKeyCombo(string id, KeyCombination keyCombo) { return AddKeyCombo(id, keyCombo.Keys, keyCombo.WithAlt, keyCombo.WithCtrl); }

    public bool RemoveKeyCombo(string id) {
        if (!_registeredKeyCombos.ContainsKey(id)) {
            return false;
        }

        _registeredKeyCombos.Remove(id);
        return true;
    }

    public void SetKeyCombos(IEnumerable<KeyCombination> combos) {
        _registeredKeyCombos.Clear();
        foreach (var combo in combos) {
            _registeredKeyCombos.TryAdd(combo.Id, combo);
        }
    }

    protected virtual void Dispose(bool disposing) {
        if (disposing) {
            // because we can unhook only in the same thread, not in garbage collector thread
            if (_windowsHookHandle != IntPtr.Zero) {
                if (!UnhookWindowsHookEx(_windowsHookHandle)) {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode,
                        $"Failed to remove keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
                }

                _windowsHookHandle = IntPtr.Zero;

                // ReSharper disable once DelegateSubtraction
                _hookProc -= LowLevelKeyboardProc;
            }
        }

        if (_user32LibraryHandle != IntPtr.Zero) {
            if (!FreeLibrary(_user32LibraryHandle)) // reduces reference to library by 1.
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode,
                    $"Failed to unload library 'User32.dll'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
            }

            _user32LibraryHandle = IntPtr.Zero;
        }
    }

    ~GlobalKeyboardHook() { Dispose(false); }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);


    #region external

    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern bool FreeLibrary(IntPtr hModule);

    /// <summary>
    /// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
    /// You would install a hook procedure to monitor the system for certain types of events. These events are
    /// associated either with a specific thread or with all threads in the same desktop as the calling thread.
    /// </summary>
    /// <param name="idHook">hook type</param>
    /// <param name="lpfn">hook procedure</param>
    /// <param name="hMod">handle to application instance</param>
    /// <param name="dwThreadId">thread identifier</param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure.</returns>
    [DllImport("USER32", SetLastError = true)]
    static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

    /// <summary>
    /// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
    /// </summary>
    /// <param name="hHook">handle to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("USER32", SetLastError = true)]
    public static extern bool UnhookWindowsHookEx(IntPtr hHook);

    /// <summary>
    /// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
    /// A hook procedure can call this function either before or after processing the hook information.
    /// </summary>
    /// <param name="hHook">handle to current hook</param>
    /// <param name="code">hook code passed to hook procedure</param>
    /// <param name="wParam">value passed to hook procedure</param>
    /// <param name="lParam">value passed to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("USER32", SetLastError = true)]
    static extern IntPtr CallNextHookEx(IntPtr hHook, int code, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct LowLevelKeyboardInputEvent {
        /// <summary>
        /// A virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        public int VirtualCode;

        // EDT: added a conversion from VirtualCode to Keys.
        /// <summary>
        /// The VirtualCode converted to typeof(Keys) for higher usability.
        /// </summary>
        public Keys Key => (Keys)VirtualCode;

        /// <summary>
        /// A hardware scan code for the key. 
        /// </summary>
        public int HardwareScanCode;

        /// <summary>
        /// The extended-key flag, event-injected Flags, context code, and transition-state flag. This member is specified as follows. An application can use the following values to test the keystroke Flags. Testing LLKHF_INJECTED (bit 4) will tell you whether the event was injected. If it was, then testing LLKHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process running at lower integrity level.
        /// </summary>
        public int Flags;

        /// <summary>
        /// The time stamp stamp for this message, equivalent to what GetMessageTime would return for this message.
        /// </summary>
        public int TimeStamp;

        /// <summary>
        /// Additional information associated with the message. 
        /// </summary>
        public IntPtr AdditionalInformation;
    }

    #endregion


    private IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam) {
        bool eatKeyStroke = false;

        if (nCode < 0) {
            goto QuickExit;
        }

        int wparamTyped = wParam.ToInt32();
        if (!Enum.IsDefined(typeof(KeyboardState), wparamTyped)) {
            goto QuickExit;
        }

        object? o = Marshal.PtrToStructure(lParam, typeof(LowLevelKeyboardInputEvent));
        var keyboardData = (LowLevelKeyboardInputEvent)o!;
        var keyboardState = (KeyboardState)wparamTyped;
        var key = keyboardData.Key;

        switch (keyboardState) {
            case KeyboardState.KeyDown:
            case KeyboardState.SysKeyDown:
                if (key == _lastPressedKey) {
                    goto QuickExit;
                }

                _lastPressedKey = key;

                switch (key) {
                    case Keys.Alt or Keys.LMenu:
                        _pressedKeys.WithAlt = true;
                        break;
                    case Keys.LControlKey or Keys.RControlKey:
                        _pressedKeys.WithCtrl = true;
                        break;
                    default:
                        _pressedKeys.Add(key);
                        break;
                }

                break;

            case KeyboardState.KeyUp:
            case KeyboardState.SysKeyUp:
                _lastPressedKey = null;

                switch (key) {
                    case Keys.Alt or Keys.LMenu:
                        _pressedKeys.WithAlt = false;
                        break;
                    case Keys.LControlKey or Keys.RControlKey:
                        _pressedKeys.WithCtrl = false;
                        break;
                    default:
                        _pressedKeys.Remove(key);
                        break;
                }

                break;

            default:
                goto QuickExit;
        }

        // handle and construct keyboard event
        var keyboardPressedEventArgs = new GlobalKeyboardHookEventArgs(keyboardData, keyboardState);
        if (_registeredKeys == null || _registeredKeys.Contains(key)) {
            var handler = KeyboardPressed;
            handler?.Invoke(this, keyboardPressedEventArgs);
            eatKeyStroke = keyboardPressedEventArgs.Handled;
        }

        // handle and construct keyboard combo event
        if (!eatKeyStroke) {
            foreach (var kvp in _registeredKeyCombos) {
                if (kvp.Value.Equals(_pressedKeys)) {
                    var keyboardComboEventArgs = new GlobalKeyboardKeyCombinationEventArgs(kvp.Key);
                    var handler = KeyboardComboPressed;
                    handler?.Invoke(this, keyboardComboEventArgs);
                    eatKeyStroke |= keyboardComboEventArgs.Handled;
                }
            }
        }

        QuickExit:
        return eatKeyStroke
            ? 1
            : CallNextHookEx(_windowsHookHandle, nCode, wParam, lParam);
    }

    public bool IsAltDown => _pressedKeys.WithAlt;

    public bool IsCtrlDown => _pressedKeys.WithCtrl;
}