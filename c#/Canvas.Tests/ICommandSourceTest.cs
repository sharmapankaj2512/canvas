using System.IO;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class CommandSourceTest
{
    [Test]
    public void CreateCanvasCommand()
    {
        var reader = new StringReader("create 0 0");
        var source = new ConsoleCommandSource(reader);

        Assert.AreEqual(true, source.MoveNext());
        Assert.AreEqual(new CreateCanvas(0, 0), source.Current);
    }

    [Test]
    public void PrintCanvasCommand()
    {
        var reader = new StringReader("create 0 0\nprint\n");
        var source = new ConsoleCommandSource(reader);
        var _ = source.Current; // ignore create

        Assert.AreEqual(true, source.MoveNext());
        Assert.AreEqual(new PrintCanvas(), source.Current);
    }

    [Test]
    public void DrawPointCommand()
    {
        var reader = new StringReader("create 1 1\npoint 0 0\n");
        var source = new ConsoleCommandSource(reader);
        var _ = source.Current; // ignore create

        Assert.AreEqual(true, source.MoveNext());
        Assert.AreEqual(new DrawPoint(0, 0), source.Current);
    }
}