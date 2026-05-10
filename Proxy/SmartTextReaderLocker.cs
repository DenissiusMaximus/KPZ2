using System;
using System.IO;
using System.Text.RegularExpressions;

public class SmartTextReaderLocker
{
    private readonly SmartTextReader _reader;
    private readonly Regex _denyPattern;

    public SmartTextReaderLocker(SmartTextReader reader, string denyRegex)
    {
        _reader = reader;
        _denyPattern = new Regex(denyRegex, RegexOptions.Compiled);
    }

    public char[][] Read(string path)
    {
        var file = Path.GetFileName(path);
        if (_denyPattern.IsMatch(file))
        {
            Console.WriteLine("Access denied!");
            return null;
        }

        return _reader.Read(path);
    }
}
