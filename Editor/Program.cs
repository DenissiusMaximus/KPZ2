using System;

var doc = new TextDocument("Hello");
var editor = new TextEditor(doc);

editor.Show();
editor.Append(", world");
editor.Save();
editor.Show();

editor.Append("! More text.");
editor.Show();

Console.WriteLine("Undoing...");
editor.Undo();
editor.Show();
