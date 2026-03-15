namespace FabricMethod;

public class EducationalSubscription : ISubscription
{
    public double MonthlyFee => 19.99;
    public int MinimumPeriodMonths => 6;
    public List<string> Channels { get; } = new() { "Discovery", "National Geographic", "History" };
    public List<string> Features { get; } = new() { "HD quality", "2 devices", "Student webinars" };
}