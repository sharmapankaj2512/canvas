using System.Collections.Generic;

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
        var points = new Canvas().CreateCanvas(width, height);
        _display.Render(points);
    }
}