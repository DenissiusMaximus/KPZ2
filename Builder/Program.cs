public class Character
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Height { get; set; }
    public string Build { get; set; } = string.Empty;
    public string HairColor { get; set; } = string.Empty;
    public string EyeColor { get; set; } = string.Empty;
    public string Outfit { get; set; } = string.Empty;
    public List<string> Inventory { get; set; } = new List<string>();
    public List<string> Deeds { get; set; } = new List<string>();

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, Color: {Color}, Type: {Type}, Height: {Height}, Build: {Build}, HairColor: {HairColor}, EyeColor: {EyeColor}, Outfit: {Outfit}, Inventory: {string.Join(", ", Inventory)}, Deeds: {string.Join(", ", Deeds)}";
    }
}

public interface IBuilder
{
    IBuilder SetAge(int age);
    IBuilder SetName(string name);
    IBuilder SetColor(string color);
    IBuilder SetType();
    IBuilder SetHeight(double height);
    IBuilder SetBuild(string build);
    IBuilder SetHairColor(string hairColor);
    IBuilder SetEyeColor(string eyeColor);
    IBuilder SetOutfit(string outfit);
    IBuilder AddToInventory(string item);
    IBuilder AddDeed(string deed);
    Character GetResult();
}

public class HeroBuilder : IBuilder
{
    private Character _character = new Character();

    public IBuilder SetAge(int age)
    {
        _character.Age = age;
        return this;
    }

    public IBuilder SetName(string name)
    {
        _character.Name = name;
        return this;
    }

    public IBuilder SetColor(string color)
    {
        _character.Color = color;
        return this;
    }

    public IBuilder SetType()
    {
        _character.Type = "Hero";
        return this;
    }

    public IBuilder SetHeight(double height)
    {
        _character.Height = height;
        return this;
    }

    public IBuilder SetBuild(string build)
    {
        _character.Build = build;
        return this;
    }

    public IBuilder SetHairColor(string hairColor)
    {
        _character.HairColor = hairColor;
        return this;
    }

    public IBuilder SetEyeColor(string eyeColor)
    {
        _character.EyeColor = eyeColor;
        return this;
    }

    public IBuilder SetOutfit(string outfit)
    {
        _character.Outfit = outfit;
        return this;
    }

    public IBuilder AddToInventory(string item)
    {
        _character.Inventory.Add(item);
        return this;
    }

    public IBuilder AddDeed(string deed)
    {
        _character.Deeds.Add(deed);
        return this;
    }

    public Character GetResult()
    {
        return _character;
    }
}

public class EnemyBuilder : IBuilder
{
    private Character _character = new Character();

    public IBuilder SetAge(int age)
    {
        _character.Age = age;
        return this;
    }

    public IBuilder SetName(string name)
    {
        _character.Name = name;
        return this;
    }

    public IBuilder SetColor(string color)
    {
        _character.Color = color;
        return this;
    }

    public IBuilder SetType()
    {
        _character.Type = "Enemy";
        return this;
    }

    public IBuilder SetHeight(double height)
    {
        _character.Height = height;
        return this;
    }

    public IBuilder SetBuild(string build)
    {
        _character.Build = build;
        return this;
    }

    public IBuilder SetHairColor(string hairColor)
    {
        _character.HairColor = hairColor;
        return this;
    }

    public IBuilder SetEyeColor(string eyeColor)
    {
        _character.EyeColor = eyeColor;
        return this;
    }

    public IBuilder SetOutfit(string outfit)
    {
        _character.Outfit = outfit;
        return this;
    }

    public IBuilder AddToInventory(string item)
    {
        _character.Inventory.Add(item);
        return this;
    }

    public IBuilder AddDeed(string deed)
    {
        _character.Deeds.Add(deed);
        return this;
    }

    public Character GetResult()
    {
        return _character;
    }
}

public interface IDirector
{
    void Construct(IBuilder builder);
}

public class CharacterDirector : IDirector
{
    public void Construct(IBuilder builder)
    {
        builder.SetName("John")
               .SetAge(30)
               .SetColor("Red")
               .SetType()
               .SetHeight(180)
               .SetBuild("Athletic")
               .SetHairColor("Black")
               .SetEyeColor("Blue")
               .SetOutfit("Armor")
               .AddToInventory("Sword")
               .AddToInventory("Shield")
               .AddDeed("Saved a village");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        CharacterDirector director = new CharacterDirector();

        HeroBuilder heroBuilder = new HeroBuilder();
        director.Construct(heroBuilder);
        Character hero = heroBuilder.GetResult();
        Console.WriteLine("Hero:");
        Console.WriteLine(hero);

        EnemyBuilder enemyBuilder = new EnemyBuilder();
        director.Construct(enemyBuilder);
        enemyBuilder.AddDeed("Burned a village");
        Character enemy = enemyBuilder.GetResult();
        Console.WriteLine("\nEnemy:");
        Console.WriteLine(enemy);
    }
}