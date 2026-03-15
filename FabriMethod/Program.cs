namespace FabricMethod;

public static class Program
{
    public static void Main()
    {
        SubscriptionFactory webSite = new WebSite();
        SubscriptionFactory mobileApp = new MobileApp();
        SubscriptionFactory managerCall = new ManagerCall();

        PrintSubscription("WebSite -> Premium", webSite.CreateSubscription("Premium"));
        PrintSubscription("MobileApp -> Domestic", mobileApp.CreateSubscription("Domestic"));
        PrintSubscription("ManagerCall -> Any", managerCall.CreateSubscription("Any"));

        try
        {
            mobileApp.CreateSubscription("Premium");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"MobileApp -> Premium: {ex.Message}");
        }
    }

    private static void PrintSubscription(string source, ISubscription subscription)
    {
        Console.WriteLine($"{source}");
        Console.WriteLine($"Monthly fee: {subscription.MonthlyFee}");
        Console.WriteLine($"Minimum period (months): {subscription.MinimumPeriodMonths}");
        Console.WriteLine($"Channels: {string.Join(", ", subscription.Channels)}");
        Console.WriteLine($"Features: {string.Join(", ", subscription.Features)}");
        Console.WriteLine();
    }
}