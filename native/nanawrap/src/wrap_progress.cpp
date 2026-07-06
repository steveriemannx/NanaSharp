/**
 * wrap_progress.cpp — Progress bar widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/progress.hpp>
#include <new>

nw_progress_handle nw_progress_create(nw_form_handle parent,
                                       int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* prog = new nana::progress(*static_cast<nana::form*>(parent),
                                        nana::rectangle{x, y, w, h});
        return static_cast<nw_progress_handle>(prog);
    } catch (...) {
        return nullptr;
    }
}

void nw_progress_destroy(nw_progress_handle prog) {
    delete static_cast<nana::progress*>(prog);
}

void nw_progress_set_value(nw_progress_handle prog, unsigned val) {
    if (!prog) return;
    static_cast<nana::progress*>(prog)->value(val);
}

unsigned nw_progress_get_value(nw_progress_handle prog) {
    if (!prog) return 0;
    return static_cast<nana::progress*>(prog)->value();
}

void nw_progress_set_range(nw_progress_handle prog,
                            unsigned min_val, unsigned max_val) {
    if (!prog) return;
    (void)min_val; // Nana progress minimum is always 0
    static_cast<nana::progress*>(prog)->amount(max_val);
}
