using System.Text;

namespace LightHTML.Core;

public sealed class PrettyPrintVisitor : INodeVisitor
{
    private readonly StringBuilder _sb = new();
    private int _depth;

    private string Indent => new(' ', _depth * 2);

    public void Visit(LightElementNode element)
    {
        var tag = element.OuterHTML().Split('>')[0] + ">";
        _sb.AppendLine($"{Indent}{tag}");
        _depth++;
        foreach (var child in element.Children)
            child.Accept(this);
        _depth--;
    }

    public void Visit(LightTextNode text) =>
        _sb.AppendLine($"{Indent}\"{text.Text}\"");

    public void Visit(LightImageNode image) =>
        _sb.AppendLine($"{Indent}<img src=\"{image.Src}\" />");

    public override string ToString() => _sb.ToString();
}

public sealed class NodeCountVisitor : INodeVisitor
{
    public int Elements { get; private set; }
    public int Texts    { get; private set; }
    public int Images   { get; private set; }
    public int Total    => Elements + Texts + Images;

    public void Visit(LightElementNode element)
    {
        Elements++;
        foreach (var child in element.Children)
            child.Accept(this);
    }

    public void Visit(LightTextNode text)   => Texts++;
    public void Visit(LightImageNode image) => Images++;
}
