namespace Canvas;

public interface IDisplay
{
    void Render(IEnumerable<Point2D> points);
    void RenderError(string message);
}