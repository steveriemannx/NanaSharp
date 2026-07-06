using Nana.Interop;

namespace Nana;

/// <summary>A clickable button widget.</summary>
public class Button : Widget
{
    private NwClickCallback? _clickCallback;
    private EventHandler? _click;

    /// <summary>Creates a button on the given parent form.</summary>
    public Button(Form parent, string text = "Button",
                   int x = 0, int y = 0, uint w = 100, uint h = 30)
        : base(NativeMethods.nw_button_create(parent.Handle, text, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native button.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => GetNativeString(NativeMethods.nw_button_get_caption(Handle)) ?? "";
        set => NativeMethods.nw_button_set_caption(Handle, value);
    }

    /// <inheritdoc/>
    public override bool Enabled
    {
        get => NativeMethods.nw_button_get_enabled(Handle) != 0;
        set => NativeMethods.nw_button_set_enabled(Handle, value ? 1 : 0);
    }

    /// <inheritdoc/>
    public override Point Position
    {
        set => NativeMethods.nw_button_set_pos(Handle, value.X, value.Y);
    }

    /// <inheritdoc/>
    public override Size Size
    {
        set => NativeMethods.nw_button_set_size(Handle, value.Width, value.Height);
    }

    /// <summary>Fires when the button is clicked.</summary>
    public event EventHandler Click
    {
        add
        {
            _click += value;
            if (_clickCallback == null)
            {
                _clickCallback = (_, _) => _click?.Invoke(this, EventArgs.Empty);
                NativeMethods.nw_button_on_click(Handle, _clickCallback, IntPtr.Zero);
            }
        }
        remove
        {
            _click -= value;
            if (_click == null)
                _clickCallback = null;
        }
    }

    /// <summary>Programmatically triggers a click.</summary>
    public void PerformClick() => NativeMethods.nw_button_click(Handle);

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        _clickCallback = null; // prevent callback into disposed object
        NativeMethods.nw_button_destroy(Handle);
    }
}
