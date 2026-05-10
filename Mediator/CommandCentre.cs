using System;
using System.Collections.Generic;

public class CommandCentre
{
    private readonly List<Runway> _runways = new();

    public void RegisterRunway(Runway r) => _runways.Add(r);

    public bool RequestLanding(Aircraft a)
    {
        var free = _runways.Find(r => !r.IsOccupied);
        if (free != null)
        {
            free.Occupy();
            Console.WriteLine($"{a.Name} cleared to land on {free.Name}.");
            return true;
        }

        Console.WriteLine($"{a.Name} hold position — no free runway.");
        return false;
    }

    public void RunwayFreed(Runway r)
    {
        Console.WriteLine($"Runway {r.Name} is free now.");
    }
}

public class Aircraft
{
    private readonly CommandCentre _centre;
    public string Name { get; }

    public Aircraft(string name, CommandCentre centre)
    {
        Name = name;
        _centre = centre;
    }

    public void RequestLanding() => _centre.RequestLanding(this);
}

public class Runway
{
    public string Name { get; }
    public bool IsOccupied { get; private set; }

    public Runway(string name, CommandCentre centre)
    {
        Name = name;
        centre.RegisterRunway(this);
    }

    public void Occupy() => IsOccupied = true;
    public void Free() => IsOccupied = false;
}
