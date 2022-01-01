using System;
using System.Collections.Generic;
using System.Linq;
using static Canvas.Tests.EitherFactory;

namespace Canvas.Tests;

class Canvas
{
    private Tuple<int, int> _border;
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
            select MakePoint(x, y)).ToList();
    }

    private Point2D MakePoint(int x, int y)
    {
        if (this._border != null && _border.Item1 == x && _border.Item2 == y)
        {
            return new Border(x, y);
        }
        return new Point2D(x, y);
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

    public void DrawPoint(Tuple<int, int> border)
    {
        this._border = border;
    }
}