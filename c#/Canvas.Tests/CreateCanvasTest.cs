using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class CreateCanvasTest
{
    [Test]
    public void CreateZeroByZeroCanvas()
    {
        Assert.AreEqual(
            Canvas.CreateCanvas(0, 0).Points(),
            Enumerable.Empty<Point2D>());
    }

    [Test]
    public void CreateOneByOneCanvas()
    {
        Assert.AreEqual(
            Canvas.CreateCanvas(1, 1).Points(),
            new List<Point2D> {new(0, 0)});
    }
}