using Nana.Interop;

namespace Nana;

/// <summary>A progress bar widget.</summary>
public class ProgressBar : Widget
{
    /// <summary>Creates a progress bar on the given parent form.</summary>
    public ProgressBar(Form parent, int x = 0, int y = 0, uint w = 200, uint h = 24)
        : base(NativeMethods.nw_progress_create(parent.Handle, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native progress bar.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => "";
        set { }
    }

    /// <summary>Gets or sets the current value.</summary>
    public uint Value
    {
        get => NativeMethods.nw_progress_get_value(Handle);
        set => NativeMethods.nw_progress_set_value(Handle, value);
    }

    /// <summary>Sets the min/max range.</summary>
    public void SetRange(uint min, uint max)
        => NativeMethods.nw_progress_set_range(Handle, min, max);

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        NativeMethods.nw_progress_destroy(Handle);
    }
}
