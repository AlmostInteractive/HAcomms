using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;

namespace HAcomms.Tools;

public static partial class BrowserTabs {
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
    
    private static readonly Dictionary<IntPtr, List<string>> _windowTabTitlesCache = new();
    private static readonly Dictionary<IntPtr, AutomationElement> _windowParentElementCache = new();
    private static readonly Condition _findTabCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
    
    
    public static List<string> GetAllTabTitles<T>(IEnumerable<IntPtr> hWnds) where T : IBrowser {
        var tabTitles = new List<string>();
        foreach (IntPtr hWnd in hWnds) {
            GetWindowTabTitles<T>(hWnd, tabTitles);
        }

        return tabTitles;
    }
    
    public static bool MatchesWatchedEntity<T>(IntPtr hWnd, WatchedEntity we) where T : IBrowser {
        _windowTabTitlesCache.TryGetValue(hWnd, out var titles);
        if (titles != null) {
            return titles.Any(we.Matches);
        }

        titles = GetWindowTabTitles<T>(hWnd);
        _windowTabTitlesCache.Add(hWnd, titles);
        return titles.Any(we.Matches);
    }

    public static void ResetCache() { _windowTabTitlesCache.Clear(); }

    private static List<string> GetWindowTabTitles<T>(IntPtr hWnd, List<string>? tabTitles = null) where T : IBrowser {
        List<string> titles = [];
        if (!IsWindowVisible(hWnd)) {
            return titles;
        }

        var sClassName = new StringBuilder(256);
        GetWindowThreadProcessId(hWnd, out uint processId);
        var processFromId = Process.GetProcessById((int)processId);
        GetClassName(hWnd, sClassName, sClassName.Capacity);

        //Only want visible browser windows (not any electron type apps that have a browser embedded!)
        if (((sClassName.ToString() != T.WindowClassName) || (processFromId.ProcessName != T.ProcessName))) {
            return titles;
        }

        GetTabTitles<T>(hWnd, titles);
        tabTitles?.AddRange(titles);

        return titles;
    }

    private static void GetTabTitles<T>(IntPtr hWnd, List<string> tabTitles) where T : IBrowser {
        _windowParentElementCache.TryGetValue(hWnd, out var parent);
        if (parent == null) {
            var tree = TreeWalker.ControlViewWalker;
            var rootElement = AutomationElement.FromHandle(hWnd);
            if (rootElement == null) {
                return;
            }

            var firstTab = rootElement.FindFirst(TreeScope.Descendants, _findTabCondition);
            if (firstTab == null) {
                return;
            }

            parent = tree.GetParent(firstTab);
            if (parent != null) {
                _windowParentElementCache.TryAdd(hWnd, parent);
            }
        }

        if (parent == null) {
            return;
        }

        var tabs = parent.FindAll(TreeScope.Children, _findTabCondition);
        if (tabs == null) {
            return;
        }

        foreach (AutomationElement tab in tabs) {
            tabTitles.Add(T.FormatTabTitle(tab.Current.Name));
        }
    }
}