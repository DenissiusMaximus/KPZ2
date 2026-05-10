using System;

var centre = new CommandCentre();
var r1 = new Runway("Runway-1", centre);
var r2 = new Runway("Runway-2", centre);

var a1 = new Aircraft("AC-101", centre);
var a2 = new Aircraft("AC-202", centre);

Console.WriteLine("Mediator demo (CommandCentre)");
a1.RequestLanding();
a2.RequestLanding();

// Free runway and try again
r1.Free();
centre.RunwayFreed(r1);
Console.WriteLine("Now another request:");
a2.RequestLanding();
