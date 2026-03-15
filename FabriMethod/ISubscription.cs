namespace FabricMethod;

public interface ISubscription
{
    double MonthlyFee { get; }
    int MinimumPeriodMonths { get; }
    List<string> Channels { get; }
    List<string> Features { get; }
}