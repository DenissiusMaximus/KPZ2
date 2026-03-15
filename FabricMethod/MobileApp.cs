namespace FabricMethod;

public class MobileApp : SubscriptionFactory
{
    public override ISubscription CreateSubscription(string type)
    {
        return type.Trim().ToLower() switch
        {
            "domestic" => new DomesticSubscription(),
            "educational" => new EducationalSubscription(),
            "premium" => throw new ArgumentException("Premium subscription is unavailable in MobileApp"),
            _ => throw new ArgumentException("Invalid subscription type for MobileApp")
        };
    }
}