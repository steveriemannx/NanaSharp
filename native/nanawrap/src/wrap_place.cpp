/**
 * wrap_place.cpp — Place (layout manager) wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/form.hpp>
#include <nana/gui/place.hpp>
#include <new>

nw_place_handle nw_place_create(nw_form_handle form) {
    if (!form) return nullptr;
    try {
        auto* place = new nana::place(*static_cast<nana::form*>(form));
        return static_cast<nw_place_handle>(place);
    } catch (...) {
        return nullptr;
    }
}

void nw_place_destroy(nw_place_handle place) {
    delete static_cast<nana::place*>(place);
}

void nw_place_div(nw_place_handle place, const char* div_text) {
    if (!place) return;
    static_cast<nana::place*>(place)->div(nw_safe_str(div_text));
}

void nw_place_field_put(nw_place_handle place,
                         const char* field_name,
                         void* widget_handle) {
    if (!place || !field_name || !widget_handle) return;
    auto* pl = static_cast<nana::place*>(place);
    auto* w = static_cast<nana::widget*>(widget_handle);
    (*pl)[field_name] << *w;
}

void nw_place_collocate(nw_place_handle place) {
    if (!place) return;
    static_cast<nana::place*>(place)->collocate();
}
