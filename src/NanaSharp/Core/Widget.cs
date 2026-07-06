using Nana.Interop;

namespace Nana;

/// <summary>
/// Abstract base class for all Nana widgets.
/// Wraps an opaque native handle and provides common properties.
/// Implements IDisposable — destroying a parent destroys all children.
/// </summary>
public abstract class Widget : IDisposable
{
    /// <summary>The native opaque handle (nw_*_handle).</summary>
    internal IntPtr Handle { get; private set; }

    /// <summary>Is the native handle still valid?</summary>
    public bool IsDisposed => Handle == IntPtr.Zero;

    /// <summary>Protected constructor for derived classes.</summary>
    protected Widget(IntPtr handle)
    {
        Handle = handle;
    }

    // ── Common properties ──────────────────────────────────────────

    /// <summary>Gets or sets the widget's caption (text).</summary>
    public abstract string Caption { get; set; }

    /// <summary>Gets or sets whether the widget is enabled.</summary>
    public virtual bool Enabled
    {
        get => true;
        set { }
    }

    /// <summary>Gets or sets the widget's position.</summary>
    public virtual Point Position
    {
        get => Point.Zero;
        set { }
    }

    /// <summary>Gets or sets the widget's size.</summary>
    public virtual Size Size
    {
        get => Size.Zero;
        set { }
    }

    /// <summary>Gets or sets the foreground color.</summary>
    public virtual Color ForegroundColor
    {
        get => Color.Black;
        set { }
    }

    /// <summary>Gets or sets the background color.</summary>
    public virtual Color BackgroundColor
    {
        get => Color.White;
        set { }
    }

    /// <summary>Shows the widget.</summary>
    public virtual void Show() { }

    /// <summary>Hides the widget.</summary>
    public virtual void Hide() { }

    /// <summary>Closes and destroys only this widget.</summary>
    public virtual void Close() { }

    // ── IDisposable ─────────────────────────────────────────────────

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Handle != IntPtr.Zero)
        {
            DestroyNativeHandle();
            Handle = IntPtr.Zero;
        }
    }

    /// <summary>
    /// Override to call the specific nw_*_destroy function.
    /// </summary>
    protected abstract void DestroyNativeHandle();

    ~Widget()
    {
        Dispose(false);
    }

    // ── Internal helpers for derived classes ────────────────────────

    /// <summary>Helper to read a string from native code and free it.</summary>
    protected static string? GetNativeString(IntPtr ptr)
        => NativeMethods.FreeAndGetString(ptr);
}
