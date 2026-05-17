namespace LightHTML.Core;

public abstract class LifecycleElementNode : LightElementNode
{
    protected LifecycleElementNode(
        string tagName,
        DisplayType display = DisplayType.Block,
        ClosingType closing = ClosingType.Closed,
        IEnumerable<string>? classes = null)
        : base(tagName, display, closing, classes)
    {
        OnCreated();
    }

    // Template method — defines the render skeleton
    public sealed override string OuterHTML()
    {
        OnBeforeRender();
        var html = base.OuterHTML();
        OnAfterRender(html);
        return html;
    }

    // Lifecycle hooks — override in subclasses
    protected virtual void OnCreated()          { }
    protected virtual void OnBeforeRender()     { }
    protected virtual void OnAfterRender(string html) { }
    public    virtual void OnInserted()         { }
    public    virtual void OnRemoved()          { }
}
