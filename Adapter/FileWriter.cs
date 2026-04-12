public class FileWriter : IFileWriter
{
    public void Write(string message)
    {
        Console.WriteLine($"Write {message}");
    }
    
    public void WriteLine(string message)
    {
        Console.WriteLine($"WriteLine {message}");
    }
}

