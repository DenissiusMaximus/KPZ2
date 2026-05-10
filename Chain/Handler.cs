using System;

public abstract class SupportHandler
{
    protected SupportHandler? Next { get; private set; }

    public SupportHandler SetNext(SupportHandler next)
    {
        Next = next;
        return next;
    }

    public void Handle(Request req)
    {
        if (!HandleRequest(req) && Next != null)
            Next.Handle(req);
    }

    protected abstract bool HandleRequest(Request req);
}

public record Request(string Topic, int Severity);

public class Level1Handler : SupportHandler
{
    protected override bool HandleRequest(Request req)
    {
        if (req.Severity <= 1)
        {
            Console.WriteLine("Level1 support handled the request (simple).");
            return true;
        }
        return false;
    }
}

public class Level2Handler : SupportHandler
{
    protected override bool HandleRequest(Request req)
    {
        if (req.Severity == 2)
        {
            Console.WriteLine("Level2 support handled the request (requires deeper knowledge).");
            return true;
        }
        return false;
    }
}

public class Level3Handler : SupportHandler
{
    protected override bool HandleRequest(Request req)
    {
        if (req.Severity == 3)
        {
            Console.WriteLine("Level3 support handled the request (specialist needed).");
            return true;
        }
        return false;
    }
}

public class Level4Handler : SupportHandler
{
    protected override bool HandleRequest(Request req)
    {
        if (req.Severity >= 4)
        {
            Console.WriteLine("Level4 support handled the request (manager/engineering).");
            return true;
        }
        return false;
    }
}
