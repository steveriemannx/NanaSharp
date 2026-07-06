/**
 * wrap_picture.cpp — Picture widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/picture.hpp>
#include <new>

nw_picture_handle nw_picture_create(nw_form_handle parent,
                                     int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* pic = new nana::picture(*static_cast<nana::form*>(parent),
                                      nana::rectangle{x, y, w, h});
        return static_cast<nw_picture_handle>(pic);
    } catch (...) {
        return nullptr;
    }
}

void nw_picture_destroy(nw_picture_handle pic) {
    delete static_cast<nana::picture*>(pic);
}

int nw_picture_load(nw_picture_handle pic, const char* filepath) {
    if (!pic || !filepath) return 0;
    try {
        static_cast<nana::picture*>(pic)->load(nana::paint::image(filepath));
        return 1;
    } catch (...) {
        return 0;
    }
}

void nw_picture_clear(nw_picture_handle pic) {
    if (!pic) return;
    try {
        static_cast<nana::picture*>(pic)->load(nana::paint::image{});
    } catch (...) { }
}
