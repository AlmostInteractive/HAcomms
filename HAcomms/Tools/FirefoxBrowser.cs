namespace HAcomms.Tools;

internal abstract class FirefoxBrowser : IBrowser {
    public static string ProcessName => "firefox";
    public static string WindowClassName => "MozillaWindowClass";
}