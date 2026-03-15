namespace FabricMethod;

public class ManagerCall : SubscriptionFactory
{
    public override ISubscription CreateSubscription(string type)
    {
        return type.Trim().ToLower() switch
        {
            "premium" => new PremiumSubscription(),
            "educational" => new EducationalSubscription(),
            _ => new DomesticSubscription()
        };
    }
}