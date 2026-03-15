namespace FabricMethod;

public class DomesticSubscription : ISubscription
{
    public double MonthlyFee => 9.99;
    public int MinimumPeriodMonths => 1;
    public List<string> Channels { get; } = new() { "UA:Перший", "1+1", "ICTV" };
    public List<string> Features { get; } = new() { "SD quality", "1 device" };
}