namespace Canvas;

public class ConsoleDisplay : IDisplay
{
    private readonly TextWriter _writer;

    public ConsoleDisplay(TextWriter writer)
    {
        _writer = writer;
    }

    public void RenderError(string message)
    {
        _writer.WriteLine(message);
    }

    public void Render(IEnumerable<Point2D> points)
    {
        var ps = points.ToHashSet();
        _writer.WriteLine(string.Join("",
            BorderRow(ps),
            Environment.NewLine,
            PaintRows(ps),
            Empty(ps) ? "" : Environment.NewLine,
            BorderRow(ps),
            Environment.NewLine));
    }

    private static bool Empty(HashSet<Point2D> ps)
    {
        return ps.Count == 0;
    }

    private string BorderRow(HashSet<Point2D> points)
    {
        var rowLength = points.Count(p => p.X == 0) + 2;
        return new string('x', rowLength);
    }

    private string PaintRows(HashSet<Point2D> points)
    {
        return string.Join(
            Environment.NewLine,
            points.GroupBy(p => p.X).Select(PaintRow));
    }

    private string PaintRow(IGrouping<int, Point2D> row)
    {
        string PaintPoints() => string.Join("", row.Select(PaintPoint));

        string PaintPoint(Point2D point) => point switch
        {
            Border => "x",
            _ => " "
        };

        return "x" + PaintPoints() + "x";
    }
}