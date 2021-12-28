using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class ReplControllerTest
{
    [Test]
    public void TestCreateEmptyCanvas()
    {
        var commandSource = new Mock<ICommandSource>();
        var display = new Mock<IDisplay>();

        commandSource.Setup(c => c.MoveNext()).Returns(true);
        commandSource.Setup(c => c.Current).Returns(new CreateCanvas(0, 0));
        display.Setup(d => d.Render(Enumerable.Empty<Point2D>()));

        var repl = new ReplController(commandSource.Object, display.Object);
        repl.Start();

        commandSource.VerifyAll();
        display.VerifyAll();
    }

    public class CreateCanvas
    {
        public CreateCanvas(int width, int height)
        {
        }
    }
}

public class ReplController
{
    private readonly ICommandSource _commandSource;
    private readonly IDisplay _display;

    public ReplController(ICommandSource commandSource, IDisplay display)
    {
        _commandSource = commandSource;
        _display = display;
    }

    public void Start()
    {
        _commandSource.MoveNext();
        var command = _commandSource.Current;
        _display.Render(Enumerable.Empty<Point2D>());
    }
}

public interface IDisplay
{
    void Render(IEnumerable<Point2D> points);
}

public interface ICommandSource : IEnumerator, IEnumerable
{
    bool IEnumerator.MoveNext()
    {
        return true;
    }
}

public class Point2D
{
}