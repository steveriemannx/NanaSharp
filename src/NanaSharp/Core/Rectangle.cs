namespace Nana;

/// <summary>Represents a rectangle (position + size).</summary>
public readonly struct Rectangle : IEquatable<Rectangle>
{
    public int X { get; }
    public int Y { get; }
    public uint Width { get; }
    public uint Height { get; }

    public Rectangle(int x, int y, uint width, uint height)
    {
        X = x; Y = y; Width = width; Height = height;
    }

    public Rectangle(Point pos, Size size)
        : this(pos.X, pos.Y, size.Width, size.Height) { }

    public Point Position => new(X, Y);
    public Size Size => new(Width, Height);

    public static Rectangle Centered(uint w, uint h) => new(0, 0, w, h);

    internal Interop.NwRectangle ToNative() => new(X, Y, Width, Height);

    public bool Equals(Rectangle other) =>
        X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
    public override bool Equals(object? obj) => obj is Rectangle other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);
    public static bool operator ==(Rectangle a, Rectangle b) => a.Equals(b);
    public static bool operator !=(Rectangle a, Rectangle b) => !a.Equals(b);
    public override string ToString() => $"({X},{Y} {Width}x{Height})";
}
