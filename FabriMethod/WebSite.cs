namespace FabricMethod;

public class WebSite : SubscriptionFactory
{
    public override ISubscription CreateSubscription(string type)
    {
        return type.Trim().ToLower() switch
        {
            "domestic" => new DomesticSubscription(),
            "educational" => new EducationalSubscription(),
            "premium" => new PremiumSubscription(),
            _ => throw new ArgumentException("Invalid subscription type for WebSite")
        };
    }
}