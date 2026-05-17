using System.Collections;
using System.Text;

namespace LightHTML.Core;

public class LightElementNode : LightNode, IEnumerable<LightNode>
{
    private readonly ElementFlyweight? _flyweight;
    private readonly string? _tagName;
    private readonly DisplayType? _display;
    private readonly ClosingType? _closing;
    private readonly IReadOnlyList<string>? _classes;

    protected string TagName    => _flyweight?.TagName ?? _tagName  ?? string.Empty;
    private DisplayType Display => _flyweight?.Display ?? _display  ?? DisplayType.Block;
    private ClosingType Closing => _flyweight?.Closing ?? _closing  ?? ClosingType.Closed;
    private IEnumerable<string> Classes =>
        (_flyweight?.Classes ?? _classes) ?? Enumerable.Empty<string>();

    public List<LightNode> Children { get; } = [];
    public Dictionary<string, string> Style      { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Attributes { get; } = new(StringComparer.OrdinalIgnoreCase);

    public IElementState State { get; set; } = NormalState.Instance;

    private readonly Dictionary<string, List<EventListener>> _listeners =
        new(StringComparer.OrdinalIgnoreCase);

    public LightElementNode(
        string tagName,
        DisplayType display = DisplayType.Block,
        ClosingType closing = ClosingType.Closed,
        IEnumerable<string>? classes = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tagName);
        _tagName = tagName;
        _display = display;
        _closing = closing;
        _classes = classes?.ToArray() ?? [];
    }

    public LightElementNode(ElementFlyweight flyweight)
    {
        _flyweight = flyweight ?? throw new ArgumentNullException(nameof(flyweight));
    }

    public override string InnerHTML()
    {
        if (Children.Count == 0) return string.Empty;
        var sb = new StringBuilder(Children.Count * 32);
        foreach (var child in Children)
            sb.Append(child.OuterHTML());
        return sb.ToString();
    }

    public override string OuterHTML()
    {
        var attrs = BuildClassAttribute() + BuildStyleAttribute() + BuildExtraAttributes();
        return Closing == ClosingType.SelfClosing
            ? $"<{TagName}{attrs} />"
            : $"<{TagName}{attrs}>{InnerHTML()}</{TagName}>";
    }

    public LightElementNode Append(LightNode child)
    {
        Children.Add(child ?? throw new ArgumentNullException(nameof(child)));
        return this;
    }

    public LightElementNode AppendText(string text) => Append(new LightTextNode(text));

    public LightElementNode WithStyle(string property, string value)
    {
        Style[property] = value;
        return this;
    }

    public LightElementNode WithAttribute(string name, string value)
    {
        Attributes[name] = value;
        return this;
    }

    public override void AddEventListener(string eventName, EventListener listener)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventName);
        ArgumentNullException.ThrowIfNull(listener);
        if (!_listeners.TryGetValue(eventName, out var list))
            _listeners[eventName] = list = [];
        list.Add(listener);
    }

    public override void RemoveEventListener(string eventName, EventListener listener)
    {
        if (_listeners.TryGetValue(eventName, out var list))
            list.Remove(listener);
    }

    public override void DispatchEvent(string eventName, string payload = "")
    {
        if (!_listeners.TryGetValue(eventName, out var list) || list.Count == 0) return;
        foreach (var handler in list.ToArray())
            handler(this, payload);
    }

    public IEnumerator<LightNode> GetEnumerator() => DomIterator.DepthFirst(this).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerable<LightNode> BreadthFirst() => DomIterator.BreadthFirst(this);

    public override void Accept(INodeVisitor visitor) => visitor.Visit(this);

    private string BuildClassAttribute()
    {
        var all = Classes
            .Concat(State.AdditionalClasses)
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .ToArray();
        return all.Length > 0 ? $" class=\"{string.Join(' ', all)}\"" : string.Empty;
    }

    private string BuildStyleAttribute()
    {
        if (Style.Count == 0) return string.Empty;
        return $" style=\"{string.Join(';', Style.Select(kv => $"{kv.Key}:{kv.Value}"))}\"";
    }

    private string BuildExtraAttributes()
    {
        var all = Attributes
            .Concat(State.AdditionalAttributes)
            .Select(kv => string.IsNullOrEmpty(kv.Value) ? kv.Key : $"{kv.Key}=\"{kv.Value}\"");
        var result = string.Join(' ', all);
        return result.Length > 0 ? " " + result : string.Empty;
    }
}
