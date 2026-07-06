using Nana.Interop;

namespace Nana;

/// <summary>A horizontal or vertical slider widget.</summary>
public class Slider : Widget
{
    private NwClickCallback? _valueChangedCallback;
    private EventHandler? _valueChanged;

    /// <summary>Creates a slider on the given parent form.</summary>
    public Slider(Form parent, int x = 0, int y = 0, uint w = 200, uint h = 30)
        : base(NativeMethods.nw_slider_create(parent.Handle, x, y, w, h))
    {
        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create native slider.");
    }

    /// <inheritdoc/>
    public override string Caption
    {
        get => "";
        set { }
    }

    /// <summary>Gets or sets the current value.</summary>
    public int Value
    {
        get => NativeMethods.nw_slider_get_value(Handle);
        set => NativeMethods.nw_slider_set_value(Handle, value);
    }

    /// <summary>Sets the min/max range.</summary>
    public void SetRange(int min, int max)
        => NativeMethods.nw_slider_set_range(Handle, min, max);

    /// <summary>Fires when the slider value changes.</summary>
    public event EventHandler ValueChanged
    {
        add
        {
            _valueChanged += value;
            if (_valueChangedCallback == null)
            {
                _valueChangedCallback = (_, _) => _valueChanged?.Invoke(this, EventArgs.Empty);
                NativeMethods.nw_slider_on_value_changed(Handle, _valueChangedCallback, IntPtr.Zero);
            }
        }
        remove
        {
            _valueChanged -= value;
            if (_valueChanged == null) _valueChangedCallback = null;
        }
    }

    /// <inheritdoc/>
    protected override void DestroyNativeHandle()
    {
        _valueChangedCallback = null;
        NativeMethods.nw_slider_destroy(Handle);
    }
}
