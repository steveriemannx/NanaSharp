/**
 * wrap_button.cpp — Button widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/button.hpp>
#include <new>

nw_button_handle nw_button_create(nw_form_handle parent,
                                   const char* text,
                                   int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* btn = new nana::button(*static_cast<nana::form*>(parent),
                                     nana::rectangle{x, y, w, h});
        btn->caption(nw_safe_str(text));
        return static_cast<nw_button_handle>(btn);
    } catch (...) {
        return nullptr;
    }
}

nw_button_handle nw_button_create_str(const char* text) {
    try {
        auto* btn = new nana::button();
        btn->caption(nw_safe_str(text));
        return static_cast<nw_button_handle>(btn);
    } catch (...) {
        return nullptr;
    }
}

void nw_button_destroy(nw_button_handle btn) {
    delete static_cast<nana::button*>(btn);
}

void nw_button_set_caption(nw_button_handle btn, const char* text) {
    if (!btn) return;
    static_cast<nana::button*>(btn)->caption(nw_safe_str(text));
}

char* nw_button_get_caption(nw_button_handle btn) {
    if (!btn) return nullptr;
    return nw_strdup(static_cast<nana::button*>(btn)->caption());
}

void nw_button_set_enabled(nw_button_handle btn, int enabled) {
    if (!btn) return;
    static_cast<nana::button*>(btn)->enabled(enabled != 0);
}

int nw_button_get_enabled(nw_button_handle btn) {
    if (!btn) return 0;
    return static_cast<nana::button*>(btn)->enabled() ? 1 : 0;
}

void nw_button_set_pos(nw_button_handle btn, int x, int y) {
    if (!btn) return;
    static_cast<nana::button*>(btn)->move(x, y);
}

void nw_button_set_size(nw_button_handle btn, unsigned w, unsigned h) {
    if (!btn) return;
    static_cast<nana::button*>(btn)->size(nana::size{w, h});
}

void nw_button_on_click(nw_button_handle btn,
                         nw_click_callback cb, void* user_data) {
    if (!btn) return;
    nana::button* b = static_cast<nana::button*>(btn);
    nw_events_store_click(btn, cb, user_data);

    b->events().click([btn](const nana::arg_click&) {
        auto ctx = nw_events_get_click(btn);
        if (ctx.fn) {
            ctx.fn(btn, ctx.user_data);
        }
    });
}

void nw_button_click(nw_button_handle btn) {
    if (!btn) return;
    auto ctx = nw_events_get_click(btn);
    if (ctx.fn) {
        ctx.fn(btn, ctx.user_data);
    }
}
