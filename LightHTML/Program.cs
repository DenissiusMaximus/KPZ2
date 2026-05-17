using LightHTML.Core;

// ── Helpers 

var lines = new[]
{
    "My Book Title",
    "Short",
    "    indented quote",
    "This is a regular paragraph line that is quite long and should become a <p> element."
};

var factory = new FlyweightFactory();

LightElementNode BuildTree(bool useFlyweight)
{
    var root = useFlyweight
        ? new LightElementNode(factory.Get("div"))
        : new LightElementNode("div");

    for (int i = 0; i < lines.Length; i++) 
    {
        var line = lines[i];
        var tag  = i == 0                                        ? "h1"
                 : line.StartsWith(' ') || line.StartsWith('\t') ? "blockquote"
                 : line.Length < 20                              ? "h2"
                 :                                                 "p";

        var el = useFlyweight
            ? new LightElementNode(factory.Get(tag)) : new LightElementNode(tag);

        el.AppendText(line.Trim()).WithAttribute("data-index", i.ToString());
        root.Append(el);
    }
    return root;
}

long MemDelta(Action action)
{
    GC.Collect();
    var b = GC.GetTotalMemory(true);
    action();
    GC.Collect();
    return GC.GetTotalMemory(true) - b;
}

static string NodeLabel(LightNode n) => n switch
{
    LightElementNode el => "<" + el.OuterHTML().TrimStart('<').Split([' ', '>'])[0] + ">",
    LightTextNode txt   => $"text({txt.Text[..Math.Min(6, txt.Text.Length)]}…)",
    _                   => n.GetType().Name
};

// === Flyweight ===
Console.WriteLine("=== Flyweight ===");
LightElementNode? regular = null, fly = null;
Console.WriteLine($"  Regular   delta: {MemDelta(() => regular = BuildTree(false)),6} bytes");
Console.WriteLine($"  Flyweight delta: {MemDelta(() => fly     = BuildTree(true)),6} bytes  (cache: {factory.CacheSize})");

// === Visitor ===
Console.WriteLine("\n=== Visitor ===");
var counter = new NodeCountVisitor();
fly!.Accept(counter);
Console.WriteLine($"  Elements={counter.Elements}, Texts={counter.Texts}, Total={counter.Total}");

var printer = new PrettyPrintVisitor();
fly.Accept(printer);
Console.Write(printer);

// === Iterator ===
Console.WriteLine("=== Iterator ===");
Console.WriteLine("  DFS: " + string.Join(", ", fly.Select(NodeLabel)));
Console.WriteLine("  BFS: " + string.Join(", ", fly.BreadthFirst().Select(NodeLabel)));

// === Command ===
Console.WriteLine("\n=== Command (undo/redo) ===");
var history = new DomHistory();
var div  = new LightElementNode("div");
var span = new LightElementNode("span").AppendText("hello");

history.Execute(new AppendChildCommand(div, span));
Console.WriteLine($"  append:   {div.OuterHTML()}");

history.Execute(new SetAttributeCommand(span, "id", "greeting"));
Console.WriteLine($"  set-id:   {div.OuterHTML()}");

history.Undo();
Console.WriteLine($"  undo:     {div.OuterHTML()}");

history.Undo();
Console.WriteLine($"  undo x2:  {div.OuterHTML()}");

history.Redo();
Console.WriteLine($"  redo:     {div.OuterHTML()}");

// === State ===
Console.WriteLine("\n=== State ===");
var btn = new LightElementNode("button", classes: ["btn"]);
Console.WriteLine($"  Normal:   {btn.OuterHTML()}");

btn.State = btn.State.OnFocus();
Console.WriteLine($"  Focused:  {btn.OuterHTML()}");

btn.State = btn.State.OnDisable();
Console.WriteLine($"  Disabled: {btn.OuterHTML()}");

btn.State = btn.State.OnEnable();
Console.WriteLine($"  Enabled:  {btn.OuterHTML()}");

// === Template Method ===
Console.WriteLine("\n=== Template Method (Lifecycle) ===");
var logged = new LoggedElement("p");
logged.AppendText("lifecycle test");
_ = logged.OuterHTML();
logged.OnInserted();
logged.OnRemoved();

sealed class LoggedElement : LifecycleElementNode
{
    public LoggedElement(string tag) : base(tag) { }

    protected override void OnCreated()                => Console.WriteLine("  [hook] OnCreated");
    protected override void OnBeforeRender()           => Console.WriteLine("  [hook] OnBeforeRender");
    protected override void OnAfterRender(string html) => Console.WriteLine($"  [hook] OnAfterRender -> {html}");
    public    override void OnInserted()               => Console.WriteLine("  [hook] OnInserted");
    public    override void OnRemoved()                => Console.WriteLine("  [hook] OnRemoved");
}
