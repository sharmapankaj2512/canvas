using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

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

    public List<Point2D> Points()
    {
        return Enumerable.Range(0, Width)
            .Zip(Enumerable.Range(0, Height))
            .Select(p => new Point2D(p.Item1, p.Item2))
            .ToList();
    }

    public static Either<Error, Canvas> CreateCanvas(int width, int height)
    {
        if (width < 0)
            return Left(Error.New("Width should not be negative"));
        if (height < 0)
            return Left(Error.New("Height should not be negative"));
        if (width > 50)
            return Left(Error.New("Width exceeds limit"));
        if (height > 50)
            return Left(Error.New("Height exceeds limit"));
        return Right(new Canvas(width, height));
    }
}