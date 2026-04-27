IRenderer vector = new VectorRenderer();
IRenderer raster = new RasterRenderer();

Shape circle1 = new Circle(vector);
Shape circle2 = new Circle(raster);
Shape square = new Square(vector);

circle1.Draw();
circle2.Draw();
square.Draw();