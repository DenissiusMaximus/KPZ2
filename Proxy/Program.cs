using System;
using System.IO;

// Demo for SmartTextReader and proxies
var tmp = Path.Combine(Path.GetTempPath(), "lab3_sample.txt");
File.WriteAllLines(tmp, new[] {
    "Book Title",
    "Short line",
    "    indented line",
    "This is a longer paragraph line that exceeds twenty characters."
});

Console.WriteLine($"Sample file created: {tmp}");

ISmartTextReader reader = new SmartTextReader();
ISmartTextReader checker = new SmartTextChecker(reader);
ISmartTextReader locker = new SmartTextReaderLocker(reader, "^lab3_.*\\.txt$"); // deny files starting with lab3_

Console.WriteLine("--- Using SmartTextChecker (logging) ---");
checker.Read(tmp);

Console.WriteLine("--- Using SmartTextReaderLocker (should deny) ---");
locker.Read(tmp);

// Read a non-denied file
var other = Path.Combine(Path.GetTempPath(), "other_sample.txt");
File.WriteAllLines(other, new[] { "Hello", "World" });
Console.WriteLine("--- Locker reading non-denied file ---");
locker = new SmartTextReaderLocker(reader, "^secret_.*\\.txt$");
var data = locker.Read(other);
Console.WriteLine(data == null ? "No data" : $"Read {data.Length} lines.");
