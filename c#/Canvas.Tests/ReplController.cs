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
        Canvas.CreateCanvas(width, height)
            .Match(Left: error => _display.RenderError(error.Message),
                Right: canvas => _display.Render(canvas.Points()));
    }
}