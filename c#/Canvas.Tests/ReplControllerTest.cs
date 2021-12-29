using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class ReplControllerTest
{
    private Mock<ICommandSource> _commandSource;
    private Mock<IDisplay> _display;

    [SetUp]
    public void Setup()
    {
        _commandSource = new Mock<ICommandSource>();
        _display = new Mock<IDisplay>();
    }

    [TearDown]
    public void TearDown()
    {
        _commandSource.VerifyAll();
        _display.VerifyAll();
    }

    [Test]
    public void CreateCanvasCommand()
    {
        _commandSource.Setup(c => c.MoveNext()).Returns(true);
        _commandSource.Setup(c => c.Current).Returns(new CreateCanvas(1, 1));
        _display.Setup(d => d.Render(new List<Point2D> {new(0, 0)}));

        new ReplController(_commandSource.Object, _display.Object).Start();
    }

    [Test]
    public void NegativeWidthCreateCanvasCommand()
    {
        _commandSource.Setup(c => c.MoveNext()).Returns(true);
        _commandSource.Setup(c => c.Current).Returns(new CreateCanvas(-1, 1));
        _display.Setup(d => d.RenderError("Width should not be negative"));
        
        new ReplController(_commandSource.Object, _display.Object).Start();
    }
}