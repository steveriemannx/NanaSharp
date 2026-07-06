using Nana.Interop;

namespace Nana;

/// <summary>A static text label widget.</summary>
public class Label : Widget
{
    /// <summary>Creates a label on the given parent form.</summary>
    public Label(Form parent, string text = "Label",
                  int x = 0, int y = 0, uint w = 200, uint h = 24)
        : base(NativeMethods.nw_label_create(parent.Handle, text, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native label.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => GetNativeString(NativeMethods.nw_label_get_caption(Handle)) ?? "";
        set => NativeMethods.nw_label_set_caption(Handle, value);
    }

    /// <summary>Sets the text alignment. Use 0=left, 1=center, 2=right etc.</summary>
    public void SetTextAlign(int horizontal, int vertical)
        => NativeMethods.nw_label_set_text_align(Handle, horizontal, vertical);

    /// <summary>Centers the text.</summary>
    public void CenterText() => SetTextAlign(1, 1);

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        NativeMethods.nw_label_destroy(Handle);
    }
}
