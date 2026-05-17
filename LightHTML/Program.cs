using LightHTML.Core;

// ── Helpers 

static LightElementNode ClassifyLine(string line, int index, FlyweightFactory factory, bool useFlyweight)
{
    ElementFlyweight Fw(string tag) =>
        factory.Get(tag, DisplayType.Block, ClosingType.Closed);

    if (useFlyweight)
    {
        var fw = index == 0                            ? Fw("h1")
               : line.StartsWith(' ') || line.StartsWith('\t') ? Fw("blockquote")
               : line.Length < 20                     ? Fw("h2")
               :                                        Fw("p");
        return new LightElementNode(fw);
    }
    else
    {
        var tag = index == 0                            ? "h1"
                : line.StartsWith(' ') || line.StartsWith('\t') ? "blockquote"
                : line.Length < 20                     ? "h2"
                :                                        "p";
        return new LightElementNode(tag);
    }
}

static LightElementNode BuildTree(IReadOnlyList<string> lines, FlyweightFactory factory, bool useFlyweight)
{
    var root = useFlyweight
        ? new LightElementNode(factory.Get("div"))
        : new LightElementNode("div");

    for (int i = 0; i < lines.Count; i++)
    {
        var line = lines[i];
        ClassifyLine(line, i, factory, useFlyweight)
            .AppendText(line.Trim())
            .WithAttribute("data-index", i.ToString())
            .Pipe(el => root.Append(el));
    }

    return root;
}

static long MeasureDelta(Action action)
{
    GC.Collect();
    var before = GC.GetTotalMemory(true);
    action();
    GC.Collect();
    return GC.GetTotalMemory(true) - before;
}

// ── Data ──────────────────────────────────────────────────────────────────────

var lines = new[]
{
    "My Book Title",
    "Short",
    "    indented quote",
    "This is a regular paragraph line that is quite long and should become a <p> element."
};

var factory = new FlyweightFactory();

// ── 1. Memory comparison ──────────────────────────────────────────────────────

Console.WriteLine("═══ Memory Comparison ═══════════════════════════════════");

LightElementNode? regularRoot = null;
var regularDelta = MeasureDelta(() => regularRoot = BuildTree(lines, factory, useFlyweight: false));
Console.WriteLine($"  Regular   tree Δ: {regularDelta,6} bytes");

LightElementNode? flyRoot = null;
var flyDelta = MeasureDelta(() => flyRoot = BuildTree(lines, factory, useFlyweight: true));
Console.WriteLine($"  Flyweight tree Δ: {flyDelta,6} bytes");
Console.WriteLine($"  Flyweight cache size: {factory.CacheSize} entries");

// ── 2. HTML output ────────────────────────────────────────────────────────────

Console.WriteLine("\n═══ HTML Output (flyweight tree) ════════════════════════");
Console.WriteLine(flyRoot!.OuterHTML());

// ── 3. Observer: event listeners ─────────────────────────────────────────────

Console.WriteLine("\n═══ Observer: Event System ══════════════════════════════");
if (flyRoot.Children.FirstOrDefault() is LightElementNode first)
{
    EventListener onClick = (sender, payload) =>
        Console.WriteLine($"  click on <{sender.TagName()}> | payload: '{payload}'");

    first.AddEventListener("click", onClick);
    first.DispatchEvent("click", "user-click");
    first.RemoveEventListener("click", onClick);
    first.DispatchEvent("click", "after-remove");   // silence expected
    Console.WriteLine("  (no output after removal — correct)");
}

// ── 4. Fluent builder + Style/Attr ────────────────────────────────────────────

Console.WriteLine("\n═══ Fluent Builder ══════════════════════════════════════");
var card = new LightElementNode("div", classes: ["card", "highlight"])
    .WithStyle("color", "#fff")
    .WithStyle("background", "#333")
    .WithAttribute("id", "main-card")
    .AppendText("Hello, LightHTML!")
    .Append(new LightImageNode("logo.png", new FileImageLoader()) { Alt = "Logo" });

Console.WriteLine(card.OuterHTML());

// ── 5. Strategy: image loading ────────────────────────────────────────────────

Console.WriteLine("\n═══ Strategy: Image Loading ══════════════════════════════");
var cachedNet = new CachedImageLoader(new NetworkImageLoader());

var img1 = new LightImageNode("C:\\images\\cover.jpg",   new FileImageLoader());
var img2 = new LightImageNode("http://example.com/a.png", cachedNet);
var img3 = new LightImageNode("http://example.com/a.png", cachedNet); // cache hit

foreach (var img in new[] { img1, img2, img3 })
{
    Console.Write($"  {img.OuterHTML()} → ");
    img.Load();
}

// ── 6. Visitor ────────────────────────────────────────────────────────────────

Console.WriteLine("\n═══ Visitor: Node Count ══════════════════════════════════");
var counter = new NodeCountVisitor();
flyRoot.Accept(counter);
Console.WriteLine($"  Elements: {counter.Elements}, Texts: {counter.Texts}, Images: {counter.Images}, Total: {counter.Total}");

// ── Extension helpers (local) ─────────────────────────────────────────────────

static class LightElementNodeExtensions
{
    // Reflect TagName without exposing it publicly on the node
    public static string TagName(this LightElementNode el) =>
        el.OuterHTML().TrimStart('<').Split([ ' ', '>' ])[0];
}

static class PipeExtensions
{
    public static T Pipe<T>(this T value, Action<T> action) { action(value); return value; }
}
