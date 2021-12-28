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
        _display.Setup(d => d.Render(Enumerable.Empty<Point2D>()));

        new ReplController(_commandSource.Object, _display.Object).Start();
    }
    
    [Test]
    public void TestCreateOneByOneCanvas()
    {
        Assert.Fail();
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