using System.Text.RegularExpressions;

namespace HAcomms.Tools;

public class WatchedEntity {
    public bool IsTab { get; init; }
    public bool IsRegex { get; init; }
    public string Entry { get; init; } = "";

    private Regex? _regex;

    public bool Matches(string title) {
        if (!IsRegex) {
            return Entry == title;
        }
        
        _regex = _regex ?? new Regex($"/{Entry}/");
        return _regex.IsMatch(title);
    }
}