using System;
using System.Collections.Generic;

public class TextEditor
{
    private readonly TextDocument _doc;
    private readonly Stack<DocumentMemento> _history = new();

    public TextEditor(TextDocument doc)
    {
        _doc = doc;
        _history.Push(_doc.CreateMemento());
    }

    public void Append(string text)
    {
        _doc.Append(text);
    }

    public void Save()
    {
        _history.Push(_doc.CreateMemento());
    }

    public void Undo()
    {
        if (_history.Count <= 1)
        {
            Console.WriteLine("Nothing to undo.");
            return;
        }

        _history.Pop();
        var m = _history.Peek();
        _doc.Restore(m);
    }

    public void Show() => Console.WriteLine(_doc.Content);
}
