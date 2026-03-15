public class Virus
{
    public int Weight { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public List<Virus> Children { get; set; }

    public Virus(int weight, int age, string name, string type)
    {
        Weight = weight;
        Age = age;
        Name = name;
        Type = type;
        Children = new List<Virus>();
    }

    public Virus Clone()
    {
        var clone = new Virus(Weight, Age, Name, Type)
        {
            Children = [.. Children.Select(child => child.Clone())]
        };
        return clone;
    }
}

public static class Program
{
    public static void Main()
    {
        Virus parent = new(12, 3, "Alpha", "A");
        parent.Children.Add(new Virus(3, 1, "Alpha-1", "A1"));

        Virus clone = parent.Clone();

        Console.WriteLine("Оригінал:");
        Console.WriteLine($"Name: {parent.Name}, Child: {parent.Children[0].Name}");

        Console.WriteLine("Клон:");
        Console.WriteLine($"Name: {clone.Name}, Child: {clone.Children[0].Name}");
    }
}

