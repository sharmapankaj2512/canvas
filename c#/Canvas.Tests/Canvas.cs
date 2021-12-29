using System.Collections.Generic;
using System.Linq;

namespace Canvas.Tests;

class Canvas
{
    public IEnumerable<Point2D> CreateCanvas(int width, int height)
    {
        return Enumerable.Range(0, width)
            .Zip(Enumerable.Range(0, height))
            .Select(p => new Point2D(p.First, p.Second))
            .ToList();
    }
}