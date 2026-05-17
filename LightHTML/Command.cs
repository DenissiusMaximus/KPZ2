namespace LightHTML.Core;

public interface ICommand
{
    void Execute();
    void Undo();
}

public sealed class AppendChildCommand : ICommand
{
    private readonly LightElementNode _parent;
    private readonly LightNode _child;

    public AppendChildCommand(LightElementNode parent, LightNode child)
    {
        _parent = parent;
        _child  = child;
    }

    public void Execute() => _parent.Children.Add(_child);
    public void Undo()    => _parent.Children.Remove(_child);
}

public sealed class RemoveChildCommand : ICommand
{
    private readonly LightElementNode _parent;
    private readonly LightNode _child;
    private int _index = -1;

    public RemoveChildCommand(LightElementNode parent, LightNode child)
    {
        _parent = parent;
        _child  = child;
    }

    public void Execute()
    {
        _index = _parent.Children.IndexOf(_child);
        if (_index >= 0) _parent.Children.RemoveAt(_index);
    }

    public void Undo()
    {
        if (_index >= 0) _parent.Children.Insert(_index, _child);
    }
}

public sealed class SetAttributeCommand : ICommand
{
    private readonly LightElementNode _element;
    private readonly string _name;
    private readonly string _value;
    private string? _previous;

    public SetAttributeCommand(LightElementNode element, string name, string value)
    {
        _element = element;
        _name    = name;
        _value   = value;
    }

    public void Execute()
    {
        _element.Attributes.TryGetValue(_name, out _previous);
        _element.Attributes[_name] = _value;
    }

    public void Undo()
    {
        if (_previous is null) _element.Attributes.Remove(_name);
        else _element.Attributes[_name] = _previous;
    }
}

public sealed class DomHistory
{
    private readonly Stack<ICommand> _undo = new();
    private readonly Stack<ICommand> _redo = new();

    public void Execute(ICommand command)
    {
        command.Execute();
        _undo.Push(command);
        _redo.Clear();
    }

    public void Undo()
    {
        if (_undo.Count == 0) return;
        var cmd = _undo.Pop();
        cmd.Undo();
        _redo.Push(cmd);
    }

    public void Redo()
    {
        if (_redo.Count == 0) return;
        var cmd = _redo.Pop();
        cmd.Execute();
        _undo.Push(cmd);
    }

    public bool CanUndo => _undo.Count > 0;
    public bool CanRedo => _redo.Count > 0;
}
