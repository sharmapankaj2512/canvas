using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class DisplayTest
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
    public void RenderEmptyPoints()
    {
        MakeDisplay().Render(Enumerable.Empty<Point2D>());
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
        MakeDisplay().Render(new List<Point2D> {new(0, 0)});
        Assert.AreEqual(
            new List<string>
            {
                "xxx",
                "x x",
                "xxx",
            }, Unlines(_writer.ToString().Trim()));
    }

    [Test]
    public void RenderSingleBorderPoint()
    {
        MakeDisplay().Render(new List<Point2D> {new Border(0, 0)});
        Assert.AreEqual(
            new List<string>
            {
                "xxx",
                "xxx",
                "xxx",
            }, Unlines(_writer.ToString().Trim()));
    }

    private IDisplay MakeDisplay()
    {
        return new ConsoleDisplay(_writer);
    }

    private static List<string> Unlines(string text)
    {
        return text.Split(Environment.NewLine)
            .Select(line => line.Trim())
            .ToList();
    }
}