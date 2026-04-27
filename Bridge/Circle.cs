public class Circle : Shape
{
    public Circle(IRenderer renderer) : base(renderer)
    {
        Name = "Circle";
    }

    public override void Draw()
    {
        _renderer.RenderShape(Name);
    }
}
