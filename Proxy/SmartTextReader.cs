using System;
using System.IO;
using System.Linq;

public class SmartTextReader
{
    public char[][] Read(string path)
    {
        var lines = File.ReadAllLines(path);
        return lines.Select(l => l.ToCharArray()).ToArray();
    }
}
