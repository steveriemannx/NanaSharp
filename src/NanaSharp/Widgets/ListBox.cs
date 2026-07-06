using Nana.Interop;

namespace Nana;

/// <summary>A listbox widget.</summary>
public class ListBox : Widget
{
    /// <summary>Creates a listbox on the given parent form.</summary>
    public ListBox(Form parent, int x = 0, int y = 0, uint w = 200, uint h = 150)
        : base(NativeMethods.nw_listbox_create(parent.Handle, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native listbox.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => "";
        set { }
    }

    /// <summary>Adds an item to the list.</summary>
    public void Add(string item) => NativeMethods.nw_listbox_push_back(Handle, item);

    /// <summary>Clears all items.</summary>
    public void Clear() => NativeMethods.nw_listbox_clear(Handle);

    /// <summary>Gets the number of items.</summary>
    public int Count => NativeMethods.nw_listbox_get_count(Handle);

    /// <summary>Gets or sets the selected index.</summary>
    public int SelectedIndex
    {
        get => NativeMethods.nw_listbox_get_selected(Handle);
        set => NativeMethods.nw_listbox_set_selected(Handle, value);
    }

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        NativeMethods.nw_listbox_destroy(Handle);
    }
}
