using Nana.Interop;

namespace Nana;

/// <summary>A checkbox widget.</summary>
public class CheckBox : Widget
{
    private NwClickCallback? _clickCallback;
    private EventHandler? _click;

    /// <summary>Creates a checkbox on the given parent form.</summary>
    public CheckBox(Form parent, string text = "CheckBox",
                     int x = 0, int y = 0, uint w = 200, uint h = 24)
        : base(NativeMethods.nw_checkbox_create(parent.Handle, text, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native checkbox.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => ""; // checkbox get_caption not exposed in simplified API
        set => NativeMethods.nw_button_set_caption(Handle, value);
    }

    /// <summary>Gets or sets the checked state.</summary>
    public bool Checked
    {
        get => NativeMethods.nw_checkbox_get_checked(Handle) != 0;
        set => NativeMethods.nw_checkbox_set_checked(Handle, value ? 1 : 0);
    }

    /// <summary>Fires when the checkbox is clicked.</summary>
    public event EventHandler Click
    {
        add
        {
            _click += value;
            if (_clickCallback == null)
            {
                _clickCallback = (_, _) => _click?.Invoke(this, EventArgs.Empty);
                NativeMethods.nw_checkbox_on_click(Handle, _clickCallback, IntPtr.Zero);
            }
        }
        remove
        {
            _click -= value;
            if (_click == null) _clickCallback = null;
        }
    }

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        _clickCallback = null;
        NativeMethods.nw_checkbox_destroy(Handle);
    }
}
