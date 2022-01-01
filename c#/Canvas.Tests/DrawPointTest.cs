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

    [Test]
    public void SomeBorderPoints()
    {
        Canvas.CreateCanvas(2, 2).ConsumeRight(canvas =>
        {
            canvas.DrawPoint(Tuple.Create(0, 1));
            canvas.DrawPoint(Tuple.Create(1, 0));

            Assert.AreEqual(new List<Point2D>
            {
                new Point2D(0, 0),
                new Border(0, 1),
                new Border(1, 0),
                new Point2D(1, 1),
            }, canvas.Points());
        });
    }

    [Test]
    public void AllBorderPoints()
    {
        Canvas.CreateCanvas(2, 2).ConsumeRight(canvas =>
        {
            canvas.DrawPoint(Tuple.Create(0, 0));
            canvas.DrawPoint(Tuple.Create(0, 1));
            canvas.DrawPoint(Tuple.Create(1, 0));
            canvas.DrawPoint(Tuple.Create(1, 1));

            Assert.AreEqual(new List<Point2D>
            {
                new Border(0, 0),
                new Border(0, 1),
                new Border(1, 0),
                new Border(1, 1),
            }, canvas.Points());
        });
    }
}