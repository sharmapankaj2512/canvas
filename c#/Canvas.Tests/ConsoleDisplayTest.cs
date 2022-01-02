using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class ConsoleDisplayTest
{
    [Test]
    public void RenderCanvasBordersWhenNoPoints()
    {
        var originalConsoleOut = Console.Out;
        using (var writer = new StringWriter())
        {
            Console.SetOut(writer);
            IDisplay display = new ConsoleDisplay(writer);
            display.Render(Enumerable.Empty<Point2D>());
            Assert.AreEqual(
                new List<string>
                {
                    "xx",
                    "xx",
                }, Unlines(writer.ToString().Trim()));
        }

        Console.SetOut(originalConsoleOut);
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