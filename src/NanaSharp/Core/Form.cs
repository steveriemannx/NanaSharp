using Nana.Interop;

namespace Nana;

/// <summary>
/// Represents a top-level window (form).
/// This is the main entry point for creating Nana GUI applications.
/// </summary>
public class Form : Widget
{
    /// <summary>Creates a centered form with the given size and title.</summary>
    public Form(string title = "Nana Window", uint width = 300, uint height = 200)
        : base(NativeMethods.nw_form_create_centered(width, height))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native form.");
        Caption = title;
    }

    /// <summary>Creates a form at a specific position and size.</summary>
    public Form(Rectangle rect, string title = "Nana Window")
        : base(NativeMethods.nw_form_create(title, rect.X, rect.Y, rect.Width, rect.Height))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native form.");
    }

    // ── Caption ───────────────────────────────────────────────────

    /// <inheritdoc/>
    public override string Caption
    {
        get => GetNativeString(NativeMethods.nw_form_get_caption(Handle)) ?? "";
        set => NativeMethods.nw_form_set_caption(Handle, value);
    }

    // ── Size ──────────────────────────────────────────────────────

    /// <inheritdoc/>
    public override Size Size
    {
        get
        {
            NativeMethods.nw_form_get_size(Handle, out uint w, out uint h);
            return new Size(w, h);
        }
        set => NativeMethods.nw_form_set_size(Handle, value.Width, value.Height);
    }

    // ── Position ──────────────────────────────────────────────────

    /// <inheritdoc/>
    public override Point Position
    {
        get
        {
            NativeMethods.nw_form_get_pos(Handle, out int x, out int y);
            return new Point(x, y);
        }
        set => NativeMethods.nw_form_set_pos(Handle, value.X, value.Y);
    }

    // ── Colors ────────────────────────────────────────────────────

    /// <inheritdoc/>
    public override Color BackgroundColor
    {
        set => NativeMethods.nw_form_set_bgcolor(Handle, value.ToNative());
    }

    /// <inheritdoc/>
    public override Color ForegroundColor
    {
        set => NativeMethods.nw_form_set_fgcolor(Handle, value.ToNative());
    }

    // ── Window controls ───────────────────────────────────────────

    /// <inheritdoc/>
    public override void Show() => NativeMethods.nw_form_show(Handle);

    /// <inheritdoc/>
    public override void Hide() => NativeMethods.nw_form_hide(Handle);

    /// <inheritdoc/>
    public override void Close() => NativeMethods.nw_form_close(Handle);

    /// <summary>Blocks execution until this form is closed (modal).</summary>
    public void ShowModal() => NativeMethods.nw_form_modality(Handle);

    /// <summary>Blocks execution until this form is closed.</summary>
    public void Wait() => NativeMethods.nw_form_wait_for(Handle);

    // ── Cleanup ──────────────────────────────────────────────────

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        NativeMethods.nw_form_destroy(Handle);
    }
}
