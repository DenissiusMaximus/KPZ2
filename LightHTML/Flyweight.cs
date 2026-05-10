using System;
using System.Collections.Generic;
using System.Linq;

public class ElementFlyweight
{
    public string TagName { get; }
    public DisplayType Display { get; }
    public ClosingType Closing { get; }
    public IEnumerable<string> Classes { get; }

    public ElementFlyweight(string tagName, DisplayType display, ClosingType closing, IEnumerable<string> classes = null)
    {
        TagName = tagName;
        Display = display;
        Closing = closing;
        Classes = classes != null ? classes.ToArray() : Array.Empty<string>();
    }
}

public class FlyweightFactory
{
    private readonly Dictionary<string, ElementFlyweight> _cache = new();

    public ElementFlyweight Get(string tagName, DisplayType display, ClosingType closing, IEnumerable<string> classes = null)
    {
        var key = tagName + "|" + display + "|" + closing + "|" + (classes == null ? "" : string.Join(',', classes));
        if (!_cache.TryGetValue(key, out var fw))
        {
            fw = new ElementFlyweight(tagName, display, closing, classes);
            _cache[key] = fw;
        }

        return fw;
    }
}
