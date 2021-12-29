using System.Collections.Generic;
using System.Linq;

namespace Canvas.Tests;

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
        var (width, height) = _commandSource.Current;
        var points = CreateCanvas(width, height);
        _display.Render(points);
    }

    private static IEnumerable<Point2D> CreateCanvas(int width, int height)
    {
        return Enumerable.Range(0, width)
            .Zip(Enumerable.Range(0, height))
            .Select(p => new Point2D(p.First, p.Second))
            .ToList();
    }
}