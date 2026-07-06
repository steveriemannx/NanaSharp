/**
 * wrap_toolbar.cpp — Toolbar widget wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/widgets/toolbar.hpp>
#include <mutex>
#include <new>
#include <unordered_map>
#include <utility>
#include <vector>

/* Store per-button callbacks keyed by toolbar handle + button index */
struct ToolbarButtonCtx {
    nw_click_callback cb;
    void* user_data;
};

static std::mutex g_toolbar_mutex;
static std::unordered_map<void*, std::vector<ToolbarButtonCtx>> g_toolbar_buttons;
static std::unordered_map<void*, bool> g_toolbar_event_set;

nw_toolbar_handle nw_toolbar_create(nw_form_handle form) {
    if (!form) return nullptr;
    try {
        auto* tb = new nana::toolbar(*static_cast<nana::form*>(form));
        return static_cast<nw_toolbar_handle>(tb);
    } catch (...) {
        return nullptr;
    }
}

void nw_toolbar_destroy(nw_toolbar_handle tb) {
    if (!tb) return;
    {
        std::lock_guard<std::mutex> lk(g_toolbar_mutex);
        g_toolbar_buttons.erase(tb);
        g_toolbar_event_set.erase(tb);
    }
    delete static_cast<nana::toolbar*>(tb);
}

void nw_toolbar_append_button(nw_toolbar_handle tb, const char* text,
                               nw_click_callback cb, void* user_data) {
    if (!tb) return;
    nana::toolbar* t = static_cast<nana::toolbar*>(tb);
    t->append(nw_safe_str(text));

    std::lock_guard<std::mutex> lk(g_toolbar_mutex);
    std::size_t idx = g_toolbar_buttons[tb].size();
    g_toolbar_buttons[tb].push_back({cb, user_data});

    /* Install the selected event handler once per toolbar */
    if (!g_toolbar_event_set[tb]) {
        g_toolbar_event_set[tb] = true;
        t->events().selected([&](const nana::arg_toolbar& arg) {
            std::lock_guard<std::mutex> lk(g_toolbar_mutex);
            auto it = g_toolbar_buttons.find(tb);
            if (it != g_toolbar_buttons.end() && arg.button < it->second.size()) {
                auto& ctx = it->second[arg.button];
                if (ctx.cb) ctx.cb(tb, ctx.user_data);
            }
        });
    }
}

void nw_toolbar_append_separator(nw_toolbar_handle tb) {
    if (!tb) return;
    static_cast<nana::toolbar*>(tb)->separate();
}
