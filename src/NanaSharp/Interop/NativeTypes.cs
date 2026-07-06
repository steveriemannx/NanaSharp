using System.Runtime.InteropServices;

namespace Nana.Interop;

/// <summary>Maps to nw_point { int x, y }</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct NwPoint
{
    public int X;
    public int Y;

    public NwPoint(int x, int y) { X = x; Y = y; }
    public override string ToString() => $"({X}, {Y})";
}

/// <summary>Maps to nw_size { unsigned w, h }</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct NwSize
{
    public uint W;
    public uint H;

    public NwSize(uint w, uint h) { W = w; H = h; }
    public override string ToString() => $"{W}x{H}";
}

/// <summary>Maps to nw_rectangle { int x, y; unsigned w, h }</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct NwRectangle
{
    public int X;
    public int Y;
    public uint W;
    public uint H;

    public NwRectangle(int x, int y, uint w, uint h)
    {
        X = x; Y = y; W = w; H = h;
    }
    public override string ToString() => $"({X},{Y} {W}x{H})";
}

/// <summary>Maps to nw_color { uint8_t r, g, b, a }</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct NwColor
{
    public byte R;
    public byte G;
    public byte B;
    public byte A;

    public NwColor(byte r, byte g, byte b, byte a = 255)
    {
        R = r; G = g; B = b; A = a;
    }

    public static readonly NwColor Black       = new(0, 0, 0);
    public static readonly NwColor White       = new(255, 255, 255);
    public static readonly NwColor Red         = new(255, 0, 0);
    public static readonly NwColor Green       = new(0, 255, 0);
    public static readonly NwColor Blue        = new(0, 0, 255);
    public static readonly NwColor Transparent = new(0, 0, 0, 0);

    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}{(A != 255 ? A.ToString("X2") : "")}";
}
