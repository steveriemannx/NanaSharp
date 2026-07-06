using Nana.Interop;

namespace Nana;

/// <summary>A panel container widget for grouping child widgets.</summary>
public class Panel : Widget
{
    /// <summary>Creates a panel on the given parent form.</summary>
    public Panel(Form parent, int x = 0, int y = 0, uint w = 200, uint h = 200)
        : base(NativeMethods.nw_panel_create(parent.Handle, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native panel.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => "";
        set { }
    }

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        NativeMethods.nw_panel_destroy(Handle);
    }
}
