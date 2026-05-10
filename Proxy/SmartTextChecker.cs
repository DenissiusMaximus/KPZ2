using System;

public class SmartTextChecker
{
    private readonly SmartTextReader _reader;

    public SmartTextChecker(SmartTextReader reader)
    {
        _reader = reader;
    }

    public char[][] Read(string path)
    {
        Console.WriteLine($"Opening file {path}...");
        var data = _reader.Read(path);
        Console.WriteLine("Read complete.");
        var lines = data?.Length ?? 0;
        var chars = data == null ? 0 : 0 + System.Linq.Enumerable.Sum(data, a => a.Length);
        Console.WriteLine($"Lines: {lines}, Chars: {chars}");
        Console.WriteLine("Closing file.");
        return data;
    }
}
