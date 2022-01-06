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
        var reader = new StringReader("create 2 2\npoint 0 0\nquit\n");
        var writer = new StringWriter();
        var display = new ConsoleDisplay(writer);
        var source = new ConsoleCommandSource(reader);
        new Repl(source, display).Start();
        
        Assert.AreEqual(
            new List<string>
            {
                "xxxx",
                "x  x",
                "x  x",
                "xxxx",
                "",
                "xxxx",
                "xx x",
                "x  x",
                "xxxx",
                "",
                "Good bye!"
            }, Unlines(writer.ToString().Trim()));
    }
}