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
            new Canvas().CreateCanvas(0, 0),
            Enumerable.Empty<Point2D>());
    }
}