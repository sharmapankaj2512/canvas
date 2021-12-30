using LanguageExt.Common;
using NUnit.Framework;
using static LanguageExt.Prelude;

namespace Canvas.Tests;

[TestFixture]
public class InvalidCanvasTest
{
    [Test]
    public void NegativeWidth()
    {
        Assert.AreEqual(
            Left<Error, Canvas>(Error.New("Width should not be negative")),
            Canvas.CreateCanvas(-1, 0));
    }

    [Test]
    public void NegativeHeight()
    {
        Assert.AreEqual(
            Left<Error, Canvas>(Error.New("Height should not be negative")),
            Canvas.CreateCanvas(1, -1));
    }
}