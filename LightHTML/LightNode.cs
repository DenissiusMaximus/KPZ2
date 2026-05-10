using System;
using System.Collections.Generic;

public abstract class LightNode
{
    public abstract string InnerHTML();
    public abstract string OuterHTML();

    public virtual void AddEventListener(string eventName, Action<LightElementNode, string> listener) { }
    public virtual void RemoveEventListener(string eventName, Action<LightElementNode, string> listener) { }
    public virtual void DispatchEvent(string eventName, string payload = "") { }
}
