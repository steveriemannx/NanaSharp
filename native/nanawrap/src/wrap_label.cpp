/**
 * wrap_label.cpp — Label widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/label.hpp>
#include <new>

nw_label_handle nw_label_create(nw_form_handle parent, const char* text,
                                 int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* lbl = new nana::label(*static_cast<nana::form*>(parent),
                                    nana::rectangle{x, y, w, h});
        lbl->caption(nw_safe_str(text));
        return static_cast<nw_label_handle>(lbl);
    } catch (...) {
        return nullptr;
    }
}

void nw_label_destroy(nw_label_handle lbl) {
    delete static_cast<nana::label*>(lbl);
}

void nw_label_set_caption(nw_label_handle lbl, const char* text) {
    if (!lbl) return;
    static_cast<nana::label*>(lbl)->caption(nw_safe_str(text));
}

char* nw_label_get_caption(nw_label_handle lbl) {
    if (!lbl) return nullptr;
    return nw_strdup(static_cast<nana::label*>(lbl)->caption());
}

void nw_label_set_text_align(nw_label_handle lbl, int halign, int valign) {
    if (!lbl) return;
    nana::label* l = static_cast<nana::label*>(lbl);
    nana::align h = nana::align::left;
    nana::align_v v = nana::align_v::top;
    switch (halign) {
        case 1: h = nana::align::center; break;
        case 2: h = nana::align::right;  break;
        default: break;
    }
    switch (valign) {
        case 1: v = nana::align_v::center; break;
        case 2: v = nana::align_v::bottom; break;
        default: break;
    }
    l->text_align(h, v);
}
