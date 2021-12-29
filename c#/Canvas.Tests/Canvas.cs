using System.Collections.Generic;
using System.Linq;

namespace Canvas.Tests;

class Canvas
{
    private int Width { get; }
    private int Height { get; }

    private Canvas(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static Canvas CreateCanvas(int width, int height)
    {
        return new Canvas(width, height);
    }

    public List<Point2D> Points()
    {
        return Enumerable.Range(0, Width)
            .Zip(Enumerable.Range(0, Height))
            .Select(p => new Point2D(p.First, p.Second))
            .ToList();
    }
}