namespace LightHTML.Core;

// ── Strategy: Image Loading ───────────────────────────────────────────────────

public interface IImageLoader
{
    string Load(string src);
}

public sealed class FileImageLoader : IImageLoader
{
    public string Load(string src) => $"[file] Loaded: {src}";
}

public sealed class NetworkImageLoader : IImageLoader
{
    public string Load(string src) => $"[network] Loaded: {src}";
}

// Proxy + Strategy: caches results of the inner loader
public sealed class CachedImageLoader : IImageLoader
{
    private readonly IImageLoader _inner;
    private readonly Dictionary<string, string> _cache = new(StringComparer.Ordinal);

    public CachedImageLoader(IImageLoader inner)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    public string Load(string src)
    {
        if (_cache.TryGetValue(src, out var cached))
            return $"[cache hit] {cached}";

        var result = _inner.Load(src);
        _cache[src] = result;
        return result;
    }
}

// ── Image Node ────────────────────────────────────────────────────────────────

public sealed class LightImageNode : LightNode
{
    public string Src { get; }
    public string? Alt { get; init; }

    private readonly IImageLoader _loader;

    public LightImageNode(string src, IImageLoader loader)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(src);
        Src     = src;
        _loader = loader ?? throw new ArgumentNullException(nameof(loader));
    }

    public string Load()
    {
        var result = _loader.Load(Src);
        Console.WriteLine(result);
        return result;
    }

    public override string InnerHTML() => string.Empty;

    public override string OuterHTML()
    {
        var altAttr = Alt is not null ? $" alt=\"{Alt}\"" : string.Empty;
        return $"<img src=\"{Src}\"{altAttr} />";
    }

    public override void Accept(INodeVisitor visitor) => visitor.Visit(this);
}
