/**
 * wrap_form.cpp — Form (top-level window) wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/form.hpp>
#include <new>

nw_form_handle nw_form_create(const char* title,
                               int x, int y, unsigned w, unsigned h) {
    try {
        auto* f = new nana::form(nana::rectangle{x, y, w, h});
        f->caption(nw_safe_str(title));
        return static_cast<nw_form_handle>(f);
    } catch (...) {
        return nullptr;
    }
}

nw_form_handle nw_form_create_centered(unsigned w, unsigned h) {
    try {
        auto* f = new nana::form(nana::API::make_center(w, h));
        return static_cast<nw_form_handle>(f);
    } catch (...) {
        return nullptr;
    }
}

void nw_form_show(nw_form_handle form) {
    if (!form) return;
    static_cast<nana::form*>(form)->show();
}

void nw_form_hide(nw_form_handle form) {
    if (!form) return;
    static_cast<nana::form*>(form)->hide();
}

void nw_form_close(nw_form_handle form) {
    if (!form) return;
    static_cast<nana::form*>(form)->close();
}

void nw_form_destroy(nw_form_handle form) {
    delete static_cast<nana::form*>(form);
}

void nw_form_set_caption(nw_form_handle form, const char* title) {
    if (!form) return;
    static_cast<nana::form*>(form)->caption(nw_safe_str(title));
}

char* nw_form_get_caption(nw_form_handle form) {
    if (!form) return nullptr;
    return nw_strdup(static_cast<nana::form*>(form)->caption());
}

void nw_form_set_size(nw_form_handle form, unsigned w, unsigned h) {
    if (!form) return;
    static_cast<nana::form*>(form)->size(nana::size{w, h});
}

void nw_form_get_size(nw_form_handle form, unsigned* w, unsigned* h) {
    if (!form || !w || !h) return;
    auto sz = static_cast<nana::form*>(form)->size();
    *w = sz.width;
    *h = sz.height;
}

void nw_form_set_pos(nw_form_handle form, int x, int y) {
    if (!form) return;
    static_cast<nana::form*>(form)->move(x, y);
}

void nw_form_get_pos(nw_form_handle form, int* x, int* y) {
    if (!form || !x || !y) return;
    auto p = static_cast<nana::form*>(form)->pos();
    *x = p.x;
    *y = p.y;
}

void nw_form_set_bgcolor(nw_form_handle form, nw_color c) {
    if (!form) return;
    static_cast<nana::form*>(form)->bgcolor(
        nana::color{static_cast<unsigned>(c.r),
                    static_cast<unsigned>(c.g),
                    static_cast<unsigned>(c.b),
                    c.a / 255.0});
}

void nw_form_set_fgcolor(nw_form_handle form, nw_color c) {
    if (!form) return;
    static_cast<nana::form*>(form)->fgcolor(
        nana::color{static_cast<unsigned>(c.r),
                    static_cast<unsigned>(c.g),
                    static_cast<unsigned>(c.b),
                    c.a / 255.0});
}

void nw_form_modality(nw_form_handle form) {
    if (!form) return;
    static_cast<nana::form*>(form)->modality();
}

void nw_form_wait_for(nw_form_handle form) {
    if (!form) return;
    static_cast<nana::form*>(form)->wait_for_this();
}
