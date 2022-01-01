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
        FakeCommandSource(new CreateCanvas(1, 1));
        _display.Setup(d => d.Render(new List<Point2D> {new(0, 0)}));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void InvalidCreateCanvasCommand()
    {
        FakeCommandSource(new CreateCanvas(-1, 1));
        _display.Setup(d => d.RenderError(It.IsAny<string>()));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void PrintCanvasCommand()
    {
        FakeCommandSource(
            new CreateCanvas(1, 1),
            new PrintCanvas());
        _display.Setup(d => d.Render(new List<Point2D> {new(0, 0)}));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.Verify(d => d.Render(new List<Point2D> {new(0, 0)}), Times.Exactly(2));
    }

    [Test]
    public void CreateCanvasCommandAgain()
    {
        FakeCommandSource(
            new CreateCanvas(1, 1), 
            new CreateCanvas(0, 0), 
            new PrintCanvas());
        _display.Setup(d => d.Render(new List<Point2D>()));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.Verify(d => d.Render(new List<Point2D>()), Times.Exactly(2));
    }

    [Test]
    public void OtherCommandFiredBeforeCreateCanvasCommand()
    {
        FakeCommandSource(new PrintCanvas());
        _display.Setup(d => d.RenderError("create canvas first"));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void OtherCommandFiredAfterCanvasCreationFailed()
    {
        FakeCommandSource(
            new CreateCanvas(-1, 1), 
            new PrintCanvas());
        _display.Setup(d => d.RenderError(It.IsAny<string>()));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void DrawPointCommand()
    {
        FakeCommandSource(
            new CreateCanvas(1, 1), 
            new DrawPoint(0, 0));
        _display.Setup(d => d.Render(new List<Point2D> {new Border(0, 0)}));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    [Test]
    public void InvalidDrawPointCommand()
    {
        FakeCommandSource(new CreateCanvas(1, 1), new DrawPoint(-1, 0));
        _display.Setup(d => d.RenderError(It.IsAny<string>()));

        new Repl(_commandSource.Object, _display.Object).Start();

        _display.VerifyAll();
    }

    private void FakeCommandSource(params ICommand[] commands)
    {
        var moveNext = _commandSource.SetupSequence(c => c.MoveNext());
        var current = _commandSource.SetupSequence(c => c.Current);
        foreach (var command in commands)
        {
            moveNext.Returns(true);
            current.Returns(command);
        }
    }
}