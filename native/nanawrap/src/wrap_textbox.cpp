/**
 * wrap_textbox.cpp — TextBox widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/textbox.hpp>
#include <new>

nw_textbox_handle nw_textbox_create(nw_form_handle parent,
                                     int x, int y, unsigned w, unsigned h) {
    if (!parent) return nullptr;
    try {
        auto* tb = new nana::textbox(*static_cast<nana::form*>(parent),
                                     nana::rectangle{x, y, w, h});
        return static_cast<nw_textbox_handle>(tb);
    } catch (...) {
        return nullptr;
    }
}

void nw_textbox_destroy(nw_textbox_handle tb) {
    delete static_cast<nana::textbox*>(tb);
}

void nw_textbox_set_text(nw_textbox_handle tb, const char* text) {
    if (!tb) return;
    static_cast<nana::textbox*>(tb)->caption(nw_safe_str(text));
}

char* nw_textbox_get_text(nw_textbox_handle tb) {
    if (!tb) return nullptr;
    return nw_strdup(static_cast<nana::textbox*>(tb)->caption());
}

void nw_textbox_set_editable(nw_textbox_handle tb, int editable) {
    if (!tb) return;
    static_cast<nana::textbox*>(tb)->editable(editable != 0);
}

void nw_textbox_set_multi_line(nw_textbox_handle tb, int multi) {
    if (!tb) return;
    static_cast<nana::textbox*>(tb)->multi_lines(multi != 0);
}

void nw_textbox_append(nw_textbox_handle tb, const char* text) {
    if (!tb) return;
    nana::textbox* t = static_cast<nana::textbox*>(tb);
    t->caption(t->caption() + nw_safe_str(text));
}

void nw_textbox_on_text_changed(nw_textbox_handle tb,
                                 nw_text_changed_callback cb, void* user_data) {
    if (!tb) return;
    nana::textbox* t = static_cast<nana::textbox*>(tb);
    nw_events_store_textchanged(tb, cb, user_data);

    t->events().text_changed([tb](const nana::arg_textbox& arg) {
        auto ctx = nw_events_get_textchanged(tb);
        if (ctx.fn) {
            std::string text = arg.widget.caption();
            ctx.fn(tb, text.c_str(), ctx.user_data);
        }
    });
}
