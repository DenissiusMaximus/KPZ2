public class Client(ILogger logger)
{
    public void DoSomething()
    {
        logger.Log("Doing something...");
    }
    
    public void DoSomethingElse()
    {
        logger.Error("Something went wrong!");
    }
}