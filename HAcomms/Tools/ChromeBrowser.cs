namespace HAcomms.Tools;

internal abstract class ChromeBrowser : IBrowser {
    public static string ProcessName => "chrome";
    public static string WindowClassName => "Chrome_WidgetWin_1";

    public static string FormatTabTitle(string tab) {
        string[] pieces = tab.Split(" - ");
        pieces = pieces.Take(pieces.Length - 2).ToArray();
        return string.Join(" - ", pieces);
    }
}