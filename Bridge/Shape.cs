public abstract class Shape(IRenderer renderer)
{
    public string Name { get; protected set; }
    
    protected readonly IRenderer _renderer = renderer;

    public abstract void Draw();
}