using HAcomms.Forms;

namespace HAcomms;

static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args) {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.

        bool minimized = (args.Length > 0 && args[0] == "-minimized");
        
        ApplicationConfiguration.Initialize();
        Application.Run(new Main(minimized));
    }
}