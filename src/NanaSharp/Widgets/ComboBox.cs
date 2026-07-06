using Nana.Interop;

namespace Nana;

/// <summary>A dropdown combobox widget.</summary>
public class ComboBox : Widget
{
    /// <summary>Creates a combobox on the given parent form.</summary>
    public ComboBox(Form parent, int x = 0, int y = 0, uint w = 200, uint h = 30)
        : base(NativeMethods.nw_combox_create(parent.Handle, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native combobox.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => GetNativeString(NativeMethods.nw_combox_get_text(Handle)) ?? "";
        set { } // combox doesn't have a simple set_caption
    }

    /// <summary>Gets the selected text.</summary>
    public string Text => Caption;

    /// <summary>Adds an item to the dropdown list.</summary>
    public void Add(string item) => NativeMethods.nw_combox_push_back(Handle, item);

    /// <summary>Clears all items.</summary>
    public void Clear() => NativeMethods.nw_combox_clear(Handle);

    /// <summary>Gets the number of items.</summary>
    public int Count => NativeMethods.nw_combox_get_count(Handle);

    /// <summary>Gets or sets the selected index (-1 if none).</summary>
    public int SelectedIndex
    {
        get => NativeMethods.nw_combox_get_selected(Handle);
        set => NativeMethods.nw_combox_set_selected(Handle, value);
    }

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        NativeMethods.nw_combox_destroy(Handle);
    }
}
