namespace LightHTML.Core;

public delegate void EventListener(LightElementNode sender, string payload);

public interface INodeVisitor
{
    void Visit(LightElementNode element);
    void Visit(LightTextNode text);
    void Visit(LightImageNode image);
}

public abstract class LightNode
{
    public abstract string InnerHTML();
    public abstract string OuterHTML();
    public abstract void Accept(INodeVisitor visitor);

    public virtual void AddEventListener(string eventName, EventListener listener) { }
    public virtual void RemoveEventListener(string eventName, EventListener listener) { }
    public virtual void DispatchEvent(string eventName, string payload = "") { }
}
