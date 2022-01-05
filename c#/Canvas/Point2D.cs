namespace Canvas;

public record Point2D(int X, int Y);

public record Border(int X, int Y) : Point2D(X, Y);