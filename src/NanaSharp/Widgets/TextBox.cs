using Nana.Interop;

namespace Nana;

/// <summary>A single-line or multi-line text input widget.</summary>
public class TextBox : Widget
{
    private NwTextChangedCallback? _textChangedCallback;

    /// <summary>Creates a textbox on the given parent form.</summary>
    public TextBox(Form parent, int x = 0, int y = 0, uint w = 200, uint h = 30)
        : base(NativeMethods.nw_textbox_create(parent.Handle, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native textbox.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => GetNativeString(NativeMethods.nw_textbox_get_text(Handle)) ?? "";
        set => NativeMethods.nw_textbox_set_text(Handle, value);
    }

    /// <summary>Gets or sets the text content. Alias for Caption.</summary>
    public string Text
    {
        get => Caption;
        set => Caption = value;
    }

    /// <summary>Gets or sets whether the textbox is editable.</summary>
    public bool Editable
    {
        set => NativeMethods.nw_textbox_set_editable(Handle, value ? 1 : 0);
    }

    /// <summary>Gets or sets whether the textbox supports multiple lines.</summary>
    public bool MultiLine
    {
        set => NativeMethods.nw_textbox_set_multi_line(Handle, value ? 1 : 0);
    }

    /// <summary>Appends text to the current content.</summary>
    public void Append(string text) => NativeMethods.nw_textbox_append(Handle, text);

    /// <summary>Fires when the text changes.</summary>
    public event EventHandler? TextChanged
    {
        add
        {
            _textChanged += value;
            if (_textChangedCallback == null)
            {
                _textChangedCallback = (_, _, _) => _textChanged?.Invoke(this, EventArgs.Empty);
                NativeMethods.nw_textbox_on_text_changed(Handle, _textChangedCallback,
                    IntPtr.Zero);
            }
        }
        remove
        {
            _textChanged -= value;
            if (_textChanged == null)
                _textChangedCallback = null;
        }
    }
    private EventHandler? _textChanged;

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        _textChangedCallback = null;
        NativeMethods.nw_textbox_destroy(Handle);
    }
}
