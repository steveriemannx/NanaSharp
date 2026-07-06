/**
 * wrap_slider.cpp — Slider widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/slider.hpp>
#include <new>

nw_slider_handle nw_slider_create(nw_form_handle parent,
                                   int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* sl = new nana::slider(*static_cast<nana::form*>(parent),
                                    nana::rectangle{x, y, w, h});
        return static_cast<nw_slider_handle>(sl);
    } catch (...) {
        return nullptr;
    }
}

void nw_slider_destroy(nw_slider_handle sl) {
    delete static_cast<nana::slider*>(sl);
}

void nw_slider_set_range(nw_slider_handle sl, int min_val, int max_val) {
    if (!sl) return;
    (void)min_val; // Nana slider minimum is always 0
    static_cast<nana::slider*>(sl)->maximum(static_cast<unsigned>(max_val));
}

void nw_slider_set_value(nw_slider_handle sl, int val) {
    if (!sl) return;
    static_cast<nana::slider*>(sl)->value(val);
}

int nw_slider_get_value(nw_slider_handle sl) {
    if (!sl) return 0;
    return static_cast<int>(static_cast<nana::slider*>(sl)->value());
}

void nw_slider_on_value_changed(nw_slider_handle sl,
                                 nw_click_callback cb, void* user_data) {
    if (!sl) return;
    nana::slider* s = static_cast<nana::slider*>(sl);
    nw_events_store_click(sl, cb, user_data);
    s->events().value_changed([sl](const nana::arg_slider&) {
        auto ctx = nw_events_get_click(sl);
        if (ctx.fn) ctx.fn(sl, ctx.user_data);
    });
}
