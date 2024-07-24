namespace HAcomms.Tools;

public interface IBrowser {
    static abstract string ProcessName { get; }
    static abstract string WindowClassName { get; }
    static abstract string FormatTabTitle(string tab);
}