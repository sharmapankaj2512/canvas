using System;
using System.Collections.Generic;
using System.Linq;
using MonadicCSharp;
using static MonadicCSharp.EitherFactory;
using static MonadicCSharp.MaybeFactory;

namespace Canvas.Tests;

class Canvas
{
    private readonly HashSet<Border> _borders;
    private int Width { get; }
    private int Height { get; }

    private Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        _borders = new HashSet<Border>();
    }

    public IEnumerable<Point2D> Points()
    {
        return (from x in Enumerable.Range(0, Width)
            from y in Enumerable.Range(0, Height)
            select MakePoint(x, y)).ToList();
    }

    private Point2D MakePoint(int x, int y)
    {
        return _borders.Contains(new Border(x, y))
            ? new Border(x, y)
            : new Point2D(x, y);
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

    public Maybe<Error> DrawPoint(Tuple<int, int> border)
    {
        if (border.Item1 < 0
            || border.Item1 >= Width
            || border.Item2 < 0
            || border.Item2 >= Height)
            return Some(Error.New("point outside canvas"));
        _borders.Add(new Border(border.Item1, border.Item2));
        return None<Error>();
    }
}