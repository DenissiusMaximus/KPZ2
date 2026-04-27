public class Triangle : Shape
{
    public Triangle(IRenderer renderer) : base(renderer)
    {
        Name = "Triangle";
    }

    public override void Draw()
    {
        _renderer.RenderShape(Name);
    }
}