using Canvas;
using Canvas.Tests;

var source = new ConsoleCommandSource(Console.In);
var display = new ConsoleDisplay(Console.Out);
var repl = new Repl(source, display);
repl.Start();