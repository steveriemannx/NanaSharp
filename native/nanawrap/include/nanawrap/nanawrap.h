/**
 * nanawrap.h — C ABI wrapper for the Nana C++ GUI library
 *
 * This header defines the entire C-compatible API surface that NanaSharp
 * consumes via P/Invoke. Every Nana C++ class is exposed as an opaque
 * handle (typedef'd void*) with create/destroy pairs and property
 * getters/setters.
 *
 * Design rules:
 *  - All handles are opaque pointers; callers never dereference them.
 *  - Every create() has a matching destroy(). Destroying a parent
 *    recursively destroys children — don't double-destroy.
 *  - Strings are UTF-8 (const char*). Caller owns the buffer for getters.
 *  - Callbacks are C function pointers with a void* user_data context.
 *  - All functions return NW_OK (0) on success or a negative error code.
 *  - The wrapper is compiled as a shared library (libnanawrap).
 *
 * License: Boost Software License 1.0 (matches Nana)
 */

#ifndef NANAWRAP_H
#define NANAWRAP_H

#include <stdint.h>

#ifdef __cplusplus
extern "C" {
#endif

/* ── Error codes ───────────────────────────────────────────────────── */

#define NW_OK        0
#define NW_ERR      -1   /* generic / unknown error */
#define NW_ERR_NULL -2   /* null handle passed */
#define NW_ERR_MEM  -3   /* allocation failure */

/* ── Opaque handle typedefs ────────────────────────────────────────── */

typedef void* nw_form_handle;
typedef void* nw_panel_handle;
typedef void* nw_button_handle;
typedef void* nw_label_handle;
typedef void* nw_textbox_handle;
typedef void* nw_checkbox_handle;
typedef void* nw_combox_handle;
typedef void* nw_listbox_handle;
typedef void* nw_slider_handle;
typedef void* nw_progress_handle;
typedef void* nw_menu_handle;
typedef void* nw_menubar_handle;
typedef void* nw_toolbar_handle;
typedef void* nw_tabbar_handle;
typedef void* nw_treebox_handle;
typedef void* nw_picture_handle;
typedef void* nw_spinbox_handle;
typedef void* nw_group_handle;
typedef void* nw_place_handle;
typedef void* nw_timer_handle;

/* ── Base types ────────────────────────────────────────────────────── */

typedef struct { int x, y; }       nw_point;
typedef struct { unsigned w, h; }  nw_size;
typedef struct { int x, y; unsigned w, h; } nw_rectangle;

typedef struct { uint8_t r, g, b, a; } nw_color;

/*
 * nw_color helpers (inline-friendly C functions).
 * We also provide them as real symbols so P/Invoke can bind to them.
 */
static inline nw_color nw_color_rgba(uint8_t r, uint8_t g, uint8_t b, uint8_t a) {
    nw_color c = {r, g, b, a};
    return c;
}

/* Pre-defined colors */
#define NW_COLOR_BLACK        ((nw_color){0,0,0,255})
#define NW_COLOR_WHITE        ((nw_color){255,255,255,255})
#define NW_COLOR_RED          ((nw_color){255,0,0,255})
#define NW_COLOR_GREEN        ((nw_color){0,255,0,255})
#define NW_COLOR_BLUE         ((nw_color){0,0,255,255})
#define NW_COLOR_TRANSPARENT  ((nw_color){0,0,0,0})

/* ── Callback types ────────────────────────────────────────────────── */

/** Generic callback with user-data context. */
typedef void (*nw_callback)(void* user_data);

/** Click event: sender handle + user_data. */
typedef void (*nw_click_callback)(void* sender, void* user_data);

/** Text-changed event. */
typedef void (*nw_text_changed_callback)(void* sender, const char* new_text, void* user_data);

/** Key-press event.  key_char==0 means a non-printable key. */
typedef void (*nw_key_callback)(void* sender, int key_char, int modifiers, void* user_data);

/** Mouse event. */
typedef void (*nw_mouse_callback)(void* sender, int x, int y, int button, void* user_data);

/** Resized event. */
typedef void (*nw_size_callback)(void* sender, unsigned w, unsigned h, void* user_data);

/** Timer tick. */
typedef void (*nw_timer_callback)(void* user_data);

/* ── Application lifecycle ─────────────────────────────────────────── */

int  nw_exec(void);
void nw_exit(void);
void nw_sleep(unsigned milliseconds);

/* ── Form (top-level window) ───────────────────────────────────────── */

nw_form_handle nw_form_create(const char* title,
                              int x, int y, unsigned w, unsigned h);
nw_form_handle nw_form_create_centered(unsigned w, unsigned h);

void nw_form_show(nw_form_handle form);
void nw_form_hide(nw_form_handle form);
void nw_form_close(nw_form_handle form);
void nw_form_destroy(nw_form_handle form);

void nw_form_set_caption(nw_form_handle form, const char* title);
/* caller must free the returned string with nw_free_string() */
char* nw_form_get_caption(nw_form_handle form);

void nw_form_set_size(nw_form_handle form, unsigned w, unsigned h);
void nw_form_get_size(nw_form_handle form, unsigned* w, unsigned* h);
void nw_form_set_pos(nw_form_handle form, int x, int y);
void nw_form_get_pos(nw_form_handle form, int* x, int* y);

void nw_form_set_bgcolor(nw_form_handle form, nw_color c);
void nw_form_set_fgcolor(nw_form_handle form, nw_color c);

void nw_form_modality(nw_form_handle form);
void nw_form_wait_for(nw_form_handle form);

/* ── Panel ─────────────────────────────────────────────────────────── */

nw_panel_handle nw_panel_create(nw_form_handle parent,
                                int x, int y, unsigned w, unsigned h);
void nw_panel_destroy(nw_panel_handle panel);

/* ── Button ─────────────────────────────────────────────────────────── */

nw_button_handle nw_button_create(nw_form_handle parent,
                                  const char* text,
                                  int x, int y, unsigned w, unsigned h);
nw_button_handle nw_button_create_str(const char* text); /* auto-place */
void nw_button_destroy(nw_button_handle btn);

void nw_button_set_caption(nw_button_handle btn, const char* text);
char* nw_button_get_caption(nw_button_handle btn);
void nw_button_set_enabled(nw_button_handle btn, int enabled);
int  nw_button_get_enabled(nw_button_handle btn);
void nw_button_set_pos(nw_button_handle btn, int x, int y);
void nw_button_set_size(nw_button_handle btn, unsigned w, unsigned h);

/** Register a click callback. Only one callback per button. */
void nw_button_on_click(nw_button_handle btn,
                        nw_click_callback cb, void* user_data);
void nw_button_click(nw_button_handle btn); /* programmatic click */

/* ── Label ──────────────────────────────────────────────────────────── */

nw_label_handle nw_label_create(nw_form_handle parent, const char* text,
                                int x, int y, unsigned w, unsigned h);
void nw_label_destroy(nw_label_handle lbl);

void nw_label_set_caption(nw_label_handle lbl, const char* text);
char* nw_label_get_caption(nw_label_handle lbl);
void nw_label_set_text_align(nw_label_handle lbl, int halign, int valign);
/* halign: 0=left, 1=center, 2=right;  valign: 0=top, 1=center, 2=bottom */

/* ── TextBox ────────────────────────────────────────────────────────── */

nw_textbox_handle nw_textbox_create(nw_form_handle parent,
                                    int x, int y, unsigned w, unsigned h);
void nw_textbox_destroy(nw_textbox_handle tb);

void nw_textbox_set_text(nw_textbox_handle tb, const char* text);
char* nw_textbox_get_text(nw_textbox_handle tb);
void nw_textbox_set_editable(nw_textbox_handle tb, int editable);
void nw_textbox_set_multi_line(nw_textbox_handle tb, int multi);
void nw_textbox_append(nw_textbox_handle tb, const char* text);

void nw_textbox_on_text_changed(nw_textbox_handle tb,
                                nw_text_changed_callback cb, void* user_data);

/* ── CheckBox ───────────────────────────────────────────────────────── */

nw_checkbox_handle nw_checkbox_create(nw_form_handle parent,
                                      const char* text,
                                      int x, int y, unsigned w, unsigned h);
void nw_checkbox_destroy(nw_checkbox_handle cb);
void nw_checkbox_set_checked(nw_checkbox_handle cb, int checked);
int  nw_checkbox_get_checked(nw_checkbox_handle cb);
void nw_checkbox_on_click(nw_checkbox_handle cb,
                          nw_click_callback fn, void* user_data);

/* ── Combox (dropdown) ──────────────────────────────────────────────── */

nw_combox_handle nw_combox_create(nw_form_handle parent,
                                  int x, int y, unsigned w, unsigned h);
void nw_combox_destroy(nw_combox_handle cbx);
void nw_combox_push_back(nw_combox_handle cbx, const char* item);
void nw_combox_clear(nw_combox_handle cbx);
int  nw_combox_get_count(nw_combox_handle cbx);
void nw_combox_set_selected(nw_combox_handle cbx, int index);
int  nw_combox_get_selected(nw_combox_handle cbx);
char* nw_combox_get_text(nw_combox_handle cbx);

/* ── ListBox ────────────────────────────────────────────────────────── */

nw_listbox_handle nw_listbox_create(nw_form_handle parent,
                                    int x, int y, unsigned w, unsigned h);
void nw_listbox_destroy(nw_listbox_handle lb);
void nw_listbox_push_back(nw_listbox_handle lb, const char* item);
void nw_listbox_clear(nw_listbox_handle lb);
int  nw_listbox_get_count(nw_listbox_handle lb);
void nw_listbox_set_selected(nw_listbox_handle lb, int index);
int  nw_listbox_get_selected(nw_listbox_handle lb);

/* ── Slider ─────────────────────────────────────────────────────────── */

nw_slider_handle nw_slider_create(nw_form_handle parent,
                                  int x, int y, unsigned w, unsigned h);
void nw_slider_destroy(nw_slider_handle sl);
void nw_slider_set_range(nw_slider_handle sl, int min_val, int max_val);
void nw_slider_set_value(nw_slider_handle sl, int val);
int  nw_slider_get_value(nw_slider_handle sl);
void nw_slider_on_value_changed(nw_slider_handle sl,
                                nw_click_callback cb, void* user_data);

/* ── Progress ───────────────────────────────────────────────────────── */

nw_progress_handle nw_progress_create(nw_form_handle parent,
                                      int x, int y, unsigned w, unsigned h);
void nw_progress_destroy(nw_progress_handle prog);
void nw_progress_set_value(nw_progress_handle prog, unsigned val);
unsigned nw_progress_get_value(nw_progress_handle prog);
void nw_progress_set_range(nw_progress_handle prog,
                           unsigned min_val, unsigned max_val);

/* ── Place (layout manager) ─────────────────────────────────────────── */

nw_place_handle nw_place_create(nw_form_handle form);
void nw_place_destroy(nw_place_handle place);

void nw_place_div(nw_place_handle place, const char* div_text);
/** field_name as in e.g. "vertical <label> | 70% <actions>" */
void nw_place_field_put(nw_place_handle place,
                        const char* field_name,
                        void* widget_handle);
void nw_place_collocate(nw_place_handle place);

/* ── Menu ───────────────────────────────────────────────────────────── */

nw_menu_handle nw_menu_create(void);
void nw_menu_destroy(nw_menu_handle menu);
void nw_menu_append(nw_menu_handle menu, const char* text,
                    nw_click_callback cb, void* user_data);
void nw_menu_append_separator(nw_menu_handle menu);
void nw_menu_popup(nw_menu_handle menu, nw_form_handle form, int x, int y);

nw_menubar_handle nw_menubar_create(nw_form_handle form);
void nw_menubar_destroy(nw_menubar_handle mb);
nw_menu_handle nw_menubar_append_menu(nw_menubar_handle mb, const char* text);

/* ── Toolbar ────────────────────────────────────────────────────────── */

nw_toolbar_handle nw_toolbar_create(nw_form_handle form);
void nw_toolbar_destroy(nw_toolbar_handle tb);
void nw_toolbar_append_button(nw_toolbar_handle tb, const char* text,
                              nw_click_callback cb, void* user_data);
void nw_toolbar_append_separator(nw_toolbar_handle tb);

/* ── TreeBox ────────────────────────────────────────────────────────── */

nw_treebox_handle nw_treebox_create(nw_form_handle parent,
                                    int x, int y, unsigned w, unsigned h);
void nw_treebox_destroy(nw_treebox_handle tree);
/* Returns an opaque node handle (void*); use it to append children. */
void* nw_treebox_append(nw_treebox_handle tree,
                        const char* text, void* parent_node);
void nw_treebox_clear(nw_treebox_handle tree);

/* ── Picture (image display) ────────────────────────────────────────── */

nw_picture_handle nw_picture_create(nw_form_handle parent,
                                    int x, int y, unsigned w, unsigned h);
void nw_picture_destroy(nw_picture_handle pic);
int  nw_picture_load(nw_picture_handle pic, const char* filepath);
void nw_picture_clear(nw_picture_handle pic);

/* ── Group (radio group) ────────────────────────────────────────────── */

nw_group_handle nw_group_create(nw_form_handle parent, const char* title,
                                int x, int y, unsigned w, unsigned h);
void nw_group_destroy(nw_group_handle grp);
void nw_group_add_option(nw_group_handle grp, const char* text);
void nw_group_radio_mode(nw_group_handle grp, int radio);
void nw_group_on_click(nw_group_handle grp,
                       nw_click_callback cb, void* user_data);

/* ── Timer ──────────────────────────────────────────────────────────── */

nw_timer_handle nw_timer_create(unsigned interval_ms,
                                nw_timer_callback cb, void* user_data);
void nw_timer_destroy(nw_timer_handle timer);
void nw_timer_start(nw_timer_handle timer);
void nw_timer_stop(nw_timer_handle timer);

/* ── Utility ────────────────────────────────────────────────────────── */

/** Free a string returned by any nw_*_get_* function. */
void nw_free_string(char* str);

/** Show a message box. Returns 0=OK, 1=Yes, 2=No, 3=Cancel. */
int  nw_msgbox(nw_form_handle owner, const char* title, const char* message,
               int flags /* 0=ok, 1=yes_no, 2=yes_no_cancel */);

/* ── File dialog ───────────────────────────────────────────────────── */

/** Returns the selected file path or NULL. Caller must nw_free_string(). */
char* nw_file_open_dialog(nw_form_handle owner, const char* title,
                          const char* filters /* e.g. "*.txt;*.cpp" */);
char* nw_file_save_dialog(nw_form_handle owner, const char* title,
                          const char* filters);

#ifdef __cplusplus
}
#endif

#endif /* NANAWRAP_H */
