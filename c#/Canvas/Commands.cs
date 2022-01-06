namespace Canvas;

public interface ICommand
{
    public static ICommand MakeCommand(string rawCommand)
    {
        if (rawCommand.StartsWith("create"))
        {
            var tokens = rawCommand.Split(" ");
            return new CreateCanvas(int.Parse(tokens[1]), int.Parse(tokens[2]));
        }

        if (rawCommand.StartsWith("point"))
        {
            var tokens = rawCommand.Split(" ");
            return new DrawPoint(int.Parse(tokens[1]), int.Parse(tokens[2]));
        }

        return new PrintCanvas();
    }
}

public record CreateCanvas(int Width, int Height) : ICommand;

public record PrintCanvas : ICommand;

public record DrawPoint(int X, int Y) : ICommand;

public record QuitCommand : ICommand;