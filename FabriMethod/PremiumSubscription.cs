namespace FabricMethod;

public class PremiumSubscription : ISubscription
{
    public double MonthlyFee => 29.99;
    public int MinimumPeriodMonths => 12;
    public List<string> Channels { get; } = new() { "HBO", "Showtime", "Cinemax" };
    public List<string> Features { get; } = new() { "4K quality", "5 devices", "Offline downloads" };
}