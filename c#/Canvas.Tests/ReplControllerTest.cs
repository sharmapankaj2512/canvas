using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public void CreateEmptyCanvas()
    {
        _commandSource.Setup(c => c.MoveNext()).Returns(true);
        _commandSource.Setup(c => c.Current).Returns(new CreateCanvas(0, 0));
        _display.Setup(d => d.Render(new List<Point2D>()));

        new ReplController(_commandSource.Object, _display.Object).Start();
    }

    [Test]
    public void TestCreateOneByOneCanvas()
    {
        _commandSource.Setup(c => c.MoveNext()).Returns(true);
        _commandSource.Setup(c => c.Current).Returns(new CreateCanvas(1, 1));
        _display.Setup(d => d.Render(new List<Point2D> {new(0, 0)}));

        new ReplController(_commandSource.Object, _display.Object).Start();
    }
}

public record CreateCanvas(int Width, int Height);

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
        var points = new List<Point2D>();
        for (int x = 0; x < command.Width; x++)
        {
            for (int y = 0; y < command.Height; y++)
            {
                points.Add(new Point2D(x, y));
            }
        }

        _display.Render(points);
    }
}

public interface IDisplay
{
    void Render(IEnumerable<Point2D> points);
}

public interface ICommandSource : IEnumerator<CreateCanvas>, IEnumerable<CreateCanvas>
{
    bool IEnumerator.MoveNext()
    {
        return true;
    }
}

public record Point2D
{
    public Point2D(int x, int y)
    {
    }
}