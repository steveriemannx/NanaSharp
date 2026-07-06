/**
 * wrap_menu.cpp — Menu / Menubar widget wrappers
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/menu.hpp>
#include <nana/gui/widgets/menubar.hpp>
#include <new>

/* ── Menu ──────────────────────────────────────────────────────────── */

nw_menu_handle nw_menu_create(void) {
    try {
        auto* m = new nana::menu;
        return static_cast<nw_menu_handle>(m);
    } catch (...) {
        return nullptr;
    }
}

void nw_menu_destroy(nw_menu_handle menu) {
    delete static_cast<nana::menu*>(menu);
}

void nw_menu_append(nw_menu_handle menu, const char* text,
                     nw_click_callback cb, void* user_data) {
    if (!menu) return;
    nana::menu* m = static_cast<nana::menu*>(menu);
    nw_events_store_click(menu, cb, user_data);
    m->append(nw_safe_str(text), [menu](nana::menu::item_proxy&) {
        auto ctx = nw_events_get_click(menu);
        if (ctx.fn) ctx.fn(menu, ctx.user_data);
    });
}

void nw_menu_append_separator(nw_menu_handle menu) {
    // Nana menu does not have a native separator — no-op
    (void)menu;
}

void nw_menu_popup(nw_menu_handle menu, nw_form_handle form, int x, int y) {
    if (!menu || !form) return;
    static_cast<nana::menu*>(menu)->popup(*static_cast<nana::form*>(form), x, y);
}

/* ── Menubar ────────────────────────────────────────────────────────── */

nw_menubar_handle nw_menubar_create(nw_form_handle form) {
    if (!form) return nullptr;
    try {
        auto* mb = new nana::menubar(*static_cast<nana::form*>(form));
        return static_cast<nw_menubar_handle>(mb);
    } catch (...) {
        return nullptr;
    }
}

void nw_menubar_destroy(nw_menubar_handle mb) {
    delete static_cast<nana::menubar*>(mb);
}

nw_menu_handle nw_menubar_append_menu(nw_menubar_handle mb, const char* text) {
    if (!mb) return nullptr;
    auto* m = new nana::menu;
    static_cast<nana::menubar*>(mb)->push_back(nw_safe_str(text));
    return static_cast<nw_menu_handle>(m);
}
