using MonadicCSharp;

namespace Canvas;

public class Repl
{
    private readonly ICommandSource _commandSource;
    private readonly IDisplay _display;

    public Repl(ICommandSource commandSource, IDisplay display)
    {
        _commandSource = commandSource;
        _display = display;
    }

    public void Start()
    {
        var error = Error.New("create canvas first");
        var canvas = EitherFactory.Left<Error, Canvas>(error);
        foreach (var command in _commandSource)
        {
            switch (command)
            {
                case CreateCanvas(var width, var height):
                    canvas = OnCreateCanvas(width, height);
                    break;
                case PrintCanvas:
                    OnPrintCanvas(canvas);
                    break;
                case DrawPoint(var x, var y):
                    OnDrawPoint(canvas, x, y);
                    break;
                case QuitCommand:
                    OnQuitCommand();
                    return;
            }
        }
    }

    private Either<Error, Canvas> OnCreateCanvas(int width, int height)
    {
        return Canvas.CreateCanvas(width, height)
            .ConsumeLeft(error => _display.RenderError(error.Message))
            .ConsumeRight(c => _display.Render(c.Points()));
    }

    private void OnPrintCanvas(Either<Error, Canvas> createCanvas)
    {
        createCanvas
            .ConsumeLeft(error => _display.RenderError(error.Message))
            .ConsumeRight(c => _display.Render(c.Points()));
    }

    private void OnDrawPoint(Either<Error, Canvas> canvas, int x, int y)
    {
        canvas.ConsumeRight(c =>
        {
            c.DrawPoint(Tuple.Create(x, y))
                .ConsumeNone(() => _display.Render(c.Points()))
                .ConsumeSome(e => _display.RenderError(e.Message));
        });
    }

    private void OnQuitCommand()
    {
        _display.Render("Good bye!");
    }
}