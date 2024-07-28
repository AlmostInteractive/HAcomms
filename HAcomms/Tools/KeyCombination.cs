using System.Text;

namespace HAcomms.Tools;

public class KeyCombination : IEquatable<KeyCombination> {
    private readonly HashSet<Keys> _keys;


    public KeyCombination(string id, IEnumerable<Keys> keys, bool withAlt = false, bool withCtrl = false) {
        Id = id;
        _keys = keys.ToHashSet();
        WithAlt = withAlt;
        WithCtrl = withCtrl;
    }

    public KeyCombination Clone() { return new KeyCombination(Id, _keys, WithAlt, WithCtrl); }

    public void Add(Keys key) { _keys.Add(key); }

    public void Remove(Keys key) { _keys.Remove(key); }

    public void Clear() { _keys.Clear(); }

    public string Id { get; internal set; }

    public IEnumerable<Keys> Keys => _keys;

    public bool WithAlt { get; set; }

    public bool WithCtrl { get; set; }

    public int Count => _keys.Count;

    public bool Contains(Keys key) { return _keys.Contains(key); }

    public bool Equals(KeyCombination? other) { return other != null && WithAlt == other.WithAlt && WithCtrl == other.WithCtrl && KeysEqual(other._keys); }

    public override bool Equals(object? obj) {
        if (obj is KeyCombination combination) {
            return Equals(combination);
        }

        return false;
    }

    private bool KeysEqual(IReadOnlySet<Keys> keys) { return keys.SetEquals(_keys); }

    public override int GetHashCode() {
        //http://stackoverflow.com/a/263416
        //http://stackoverflow.com/a/8094931
        //assume keys not going to modify after we use GetHashCode
        unchecked {
            int hash = 19;
            foreach (var key in _keys) {
                hash = hash * 31 + key.GetHashCode();
            }

            return hash;
        }
    }

    public override string ToString() {
        var sb = new StringBuilder((_keys.Count - 1) * 4 + 10);
        bool first = true;

        if (WithCtrl) {
            sb.Append("CTRL");
            first = false;
        }

        if (WithAlt) {
            sb.Append((first
                ? ""
                : " + ") + "ALT");
            first = false;
        }

        foreach (var key in _keys) {
            sb.Append((first
                ? ""
                : " + ") + key);
            first = false;
        }

        return sb.ToString();
    }
}