/**
 * wrap_checkbox.cpp — CheckBox widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/checkbox.hpp>
#include <new>

nw_checkbox_handle nw_checkbox_create(nw_form_handle parent,
                                       const char* text,
                                       int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* cb = new nana::checkbox(*static_cast<nana::form*>(parent),
                                      nana::rectangle{x, y, w, h});
        cb->caption(nw_safe_str(text));
        return static_cast<nw_checkbox_handle>(cb);
    } catch (...) {
        return nullptr;
    }
}

void nw_checkbox_destroy(nw_checkbox_handle cb) {
    delete static_cast<nana::checkbox*>(cb);
}

void nw_checkbox_set_checked(nw_checkbox_handle cb, int checked) {
    if (!cb) return;
    static_cast<nana::checkbox*>(cb)->check(checked != 0);
}

int nw_checkbox_get_checked(nw_checkbox_handle cb) {
    if (!cb) return 0;
    return static_cast<nana::checkbox*>(cb)->checked() ? 1 : 0;
}

void nw_checkbox_on_click(nw_checkbox_handle cb,
                          nw_click_callback fn, void* user_data) {
    if (!cb) return;
    nana::checkbox* c = static_cast<nana::checkbox*>(cb);
    nw_events_store_click(cb, fn, user_data);
    c->events().click([cb](const nana::arg_click&) {
        auto ctx = nw_events_get_click(cb);
        if (ctx.fn) ctx.fn(cb, ctx.user_data);
    });
}
