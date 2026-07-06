/**
 * wrap_listbox.cpp — ListBox widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/listbox.hpp>
#include <new>

nw_listbox_handle nw_listbox_create(nw_form_handle parent,
                                     int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* lb = new nana::listbox(*static_cast<nana::form*>(parent),
                                     nana::rectangle{x, y, w, h});
        return static_cast<nw_listbox_handle>(lb);
    } catch (...) {
        return nullptr;
    }
}

void nw_listbox_destroy(nw_listbox_handle lb) {
    delete static_cast<nana::listbox*>(lb);
}

void nw_listbox_push_back(nw_listbox_handle lb, const char* item) {
    if (!lb) return;
    static_cast<nana::listbox*>(lb)->at(0).append(nw_safe_str(item));
}

void nw_listbox_clear(nw_listbox_handle lb) {
    if (!lb) return;
    static_cast<nana::listbox*>(lb)->clear();
}

int nw_listbox_get_count(nw_listbox_handle lb) {
    if (!lb) return 0;
    return static_cast<int>(static_cast<nana::listbox*>(lb)->at(0).size());
}

void nw_listbox_set_selected(nw_listbox_handle lb, int index) {
    if (!lb) return;
    auto* l = static_cast<nana::listbox*>(lb);
    if (index >= 0 && index < static_cast<int>(l->at(0).size())) {
        // Nana listbox selection is item-based
        auto items = l->at(0);
        // Select by index via the catalog
        l->at(0).at(index).select(true);
    }
}

int nw_listbox_get_selected(nw_listbox_handle lb) {
    if (!lb) return -1;
    auto* l = static_cast<nana::listbox*>(lb);
    auto selected = l->selected();
    if (selected.empty()) return -1;
    return static_cast<int>(selected[0].item);
}
