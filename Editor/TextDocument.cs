using System;

public class TextDocument
{
    public string Content { get; private set; }

    public TextDocument(string content = "")
    {
        Content = content;
    }

    public void Append(string text)
    {
        Content += text;
    }

    public DocumentMemento CreateMemento() => new DocumentMemento(Content);

    public void Restore(DocumentMemento m)
    {
        Content = m.State;
    }
}

public record DocumentMemento(string State);
