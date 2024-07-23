using System.Runtime.InteropServices;
using System.Text;

namespace HAcomms.Tools;

[StructLayout(LayoutKind.Sequential)]
public struct Rect {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}

public static class WindowsTools {
    [DllImport("user32.dll")] private static extern IntPtr GetDesktopWindow();
    [DllImport("user32.dll")] private static extern IntPtr GetShellWindow();
    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
    [DllImport("user32.dll")] private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll")] private static extern int GetWindowTextLength(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowRect(IntPtr hwnd, out Rect rc);

    private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);


    public static IDictionary<IntPtr, string> GetOpenWindows() {
        IntPtr shellWindow = GetShellWindow();
        var windows = new Dictionary<IntPtr, string>();

        EnumWindows(delegate(IntPtr hWnd, int lParam) {
            if (hWnd == shellWindow)
                return true;
            if (!IsWindowVisible(hWnd))
                return true;

            int length = GetWindowTextLength(hWnd);
            if (length == 0)
                return true;

            var builder = new StringBuilder(length);
            GetWindowText(hWnd, builder, length + 1);

            windows[hWnd] = builder.ToString();
            return true;
        }, 0);

        return windows;
    }

    public static bool IsFullScreen(IntPtr hWnd) {
        if (hWnd.Equals(IntPtr.Zero)) {
            return false;
        }

        if (!IsWindowVisible(hWnd)) {
            return false;
        }

        //Detect if the current app is running in full screen
        bool runningFullScreen = false;
        IntPtr desktopWindow = GetDesktopWindow();
        IntPtr shellWindow = GetShellWindow();

        if (hWnd.Equals(desktopWindow) || hWnd.Equals(shellWindow)) {
            return false;
        }

        //Check we haven't picked up the desktop or the shell
        GetWindowRect(hWnd, out var appBounds);
        //determine if window is fullscreen
        var screenBounds = Screen.FromHandle(hWnd).Bounds;
        if ((appBounds.Bottom - appBounds.Top) == screenBounds.Height && (appBounds.Right - appBounds.Left) == screenBounds.Width) {
            runningFullScreen = true;
        }

        return runningFullScreen;
    }

    public static bool AnyTitleMatches(WatchedEntity we, IDictionary<IntPtr, string>? windows = null) {
        windows = windows ?? GetOpenWindows();
        return windows.Any(kvp => we.Matches(kvp.Value));
    }
}