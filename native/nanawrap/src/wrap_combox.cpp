/**
 * wrap_combox.cpp — Combox (dropdown) widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/combox.hpp>
#include <new>

nw_combox_handle nw_combox_create(nw_form_handle parent,
                                   int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* cbx = new nana::combox(*static_cast<nana::form*>(parent),
                                     nana::rectangle{x, y, w, h});
        return static_cast<nw_combox_handle>(cbx);
    } catch (...) {
        return nullptr;
    }
}

void nw_combox_destroy(nw_combox_handle cbx) {
    delete static_cast<nana::combox*>(cbx);
}

void nw_combox_push_back(nw_combox_handle cbx, const char* item) {
    if (!cbx) return;
    static_cast<nana::combox*>(cbx)->push_back(nw_safe_str(item));
}

void nw_combox_clear(nw_combox_handle cbx) {
    if (!cbx) return;
    static_cast<nana::combox*>(cbx)->clear();
}

int nw_combox_get_count(nw_combox_handle cbx) {
    if (!cbx) return 0;
    return static_cast<int>(static_cast<nana::combox*>(cbx)->the_number_of_options());
}

void nw_combox_set_selected(nw_combox_handle cbx, int index) {
    if (!cbx) return;
    static_cast<nana::combox*>(cbx)->option(index);
}

int nw_combox_get_selected(nw_combox_handle cbx) {
    if (!cbx) return -1;
    auto n = static_cast<nana::combox*>(cbx)->option();
    if (n == nana::npos) return -1;
    return static_cast<int>(n);
}

char* nw_combox_get_text(nw_combox_handle cbx) {
    if (!cbx) return nullptr;
    return nw_strdup(static_cast<nana::combox*>(cbx)->caption());
}
