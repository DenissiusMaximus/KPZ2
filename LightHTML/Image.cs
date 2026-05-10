using System;

public interface IImageLoader
{
    string Load(string href);
}

public class FileImageLoader : IImageLoader
{
    public string Load(string href)
    {
        // Simulate file load
        return $"(file) Loaded image from {href}";
    }
}

public class NetworkImageLoader : IImageLoader
{
    public string Load(string href)
    {
        // Simulate network load
        return $"(network) Loaded image from {href}";
    }
}

public class LightImageNode : LightNode
{
    public string Href { get; }
    private readonly IImageLoader _loader;

    public LightImageNode(string href, IImageLoader loader)
    {
        Href = href;
        _loader = loader;
    }

    public string Load()
    {
        var r = _loader.Load(Href);
        Console.WriteLine(r);
        return r;
    }

    public override string InnerHTML() => string.Empty;

    public override string OuterHTML() => $"<img src=\"{Href}\" />";
}
