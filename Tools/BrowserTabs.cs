using System.Text.RegularExpressions;

namespace HAcomms.BrowserTools;

public partial class BrowserTabs {
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
}