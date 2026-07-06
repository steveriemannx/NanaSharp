/**
 * wrap_panel.cpp — Panel widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/panel.hpp>
#include <new>

nw_panel_handle nw_panel_create(nw_form_handle parent,
                                 int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* pnl = new nana::panel<true>(*static_cast<nana::form*>(parent),
                                          nana::rectangle{x, y, w, h});
        return static_cast<nw_panel_handle>(pnl);
    } catch (...) {
        return nullptr;
    }
}

void nw_panel_destroy(nw_panel_handle panel) {
    delete static_cast<nana::panel<true>*>(panel);
}
