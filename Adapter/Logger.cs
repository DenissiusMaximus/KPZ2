public class Logger : ILogger
{
    public void Log(string message)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Log: {message}");
        Console.ForegroundColor = prev;
    }
    
    public void Error(string message)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ForegroundColor = prev;
    }
    
    public void Warn(string message)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Warn: {message}");
        Console.ForegroundColor = prev;
    }
}