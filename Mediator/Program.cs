using DesignPatterns.Mediator;

class Program
{
    static void Main(string[] args)
    {
        Runway runway1 = new Runway();
        Runway runway2 = new Runway();

        Aircraft aircraft1 = new Aircraft("Boeing 737", 100);
        Aircraft aircraft2 = new Aircraft("Airbus A320", 90);
        Aircraft aircraft3 = new Aircraft("Cessna 172", 10);

        CommandCentre commandCentre = new CommandCentre(
            new[] { runway1, runway2 },
            new[] { aircraft1, aircraft2, aircraft3 }
        );

        Console.WriteLine("=== ТЕСТ ПОСАДКИ ===");
        aircraft1.Land();
        Console.WriteLine(new string('-', 30));
        
        aircraft2.Land(); 
        Console.WriteLine(new string('-', 30));
        
        aircraft3.Land(); 

        Console.WriteLine("\n=== ТЕСТ ЗЛЬОТУ ===");
        aircraft1.TakeOff(); 
        
        Console.WriteLine("\n=== ПОВТОРНА СПРОБА ПОСАДКИ ===");
        aircraft3.Land(); 
    }
}   