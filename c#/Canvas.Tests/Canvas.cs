using System.Collections.Generic;
using System.Linq;
using static Canvas.Tests.EitherFactory;

namespace Canvas.Tests;

class Canvas
{
    private int Width { get; }
    private int Height { get; }

    private Canvas(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public IEnumerable<Point2D> Points()
    {
        return (from x in Enumerable.Range(0, Width)
            from y in Enumerable.Range(0, Height)
            select new Point2D(x, y)).ToList();
    }

    public static Either<Error, Canvas> CreateCanvas(int width, int height)
    {
        if (width < 0)
            return Left<Error, Canvas>(Error.New("Width should not be negative"));
        if (height < 0)
            return Left<Error, Canvas>(Error.New("Height should not be negative"));
        if (width > 50)
            return Left<Error, Canvas>(Error.New("Width exceeds limit"));
        if (height > 50)
            return Left<Error, Canvas>(Error.New("Height exceeds limit"));
        return Right<Error, Canvas>(new Canvas(width, height));
    }
}