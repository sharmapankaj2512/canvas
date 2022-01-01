using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class ReplTest
{
    private Mock<ICommandSource> _commandSource;
    private Mock<IDisplay> _display;

    [SetUp]
    public void Setup()
    {
        _commandSource = new Mock<ICommandSource>();
        _display = new Mock<IDisplay>();
    }

    [Test]
    public void CreateCanvasCommand()
    {
        _commandSource.SetupSequence(c => c.MoveNext()).Returns(true).Returns(false);
        _commandSource.Setup(c => c.Current).Returns(new CreateCanvas(1, 1));
        _display.Setup(d => d.Render(new List<Point2D> {new(0, 0)}));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void InvalidCreateCanvasCommand()
    {
        _commandSource.SetupSequence(c => c.MoveNext()).Returns(true);
        _commandSource.Setup(c => c.Current).Returns(new CreateCanvas(-1, 1));
        _display.Setup(d => d.RenderError(It.IsAny<string>()));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void PrintCanvasCommand()
    {
        _commandSource.SetupSequence(c => c.MoveNext()).Returns(true).Returns(true);
        _commandSource.SetupSequence(c => c.Current)
            .Returns(new CreateCanvas(1, 1))
            .Returns(new PrintCanvas());

        _display.Setup(d => d.Render(new List<Point2D> {new(0, 0)}));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.Verify(d => d.Render(new List<Point2D> {new(0, 0)}), Times.Exactly(2));
    }

    [Test]
    public void CreateCanvasCommandAgain()
    {
        _commandSource.SetupSequence(c => c.MoveNext())
            .Returns(true).Returns(true).Returns(true);
        _commandSource.SetupSequence(c => c.Current)
            .Returns(new CreateCanvas(1, 1))
            .Returns(new CreateCanvas(0, 0))
            .Returns(new PrintCanvas());
        _display.Setup(d => d.Render(new List<Point2D>()));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.Verify(d => d.Render(new List<Point2D>()), Times.Exactly(2));
    }

    [Test]
    public void OtherCommandFiredBeforeCreateCanvasCommand()
    {
        _commandSource.SetupSequence(c => c.MoveNext()).Returns(true);
        _commandSource.Setup(c => c.Current).Returns(new PrintCanvas());
        _display.Setup(d => d.RenderError("create canvas first"));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void OtherCommandFiredAfterCanvasCreationFailed()
    {
        _commandSource.SetupSequence(c => c.MoveNext()).Returns(true).Returns(true);
        _commandSource.SetupSequence(c => c.Current)
            .Returns(new CreateCanvas(-1, 1))
            .Returns(new PrintCanvas());
        _display.Setup(d => d.RenderError(It.IsAny<string>()));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void DrawPointCommand()
    {
        _commandSource.SetupSequence(c => c.MoveNext())
            .Returns(true).Returns(true).Returns(true);
        _commandSource.SetupSequence(c => c.Current)
            .Returns(new CreateCanvas(1, 1))
            .Returns(new DrawPoint(0, 0));
        _display.Setup(d => d.Render(new List<Point2D> {new Border(0, 0)}));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }
}