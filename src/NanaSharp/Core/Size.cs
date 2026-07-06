namespace Nana;

/// <summary>Represents a 2D size (width, height).</summary>
public readonly struct Size : IEquatable<Size>
{
    public uint Width { get; }
    public uint Height { get; }

    public Size(uint width, uint height) { Width = width; Height = height; }
    public Size(int width, int height) : this((uint)width, (uint)height) { }

    public static readonly Size Zero = new(0, 0);
    public static readonly Size Default = new(300, 200);

    internal Interop.NwSize ToNative() => new(Width, Height);
    internal static Size FromNative(Interop.NwSize s) => new(s.W, s.H);

    public bool Equals(Size other) => Width == other.Width && Height == other.Height;
    public override bool Equals(object? obj) => obj is Size other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Width, Height);
    public static bool operator ==(Size a, Size b) => a.Equals(b);
    public static bool operator !=(Size a, Size b) => !a.Equals(b);
    public override string ToString() => $"{Width}x{Height}";
}
