public class LRUCache
{
    private readonly Dictionary<string, CacheEntry> _store = new Dictionary<string, CacheEntry>();

    private CacheEntry _first;
    private CacheEntry _last;

    public void Add(string key, string value)
    {
        var exist = Get(key);

        if (exist == null)
        {
            var entry = new CacheEntry(key, value);

            this._store.Add(key, entry);
            AppendToLast(entry);
        }
        else
        {
            exist.Value = value;
        }
    }

    public CacheEntry Get(string key)
    {
        if (_store.ContainsKey(key))
        {
            MoveToLast(_store[key]);

            return _store[key];
        }

        return null;
    }

    private void MoveToLast(CacheEntry entry)
    {
        entry.Previous = entry.Next;

        _last.Next = entry;
        entry.Previous = _last;
        entry.Next = null;
    }

    private void AppendToLast(CacheEntry entry)
    {
        if (_first == null && _last == null)
        {
            _first = _last;
            _last = entry;
        }
        else
        {
            _last.Next = entry;
            entry.Previous = _last;

            _last = _last.Next;
        }
    }
}

public class CacheEntry
{
    public string Key { get; }
    public string Value { get; set; }

    public CacheEntry Next { get; set; }
    public CacheEntry Previous { get; set; }

    public CacheEntry(string key, string Value)
    {
        this.Key = key;
        this.Value = Value;
    }
}
