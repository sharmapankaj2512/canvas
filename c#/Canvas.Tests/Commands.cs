namespace Canvas.Tests;

public interface ICommand
{
    
}
public record CreateCanvas(int Width, int Height): ICommand;

public record PrintCanvas: ICommand;