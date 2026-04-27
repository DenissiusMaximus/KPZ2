public class Square : Shape
{
    public Square(IRenderer renderer) : base(renderer)
    {
        Name = "Square";
    }

    public override void Draw()
    {
        _renderer.RenderShape(Name);
    }
}