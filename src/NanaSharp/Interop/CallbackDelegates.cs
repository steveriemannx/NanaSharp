using System.Runtime.InteropServices;

namespace Nana.Interop;

/// <summary>
/// Delegate types matching nanawrap callback typedefs.
/// These must have the same calling convention and signature as the C side.
/// </summary>

/// <summary>nw_click_callback: void (*)(void* sender, void* user_data)</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void NwClickCallback(IntPtr sender, IntPtr userData);

/// <summary>nw_text_changed_callback: void (*)(void* sender, const char* new_text, void* user_data)</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void NwTextChangedCallback(IntPtr sender, IntPtr newText, IntPtr userData);

/// <summary>nw_timer_callback: void (*)(void* user_data)</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void NwTimerCallback(IntPtr userData);

/// <summary>nw_key_callback: void (*)(void* sender, int key_char, int modifiers, void* user_data)</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void NwKeyCallback(IntPtr sender, int keyChar, int modifiers, IntPtr userData);

/// <summary>nw_mouse_callback: void (*)(void* sender, int x, int y, int button, void* user_data)</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void NwMouseCallback(IntPtr sender, int x, int y, int button, IntPtr userData);

/// <summary>nw_size_callback: void (*)(void* sender, unsigned w, unsigned h, void* user_data)</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void NwSizeCallback(IntPtr sender, uint w, uint h, IntPtr userData);
