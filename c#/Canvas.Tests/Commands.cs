namespace Canvas.Tests;

public interface ICommand
{
    
}
public record CreateCanvas(int Width, int Height): ICommand;

public record PrintCanvas: ICommand;

public record DrawPoint(int X, int Y) : ICommand;