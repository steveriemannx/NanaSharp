namespace Nana;

/// <summary>Represents a 2D point.</summary>
public readonly struct Point : IEquatable<Point>
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }

    public static readonly Point Zero = new(0, 0);

    public Point Offset(int dx, int dy) => new(X + dx, Y + dy);

    internal Interop.NwPoint ToNative() => new(X, Y);
    internal static Point FromNative(Interop.NwPoint p) => new(p.X, p.Y);

    public bool Equals(Point other) => X == other.X && Y == other.Y;
    public override bool Equals(object? obj) => obj is Point other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public static bool operator ==(Point a, Point b) => a.Equals(b);
    public static bool operator !=(Point a, Point b) => !a.Equals(b);
    public override string ToString() => $"({X}, {Y})";
}
