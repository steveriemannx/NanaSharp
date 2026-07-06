using Nana.Interop;

namespace Nana;

/// <summary>A picture/image display widget.</summary>
public class Picture : Widget
{
    /// <summary>Creates a picture widget on the given parent form.</summary>
    public Picture(Form parent, int x = 0, int y = 0, uint w = 200, uint h = 200)
        : base(NativeMethods.nw_picture_create(parent.Handle, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native picture widget.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => "";
        set { }
    }

    /// <summary>Loads an image from a file path. Returns true on success.</summary>
    public bool Load(string filepath)
        => NativeMethods.nw_picture_load(Handle, filepath) != 0;

    /// <summary>Clears the displayed image.</summary>
    public void Clear() => NativeMethods.nw_picture_clear(Handle);

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        NativeMethods.nw_picture_destroy(Handle);
    }
}
