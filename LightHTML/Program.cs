using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

// Demo: build LightHTML tree from text and measure memory usage
var lines = new[] {
    "My Book Title",
    "Short",
    "    indented quote",
    "This is a regular paragraph line that is quite long and should become a <p> element."
};

Console.WriteLine("Building regular tree (no flyweight)...");
GC.Collect();
var before = GC.GetTotalMemory(true);

var root = new LightElementNode("div", DisplayType.Block, ClosingType.Closed);
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    LightElementNode el;
    if (i == 0)
        el = new LightElementNode("h1", DisplayType.Block, ClosingType.Closed);
    else if (line.StartsWith(" ") || line.StartsWith("\t"))
        el = new LightElementNode("blockquote", DisplayType.Block, ClosingType.Closed);
    else if (line.Length < 20)
        el = new LightElementNode("h2", DisplayType.Block, ClosingType.Closed);
    else
        el = new LightElementNode("p", DisplayType.Block, ClosingType.Closed);

    el.Children.Add(new LightTextNode(line.Trim()));
    root.Children.Add(el);
}

GC.Collect();
var after = GC.GetTotalMemory(true);
Console.WriteLine($"Regular tree memory delta: {after - before} bytes");
Console.WriteLine("Sample outerHTML of first child:");
Console.WriteLine(root.Children[0].OuterHTML());

// Now build using flyweight
Console.WriteLine("\nBuilding flyweight-backed tree...");
GC.Collect();
before = GC.GetTotalMemory(true);

var factory = new FlyweightFactory();
var root2 = new LightElementNode(factory.Get("div", DisplayType.Block, ClosingType.Closed));
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    ElementFlyweight fw;
    if (i == 0)
        fw = factory.Get("h1", DisplayType.Block, ClosingType.Closed);
    else if (line.StartsWith(" ") || line.StartsWith("\t"))
        fw = factory.Get("blockquote", DisplayType.Block, ClosingType.Closed);
    else if (line.Length < 20)
        fw = factory.Get("h2", DisplayType.Block, ClosingType.Closed);
    else
        fw = factory.Get("p", DisplayType.Block, ClosingType.Closed);

    var el = new LightElementNode(fw);
    el.Children.Add(new LightTextNode(line.Trim()));
    root2.Children.Add(el);
}

GC.Collect();
after = GC.GetTotalMemory(true);
Console.WriteLine($"Flyweight tree memory delta: {after - before} bytes");
Console.WriteLine("Sample outerHTML of first child (flyweight):");
Console.WriteLine(root2.Children[0].OuterHTML());

// Observer demo: add event listener to first heading
Console.WriteLine("\nObserver demo: adding click listener to first h1");
if (root2.Children.Count > 0 && root2.Children[0] is LightElementNode first)
{
    first.AddEventListener("click", (sender, payload) =>
    {
        Console.WriteLine($"Event 'click' received on <{sender.OuterHTML()}> payload: {payload}");
    });

    // simulate click
    first.DispatchEvent("click", "user-click");
}

// Strategy demo: Image element with file and network loaders
Console.WriteLine("\nStrategy demo: image loading");
var fileLoader = new FileImageLoader();
var netLoader = new NetworkImageLoader();

var img1 = new LightImageNode("C:\\images\\cover.jpg", fileLoader);
var img2 = new LightImageNode("http://example.com/pic.png", netLoader);

Console.WriteLine(img1.OuterHTML());
img1.Load();

Console.WriteLine(img2.OuterHTML());
img2.Load();
