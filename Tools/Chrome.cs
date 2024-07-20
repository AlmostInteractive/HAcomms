using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;

namespace HAcomms.Tools;

class Chrome {
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true)] static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)] static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    
    public static List<string> GetAllTabTitles(IEnumerable<IntPtr> hWnds) {
        var tabTitles = new List<string>();
        foreach (IntPtr hWnd in hWnds) {
            GetWindowTabs(hWnd, tabTitles);
        }

        return tabTitles;
    }
    
    private static void GetWindowTabs(IntPtr hWnd, List<string> tabTitles) {
        if (!IsWindowVisible(hWnd)) {
            return;
        }

        var sClassName = new StringBuilder(256);
        GetWindowThreadProcessId(hWnd, out uint processId);
        var processFromId = Process.GetProcessById((int)processId);
        GetClassName(hWnd, sClassName, sClassName.Capacity);

        //Only want visible chrome windows (not any electron type apps that have chrome embedded!)
        if (((sClassName.ToString() == "Chrome_WidgetWin_1") && (processFromId.ProcessName == "chrome"))) {
            FindChromeTabs(hWnd, tabTitles);
        }
    }

    private static void FindChromeTabs(IntPtr hWnd, List<string> tabTitles) {
        var tree = TreeWalker.ControlViewWalker;
        var rootElement = AutomationElement.FromHandle(hWnd);
        Condition condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
        var firstTab = rootElement.FindFirst(TreeScope.Descendants, condition);
        var parent = tree.GetParent(firstTab);
        var tabs = parent.FindAll(TreeScope.Children, condition);
        
        foreach (AutomationElement tab in tabs) {
            string[] pieces = tab.Current.Name.Split(" - ");
            pieces = pieces.Take(pieces.Length - 2).ToArray();
            string tabName = string.Join(" - ", pieces);
            tabTitles.Add(tabName);
        }
    }
}