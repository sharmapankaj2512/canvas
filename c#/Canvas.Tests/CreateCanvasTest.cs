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
}