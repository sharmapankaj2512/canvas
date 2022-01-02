using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class ConsoleDisplayTest
{
    private TextWriter _originalConsoleOut = null!;
    private StringWriter _writer = null!;

    [SetUp]
    public void Setup()
    {
        _writer = new StringWriter();
        _originalConsoleOut = Console.Out;
        Console.SetOut(_writer);
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetOut(_originalConsoleOut);
    }

    [Test]
    public void RenderCanvasBordersWhenNoPoints()
    {
        IDisplay display = new ConsoleDisplay(_writer);
        display.Render(Enumerable.Empty<Point2D>());

        Assert.AreEqual(
            new List<string>
            {
                "xx",
                "xx",
            }, Unlines(_writer.ToString().Trim()));
    }

    [Test]
    public void RenderSinglePoint()
    {
        IDisplay display = new ConsoleDisplay(_writer);
        display.Render(new List<Point2D> {new Border(0, 0)});

        Assert.AreEqual(
            new List<string>
            {
                "xxx",
                "x x",
                "xxx",
            }, Unlines(_writer.ToString().Trim()));
    }

    private static List<string> Unlines(string text)
    {
        return text.Split("\n")
            .Select(line => line.Trim())
            .ToList();
    }
}

public class ConsoleDisplay : IDisplay
{
    private readonly TextWriter _writer;

    public ConsoleDisplay(TextWriter writer)
    {
        _writer = writer;
    }

    public void Render(IEnumerable<Point2D> points)
    {
        _writer.WriteLine("xx\nxx");
    }

    public void RenderError(string message)
    {
        throw new NotImplementedException();
    }
}