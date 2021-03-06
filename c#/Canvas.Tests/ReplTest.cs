using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class ReplTest
{
    private Mock<ICommandSource> _commandSource = null!;
    private Mock<IDisplay> _display = null!;

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

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderPoints(new List<Point2D> {new(0, 0)});
    }

    [Test]
    public void InvalidCreateCanvasCommand()
    {
        FakeCommandSource(new CreateCanvas(-1, 1));

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderError();
    }

    [Test]
    public void PrintCanvasCommand()
    {
        FakeCommandSource(
            new CreateCanvas(1, 1),
            new PrintCanvas());

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderPoints(new List<Point2D> {new(0, 0)}, 2);
    }

    [Test]
    public void CreateCanvasCommandAgain()
    {
        FakeCommandSource(
            new CreateCanvas(1, 1),
            new CreateCanvas(0, 0),
            new PrintCanvas());

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderPoints(new List<Point2D>(), 2);
    }

    [Test]
    public void OtherCommandFiredBeforeCreateCanvasCommand()
    {
        FakeCommandSource(new PrintCanvas());

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderError("create canvas first");
    }

    [Test]
    public void OtherCommandFiredAfterCanvasCreationFailed()
    {
        FakeCommandSource(
            new CreateCanvas(-1, 1),
            new PrintCanvas());

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderError();
    }

    [Test]
    public void DrawPointCommand()
    {
        FakeCommandSource(
            new CreateCanvas(1, 1),
            new DrawPoint(0, 0));

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderPoints(new List<Point2D> {new Border(0, 0)});
    }

    [Test]
    public void QuitCommand()
    {
        FakeCommandSource(
            new QuitCommand(),
            new CreateCanvas(1, 1),
            new PrintCanvas());

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderMessage("Good bye!");
    }

    [Test]
    public void InvalidDrawPointCommand()
    {
        FakeCommandSource(new CreateCanvas(1, 1), new DrawPoint(-1, 0));

        new Repl(_commandSource.Object, _display.Object).Start();

        AssertExpectRenderError();
    }

    private void FakeCommandSource(params ICommand[] commands)
    {
        _commandSource.Setup(d => d.GetEnumerator())
            .Returns(commands.ToList().GetEnumerator());
    }

    private void AssertExpectRenderPoints(List<Point2D> points, int times = 1)
    {
        _display.Verify(d => d.Render(points), Times.Exactly(times));
    }

    private void AssertExpectRenderError()
    {
        _display.Verify(d => d.RenderError(It.IsAny<string>()));
    }

    private void AssertExpectRenderError(string message)
    {
        _display.Verify(d => d.RenderError(message));
    }

    private void AssertExpectRenderMessage(string message)
    {
        _display.Verify(d => d.Render(message));
    }
}