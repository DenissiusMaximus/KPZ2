namespace LightHTML.Core;

public sealed class LightTextNode : LightNode
{
    public string Text { get; }

    public LightTextNode(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        Text = text;
    }

    public override string InnerHTML() => Text;
    public override string OuterHTML() => Text;
    public override void Accept(INodeVisitor visitor) => visitor.Visit(this);
}
