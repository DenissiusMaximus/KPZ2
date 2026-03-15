class Authenticator
{
    private Authenticator() { }

    private static Authenticator _instance;

    private static readonly object _lock = new object();

    public static Authenticator GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Authenticator();
                }
            }
        }
        return _instance;
    }

    public string User { get; set; }
}

class Program
{
    static void Main()
    {
        var firstAuthenticator = Authenticator.GetInstance();
        firstAuthenticator.User = "admin";

        var secondAuthenticator = Authenticator.GetInstance();

        Console.WriteLine($"Same instance: {ReferenceEquals(firstAuthenticator, secondAuthenticator)}");
        Console.WriteLine($"User from second reference: {secondAuthenticator.User}");
    }
}