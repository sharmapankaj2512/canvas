using System.Collections.Generic;
using System.IO;
using Canvas.Tests;
using NUnit.Framework;
using static Canvas.StringExtensions;

namespace Canvas.Acceptance.Tests;

[TestFixture]
public class CanvasApplicationTest
{
    [Test]
    public void ConsoleApplicationTest()
    {
        var reader = new StringReader("create 2 2\npoint 0 0\n");
        var writer = new StringWriter();
        var display = new ConsoleDisplay(writer);
        var source = new ConsoleCommandSource(reader);
        var repl = new Repl(source, display);
        repl.Start();
        
        Assert.AreEqual(
            new List<string>
            {
                "xxxx",
                "xx x",
                "x  x",
                "xxxx",
            }, Unlines(writer.ToString().Trim()));
    }
}