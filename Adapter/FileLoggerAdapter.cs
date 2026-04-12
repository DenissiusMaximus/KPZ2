public class FileLoggerAdapter(IFileWriter fileWriter) : ILogger
{
    public void Log(string message)
    {
        fileWriter.Write(message);
    }

    public void Error(string message)
    {
        fileWriter.WriteLine(message);
    }

    public void Warn(string message)
    {
        fileWriter.Write(message);
    }
}