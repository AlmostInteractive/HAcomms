using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using HAcomms.Tools;

namespace HAcomms.BrowserTools;

public partial class BrowserTabs {
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    private static readonly Dictionary<IntPtr, List<string>> _windowTabTitlesCache = new();
    
    public static bool CheckTabsForMeetings(List<string> tabs) {
        var googleMeetRegex = ActiveGoogleMeetRegex();
        foreach (string tab in tabs) {
            if (googleMeetRegex.IsMatch(tab))
                return true;
        }

        return false;
    }

    [GeneratedRegex("^Meet - [a-z]{3}-[a-z]{4}-[a-z]{3}.*")]
    private static partial Regex ActiveGoogleMeetRegex();
    
    
    
    
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

        GetTabTitles(hWnd, titles);
        tabTitles?.AddRange(titles);

        return titles;
    }

    private static void GetTabTitles(IntPtr hWnd, List<string> tabTitles) {
        var tree = TreeWalker.ControlViewWalker;
        var rootElement = AutomationElement.FromHandle(hWnd);
        if (rootElement == null) {
            return;
        }
        
        Condition condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
        var firstTab = rootElement.FindFirst(TreeScope.Descendants, condition);
        if (firstTab == null) {
            return;
        }

        var parent = tree.GetParent(firstTab);
        if (parent == null) {
            return;
        }

        var tabs = parent.FindAll(TreeScope.Children, condition);
        if (tabs == null) {
            return;
        }

        foreach (AutomationElement tab in tabs) {
            string[] pieces = tab.Current.Name.Split(" - ");
            pieces = pieces.Take(pieces.Length - 2).ToArray();
            string tabName = string.Join(" - ", pieces);
            tabTitles.Add(tabName);
        }
    }
}