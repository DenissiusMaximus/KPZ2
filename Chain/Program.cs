using System;

// Simple interactive menu to choose support level using Chain of Responsibility
var root = new Level1Handler();
var l2 = new Level2Handler();
var l3 = new Level3Handler();
var l4 = new Level4Handler();

root.SetNext(l2).SetNext(l3).SetNext(l4);

Console.WriteLine("Support system demo (type 'q' to quit)");
while (true)
{
    Console.WriteLine("Choose issue urgency: 1-Low,2-Medium,3-High,4-Critical");
    var input = Console.ReadLine();
    if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) break;
    if (!int.TryParse(input, out var severity) || severity < 1)
    {
        Console.WriteLine("Invalid input, try again.");
        continue;
    }

    Console.WriteLine("Enter short topic (billing/tech/account/other):");
    var topic = Console.ReadLine() ?? string.Empty;

    var req = new Request(topic, severity);
    root.Handle(req);

    Console.WriteLine();
}
