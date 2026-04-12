public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}", ConsoleColor.Green);
    }
    
    public void Error(string message)
    {
        Console.WriteLine($"Error: {message}", ConsoleColor.Red);
    }
    
    public void Warn(string message)
    {
        Console.WriteLine($"Warn: {message}", ConsoleColor.Yellow);
    }
}