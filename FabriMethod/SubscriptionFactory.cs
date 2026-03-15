namespace FabricMethod;

public abstract class SubscriptionFactory
{
    public abstract ISubscription CreateSubscription(string type);
}