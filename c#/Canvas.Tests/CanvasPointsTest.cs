using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class CanvasPointsTest
{
    public static IEnumerable<TestCaseData> CanvasPointsData
    {
        get
        {
            yield return new TestCaseData(0, 0, Enumerable.Empty<Point2D>()).SetName("empty canvas zero points");
            yield return new TestCaseData(1, 1, new List<Point2D> {new(0, 0)}).SetName(
                "one by one canvas has one point");
        }
    }


    [TestCaseSource("CanvasPointsData")]
    public void CanvasPoints(int width, int height, IEnumerable<Point2D> expected)
    {
        var aCanvas = Canvas.CreateCanvasv2(width, height);
        aCanvas.Do(c => Assert.AreEqual(c.Points(), expected));
    }
}