using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class InvalidCanvasTest
{
    [Test]
    public void NegativeWidth()
    {
        Assert.AreEqual(
            Either<Error, Canvas>.MakeLeft(Error.New("Width should not be negative")),
            Canvas.CreateCanvas(-1, 0));
    }

    [Test]
    public void NegativeHeight()
    {
        Assert.AreEqual(
            Either<Error, Canvas>.MakeLeft(Error.New("Height should not be negative")),
            Canvas.CreateCanvas(1, -1));
    }

    [Test]
    public void WidhtExceedsLimit()
    {
        Assert.AreEqual(
            Either<Error, Canvas>.MakeLeft(Error.New("Width exceeds limit")),
            Canvas.CreateCanvas(51, 3));
    }

    [Test]
    public void HeightExceedsLimit()
    {
        Assert.AreEqual(
            Either<Error, Canvas>.MakeLeft(Error.New("Height exceeds limit")),
            Canvas.CreateCanvas(3, 51));
    }
}