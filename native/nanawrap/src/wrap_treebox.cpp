/**
 * wrap_treebox.cpp — TreeBox widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/treebox.hpp>
#include <new>
#include <string>

nw_treebox_handle nw_treebox_create(nw_form_handle parent,
                                     int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* tree = new nana::treebox(*static_cast<nana::form*>(parent),
                                       nana::rectangle{x, y, w, h});
        return static_cast<nw_treebox_handle>(tree);
    } catch (...) {
        return nullptr;
    }
}

void nw_treebox_destroy(nw_treebox_handle tree) {
    delete static_cast<nana::treebox*>(tree);
}

void* nw_treebox_append(nw_treebox_handle tree,
                         const char* text, void* parent_node) {
    if (!tree || !text) return nullptr;
    auto* t = static_cast<nana::treebox*>(tree);
    try {
        if (parent_node) {
            auto* node = static_cast<nana::treebox::item_proxy*>(parent_node);
            auto child = node->append(text, text);
            auto* proxy = new nana::treebox::item_proxy(child);
            return proxy;
        } else {
            auto node = t->insert(text, text);
            auto* proxy = new nana::treebox::item_proxy(node);
            return proxy;
        }
    } catch (...) {
        return nullptr;
    }
}

void nw_treebox_clear(nw_treebox_handle tree) {
    if (!tree) return;
    static_cast<nana::treebox*>(tree)->clear();
}
