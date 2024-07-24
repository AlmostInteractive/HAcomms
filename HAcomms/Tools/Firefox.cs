using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;

namespace HAcomms.Tools;

internal static class Firefox {
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    private static Dictionary<IntPtr, List<string>> _cache = new();
    
    
    public static List<string> GetAllTabTitles(IEnumerable<IntPtr> hWnds) {
        var tabTitles = new List<string>();
        foreach (IntPtr hWnd in hWnds) {
            GetWindowTabTitles(hWnd, tabTitles);
        }

        return tabTitles;
    }

    private static List<string> GetWindowTabTitles(IntPtr hWnd, List<string>? tabTitles = null) {
        List<string> titles = [];
        if (!IsWindowVisible(hWnd)) {
            return titles;
        }

        var sClassName = new StringBuilder(256);
        GetWindowThreadProcessId(hWnd, out uint processId);
        var processFromId = Process.GetProcessById((int)processId);
        GetClassName(hWnd, sClassName, sClassName.Capacity);

        //Only want visible chrome windows (not any electron type apps that have chrome embedded!)
        if (((sClassName.ToString() != "Chrome_WidgetWin_1") || (processFromId.ProcessName != "chrome"))) {
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
            tabTitles.Add(tab.Current.Name);
        }
    }
}