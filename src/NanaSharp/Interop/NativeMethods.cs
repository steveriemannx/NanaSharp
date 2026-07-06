using System.Runtime.InteropServices;

namespace Nana.Interop;

/// <summary>
/// P/Invoke declarations for the nanawrap C library.
/// All functions map 1:1 to nanawrap.h declarations.
/// </summary>
internal static partial class NativeMethods
{
    // ── Library name resolution ──────────────────────────────────────
    // .NET runtime resolves "nanawrap" to:
    //   Linux/FreeBSD:  libnanawrap.so
    //   macOS:          libnanawrap.dylib
    //   Windows:        nanawrap.dll
    private const string LibName = "nanawrap";

    // ── Application lifecycle ────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_exec();

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_exit();

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_sleep(uint milliseconds);

    // ── Form ─────────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_form_create(
        [MarshalAs(UnmanagedType.LPStr)] string title,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_form_create_centered(uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_show(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_hide(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_close(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_destroy(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_set_caption(IntPtr form,
        [MarshalAs(UnmanagedType.LPStr)] string title);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_form_get_caption(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_set_size(IntPtr form, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_get_size(IntPtr form, out uint w, out uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_set_pos(IntPtr form, int x, int y);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_get_pos(IntPtr form, out int x, out int y);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_set_bgcolor(IntPtr form, NwColor c);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_set_fgcolor(IntPtr form, NwColor c);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_modality(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_form_wait_for(IntPtr form);

    // ── Panel ────────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_panel_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_panel_destroy(IntPtr panel);

    // ── Button ───────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_button_create(IntPtr parent,
        [MarshalAs(UnmanagedType.LPStr)] string text,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_button_destroy(IntPtr btn);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_button_set_caption(IntPtr btn,
        [MarshalAs(UnmanagedType.LPStr)] string text);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_button_get_caption(IntPtr btn);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_button_set_enabled(IntPtr btn, int enabled);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_button_get_enabled(IntPtr btn);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_button_set_pos(IntPtr btn, int x, int y);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_button_set_size(IntPtr btn, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_button_on_click(IntPtr btn,
        NwClickCallback cb, IntPtr userData);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_button_click(IntPtr btn);

    // ── Label ────────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_label_create(IntPtr parent,
        [MarshalAs(UnmanagedType.LPStr)] string text,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_label_destroy(IntPtr lbl);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_label_set_caption(IntPtr lbl,
        [MarshalAs(UnmanagedType.LPStr)] string text);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_label_get_caption(IntPtr lbl);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_label_set_text_align(IntPtr lbl, int halign, int valign);

    // ── TextBox ──────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_textbox_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_textbox_destroy(IntPtr tb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_textbox_set_text(IntPtr tb,
        [MarshalAs(UnmanagedType.LPStr)] string text);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_textbox_get_text(IntPtr tb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_textbox_set_editable(IntPtr tb, int editable);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_textbox_set_multi_line(IntPtr tb, int multi);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_textbox_append(IntPtr tb,
        [MarshalAs(UnmanagedType.LPStr)] string text);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_textbox_on_text_changed(IntPtr tb,
        NwTextChangedCallback cb, IntPtr userData);

    // ── CheckBox ─────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_checkbox_create(IntPtr parent,
        [MarshalAs(UnmanagedType.LPStr)] string text,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_checkbox_destroy(IntPtr cb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_checkbox_set_checked(IntPtr cb, int check);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_checkbox_get_checked(IntPtr cb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_checkbox_on_click(IntPtr cb,
        NwClickCallback fn, IntPtr userData);

    // ── Combox ───────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_combox_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_combox_destroy(IntPtr cbx);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_combox_push_back(IntPtr cbx,
        [MarshalAs(UnmanagedType.LPStr)] string item);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_combox_clear(IntPtr cbx);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_combox_get_count(IntPtr cbx);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_combox_set_selected(IntPtr cbx, int index);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_combox_get_selected(IntPtr cbx);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_combox_get_text(IntPtr cbx);

    // ── ListBox ──────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_listbox_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_listbox_destroy(IntPtr lb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_listbox_push_back(IntPtr lb,
        [MarshalAs(UnmanagedType.LPStr)] string item);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_listbox_clear(IntPtr lb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_listbox_get_count(IntPtr lb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_listbox_set_selected(IntPtr lb, int index);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_listbox_get_selected(IntPtr lb);

    // ── Slider ───────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_slider_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_slider_destroy(IntPtr sl);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_slider_set_range(IntPtr sl, int minVal, int maxVal);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_slider_set_value(IntPtr sl, int val);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_slider_get_value(IntPtr sl);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_slider_on_value_changed(IntPtr sl,
        NwClickCallback cb, IntPtr userData);

    // ── Progress ─────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_progress_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_progress_destroy(IntPtr prog);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_progress_set_value(IntPtr prog, uint val);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint nw_progress_get_value(IntPtr prog);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_progress_set_range(IntPtr prog, uint minVal, uint maxVal);

    // ── Place (layout) ───────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_place_create(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_place_destroy(IntPtr place);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_place_div(IntPtr place,
        [MarshalAs(UnmanagedType.LPStr)] string divText);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_place_field_put(IntPtr place,
        [MarshalAs(UnmanagedType.LPStr)] string fieldName, IntPtr widgetHandle);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_place_collocate(IntPtr place);

    // ── Menu ─────────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_menu_create();

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_menu_destroy(IntPtr menu);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_menu_append(IntPtr menu,
        [MarshalAs(UnmanagedType.LPStr)] string text,
        NwClickCallback cb, IntPtr userData);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_menu_append_separator(IntPtr menu);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_menu_popup(IntPtr menu, IntPtr form,
        int x, int y);

    // ── Menubar ──────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_menubar_create(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_menubar_destroy(IntPtr mb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_menubar_append_menu(IntPtr mb,
        [MarshalAs(UnmanagedType.LPStr)] string text);

    // ── Toolbar ──────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_toolbar_create(IntPtr form);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_toolbar_destroy(IntPtr tb);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_toolbar_append_button(IntPtr tb,
        [MarshalAs(UnmanagedType.LPStr)] string text,
        NwClickCallback cb, IntPtr userData);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_toolbar_append_separator(IntPtr tb);

    // ── TreeBox ──────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_treebox_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_treebox_destroy(IntPtr tree);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_treebox_append(IntPtr tree,
        [MarshalAs(UnmanagedType.LPStr)] string text, IntPtr parentNode);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_treebox_clear(IntPtr tree);

    // ── Picture ──────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_picture_create(IntPtr parent,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_picture_destroy(IntPtr pic);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int nw_picture_load(IntPtr pic,
        [MarshalAs(UnmanagedType.LPStr)] string filepath);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_picture_clear(IntPtr pic);

    // ── Group ────────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_group_create(IntPtr parent,
        [MarshalAs(UnmanagedType.LPStr)] string title,
        int x, int y, uint w, uint h);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_group_destroy(IntPtr grp);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_group_add_option(IntPtr grp,
        [MarshalAs(UnmanagedType.LPStr)] string text);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_group_radio_mode(IntPtr grp, int radio);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_group_on_click(IntPtr grp,
        NwClickCallback cb, IntPtr userData);

    // ── Timer ────────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr nw_timer_create(uint intervalMs,
        NwTimerCallback cb, IntPtr userData);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_timer_destroy(IntPtr timer);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_timer_start(IntPtr timer);

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_timer_stop(IntPtr timer);

    // ── Utility ──────────────────────────────────────────────────────

    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void nw_free_string(IntPtr str);

    /// <summary>
    /// Helper: call nw_free_string and return the marshalled string.
    /// </summary>
    internal static string? FreeAndGetString(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero) return null;
        string? result = Marshal.PtrToStringUTF8(ptr);
        nw_free_string(ptr);
        return result;
    }
}
