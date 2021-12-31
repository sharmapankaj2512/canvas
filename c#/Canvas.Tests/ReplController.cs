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
        Either<Error, Canvas>? canvas = null;
        while (_commandSource.MoveNext())
        {
            switch (_commandSource.Current)
            {
                case CreateCanvas(var width, var height):
                    canvas = OnCreateCanvas(width, height);
                    break;
                case PrintCanvas:
                    OnPrintCanvas(canvas);
                    break;
            }
        }
    }

    private Either<Error, Canvas> OnCreateCanvas(int width, int height)
    {
        return Canvas.CreateCanvas(width, height)
            .ConsumeLeft(error => _display.RenderError(error.Message))
            .ConsumeRight(c => _display.Render(c.Points()));
    }

    private void OnPrintCanvas(Either<Error, Canvas>? canvas)
    {
        if (canvas == null)
        {
            _display.RenderError("create canvas first");
        }
        else
        {
            canvas.ConsumeLeft(error => _display.RenderError(error.Message))
                .ConsumeRight(c => _display.Render(c.Points()));
        }
    }
}