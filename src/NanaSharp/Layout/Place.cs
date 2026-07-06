using Nana.Interop;

namespace Nana;

/// <summary>
/// Layout manager for Nana forms. Uses a div-text mini-language to define
/// the layout structure, then widgets are placed into named fields.
/// </summary>
public class Place : IDisposable
{
    private readonly IntPtr _handle;
    private bool _disposed;

    /// <summary>Creates a Place layout for the given form.</summary>
    public Place(Form form)
    {
        _handle = NativeMethods.nw_place_create(form.Handle);
        if (_handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native place layout.");
    }

    /// <summary>
    /// Sets the div layout string. E.g.:
    /// "vertical &lt;label margin=10&gt; | 70% &lt;actions&gt;"
    /// </summary>
    public void Div(string divText)
    {
        NativeMethods.nw_place_div(_handle, divText);
    }

    /// <summary>Places a widget into a named field defined in the div string.</summary>
    public void Put(string fieldName, Widget widget)
    {
        NativeMethods.nw_place_field_put(_handle, fieldName, widget.Handle);
    }

    /// <summary>Collocates (resolves) the layout. Call after all Put() calls.</summary>
    public void Collocate()
    {
        NativeMethods.nw_place_collocate(_handle);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            NativeMethods.nw_place_destroy(_handle);
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }

    ~Place() => Dispose();
}
