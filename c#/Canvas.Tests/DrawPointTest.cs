using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class DrawPointTest
{
    [Test]
    public void BorderPointOnly()
    {
        Canvas.CreateCanvas(1, 1).ConsumeRight(canvas =>
        {
            canvas.DrawPoint(Tuple.Create(0, 0));
            
            Assert.AreEqual(new List<Point2D>
            {
                new Border(0, 0)
            }, canvas.Points());
        });
    }
}