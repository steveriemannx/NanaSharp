/**
 * wrap_events.cpp — Event callback infrastructure
 *
 * Nana's event system uses std::function for callbacks. We need to
 * bridge C function pointers + void* user_data into C++ lambdas.
 *
 * Pattern:
 *   nw_*_on_click(handle, callback_fn, user_data)
 *   → stores {callback_fn, user_data} in a per-widget event context
 *   → registers a lambda with nana that calls callback_fn(user_data)
 *
 * The event context lives as long as the widget lives. We use a
 * thread-safe map keyed by the widget's nana::window handle to
 * store callbacks so the C++ lambda remains valid.
 */

#include "nw_internal.h"
#include <nana/gui.hpp>
#include <unordered_map>
#include <mutex>
#include <functional>
#include <string>

/* ── Global callback registries (keyed by native window handle) ─────── */
/* We use nana::window (basic_window*) as key when available, or a raw
   pointer cast. For simplicity, we key by the nana widget pointer.  */

static std::mutex g_click_mutex;
static std::unordered_map<void*, ClickEventCtx> g_click_map;

static std::mutex g_tchanged_mutex;
static std::unordered_map<void*, TextChangedCtx> g_textchanged_map;

static std::mutex g_timer_mutex;
static std::unordered_map<void*, TimerCtx> g_timer_map;

/* ── Click callback storage ─────────────────────────────────────────── */

void nw_events_store_click(void* widget_ptr, nw_click_callback fn, void* ud) {
    std::lock_guard<std::mutex> lk(g_click_mutex);
    if (fn)
        g_click_map[widget_ptr] = ClickEventCtx{fn, ud};
    else
        g_click_map.erase(widget_ptr);
}

ClickEventCtx nw_events_get_click(void* widget_ptr) {
    std::lock_guard<std::mutex> lk(g_click_mutex);
    auto it = g_click_map.find(widget_ptr);
    if (it != g_click_map.end()) return it->second;
    return {nullptr, nullptr};
}

/* ── Text-changed callback storage ──────────────────────────────────── */

void nw_events_store_textchanged(void* widget_ptr, nw_text_changed_callback fn, void* ud) {
    std::lock_guard<std::mutex> lk(g_tchanged_mutex);
    if (fn)
        g_textchanged_map[widget_ptr] = TextChangedCtx{fn, ud};
    else
        g_textchanged_map.erase(widget_ptr);
}

TextChangedCtx nw_events_get_textchanged(void* widget_ptr) {
    std::lock_guard<std::mutex> lk(g_tchanged_mutex);
    auto it = g_textchanged_map.find(widget_ptr);
    if (it != g_textchanged_map.end()) return it->second;
    return {nullptr, nullptr};
}

/* ── Timer callback storage ─────────────────────────────────────────── */

void nw_events_store_timer(void* timer_ptr, nw_timer_callback fn, void* ud) {
    std::lock_guard<std::mutex> lk(g_timer_mutex);
    if (fn)
        g_timer_map[timer_ptr] = TimerCtx{fn, ud};
    else
        g_timer_map.erase(timer_ptr);
}

TimerCtx nw_events_get_timer(void* timer_ptr) {
    std::lock_guard<std::mutex> lk(g_timer_mutex);
    auto it = g_timer_map.find(timer_ptr);
    if (it != g_timer_map.end()) return it->second;
    return {nullptr, nullptr};
}

void nw_events_remove_timer(void* timer_ptr) {
    std::lock_guard<std::mutex> lk(g_timer_mutex);
    g_timer_map.erase(timer_ptr);
}
