using System;

public class LightTextNode : LightNode
{
    public string Text { get; }

    public LightTextNode(string text)
    {
        Text = text;
    }

    public override string InnerHTML() => Text;
    public override string OuterHTML() => Text;
}
