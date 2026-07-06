/**
 * wrap_group.cpp — Group (radio group) widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/group.hpp>
#include <new>

nw_group_handle nw_group_create(nw_form_handle parent, const char* title,
                                 int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* grp = new nana::group(*static_cast<nana::form*>(parent),
                                    nana::rectangle{x, y, w, h});
        grp->caption(nw_safe_str(title));
        return static_cast<nw_group_handle>(grp);
    } catch (...) {
        return nullptr;
    }
}

void nw_group_destroy(nw_group_handle grp) {
    delete static_cast<nana::group*>(grp);
}

void nw_group_add_option(nw_group_handle grp, const char* text) {
    if (!grp) return;
    static_cast<nana::group*>(grp)->add_option(nw_safe_str(text));
}

void nw_group_radio_mode(nw_group_handle grp, int radio) {
    if (!grp) return;
    static_cast<nana::group*>(grp)->radio_mode(radio != 0);
}

void nw_group_on_click(nw_group_handle grp,
                        nw_click_callback cb, void* user_data) {
    if (!grp) return;
    nana::group* g = static_cast<nana::group*>(grp);
    nw_events_store_click(grp, cb, user_data);
    g->events().click([grp]() {
        auto ctx = nw_events_get_click(grp);
        if (ctx.fn) ctx.fn(grp, ctx.user_data);
    });
}
