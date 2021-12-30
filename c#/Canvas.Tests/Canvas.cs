using System.Collections.Generic;
using System.Linq;

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
        return Enumerable.ToList(from x in Enumerable.Range(0, Width)
                                 from y in Enumerable.Range(0, Height)
                                 select new Point2D(x, y));
    }

    public static Either<Error, Canvas> CreateCanvas(int width, int height)
    {
        if (width < 0)
            return Either<Error, Canvas>.MakeLeft(Error.New("Width should not be negative"));
        if (height < 0)
            return Either<Error, Canvas>.MakeLeft(Error.New("Height should not be negative"));
        if (width > 50)
            return Either<Error, Canvas>.MakeLeft(Error.New("Width exceeds limit"));
        if (height > 50)
            return Either<Error, Canvas>.MakeLeft(Error.New("Height exceeds limit"));
        return Either<Error, Canvas>.MakeRight(new Canvas(width, height));
    }
}