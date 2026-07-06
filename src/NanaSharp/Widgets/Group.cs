using Nana.Interop;

namespace Nana;

/// <summary>A group widget (typically used for radio button groups).</summary>
public class Group : Widget
{
    private NwClickCallback? _clickCallback;
    private EventHandler? _click;

    /// <summary>Creates a group on the given parent form.</summary>
    public Group(Form parent, string title = "Group",
                  int x = 0, int y = 0, uint w = 200, uint h = 100)
        : base(NativeMethods.nw_group_create(parent.Handle, title, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native group.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => "";
        set => NativeMethods.nw_button_set_caption(Handle, value);
    }

    /// <summary>Adds an option (radio/checkbox) to the group.</summary>
    public void AddOption(string text) => NativeMethods.nw_group_add_option(Handle, text);

    /// <summary>Enables or disables radio mode (only one option selectable).</summary>
    public bool RadioMode
    {
        set => NativeMethods.nw_group_radio_mode(Handle, value ? 1 : 0);
    }

    /// <summary>Fires when the group is clicked.</summary>
    public event EventHandler Click
    {
        add
        {
            _click += value;
            if (_clickCallback == null)
            {
                _clickCallback = (_, _) => _click?.Invoke(this, EventArgs.Empty);
                NativeMethods.nw_group_on_click(Handle, _clickCallback, IntPtr.Zero);
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
        NativeMethods.nw_group_destroy(Handle);
    }
}
