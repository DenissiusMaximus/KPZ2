namespace LightHTML.Core;

public sealed record ElementFlyweight(
    string TagName,
    DisplayType Display,
    ClosingType Closing,
    IReadOnlyList<string> Classes)
{
    public string CacheKey { get; } =
        $"{TagName}|{Display}|{Closing}|{string.Join(',', Classes)}";
}

public sealed class FlyweightFactory
{
    private readonly Dictionary<string, ElementFlyweight> _cache = new(StringComparer.Ordinal);

    public ElementFlyweight Get(
        string tagName,
        DisplayType display = DisplayType.Block,
        ClosingType closing = ClosingType.Closed,
        IEnumerable<string>? classes = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tagName);

        var classList = classes?.ToArray() ?? [];
        var key = $"{tagName}|{display}|{closing}|{string.Join(',', classList)}";

        if (_cache.TryGetValue(key, out var cached))
            return cached;

        var fw = new ElementFlyweight(tagName, display, closing, classList);
        _cache[key] = fw;
        return fw;
    }

    public int CacheSize => _cache.Count;
}
