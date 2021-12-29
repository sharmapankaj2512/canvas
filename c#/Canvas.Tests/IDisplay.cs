using System.Collections.Generic;

namespace Canvas.Tests;

public interface IDisplay
{
    void Render(IEnumerable<Point2D> points);
}