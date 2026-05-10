using System;
using System.Collections.Generic;
using System.Linq;

public enum DisplayType { Block, Inline }
public enum ClosingType { Single, Closed }

public class LightElementNode : LightNode
{
    // Optional local metadata (when not using flyweight)
    private readonly string? _tagName;
    private readonly DisplayType? _display;
    private readonly ClosingType? _closing;
    private readonly List<string>? _classes;

    // Optional flyweight
    private readonly ElementFlyweight? _fly;

    public List<LightNode> Children { get; } = new();
    // Event listeners: event name -> list of handlers
    private readonly Dictionary<string, List<Action<LightElementNode, string>>> _listeners = new();

    // Constructor without flyweight
    public LightElementNode(string tagName, DisplayType display, ClosingType closing, IEnumerable<string> classes = null)
    {
        _tagName = tagName;
        _display = display;
        _closing = closing;
        _classes = classes != null ? new List<string>(classes) : new List<string>();
    }

    // Constructor with flyweight
    public LightElementNode(ElementFlyweight fly)
    {
        _fly = fly ?? throw new ArgumentNullException(nameof(fly));
    }

    private string TagName => _fly?.TagName ?? _tagName ?? string.Empty;
    private DisplayType Display => _fly?.Display ?? _display.GetValueOrDefault();
    private ClosingType Closing => _fly?.Closing ?? _closing.GetValueOrDefault();
    private IEnumerable<string> Classes => _fly?.Classes ?? (IEnumerable<string>? )_classes ?? Enumerable.Empty<string>();

    public override string InnerHTML() => string.Concat(Children.Select(c => c.OuterHTML()));

    public override string OuterHTML()
    {
        var classes = Classes != null && Classes.Any() ? $" class=\"{string.Join(' ', Classes)}\"" : string.Empty;

        if (Closing == ClosingType.Single)
            return $"<{TagName}{classes} />";

        return $"<{TagName}{classes}>{InnerHTML()}</{TagName}>";
    }

    public override void AddEventListener(string eventName, Action<LightElementNode, string> listener)
    {
        if (!_listeners.TryGetValue(eventName, out var list))
        {
            list = new List<Action<LightElementNode, string>>();
            _listeners[eventName] = list;
        }

        list.Add(listener);
    }

    public override void RemoveEventListener(string eventName, Action<LightElementNode, string> listener)
    {
        if (_listeners.TryGetValue(eventName, out var list))
            list.Remove(listener);
    }

    public override void DispatchEvent(string eventName, string payload = "")
    {
        if (!_listeners.TryGetValue(eventName, out var list)) return;
        foreach (var l in list)
            l(this, payload);
    }
}
