/**
 * nw_internal.h — Internal helpers shared across wrapper source files
 * NOT part of the public C ABI.
 */
#ifndef NANAWRAP_INTERNAL_H
#define NANAWRAP_INTERNAL_H

#include <nanawrap/nanawrap.h>
#include <string>

/* String helpers (defined in util.cpp) */
char* nw_strdup(const std::string& s);
char* nw_strdup(const char* s);
const char* nw_safe_str(const char* s);

/* ── Event context structs (full definitions) ─────────────────────── */

struct ClickEventCtx {
    nw_click_callback fn = nullptr;
    void* user_data = nullptr;
};

struct TextChangedCtx {
    nw_text_changed_callback fn = nullptr;
    void* user_data = nullptr;
};

struct TimerCtx {
    nw_timer_callback fn = nullptr;
    void* user_data = nullptr;
};

/* Event storage helpers (defined in wrap_events.cpp) */
void nw_events_store_click(void* widget_ptr, nw_click_callback fn, void* ud);
ClickEventCtx nw_events_get_click(void* widget_ptr);

void nw_events_store_textchanged(void* widget_ptr, nw_text_changed_callback fn, void* ud);
TextChangedCtx nw_events_get_textchanged(void* widget_ptr);

void nw_events_store_timer(void* timer_ptr, nw_timer_callback fn, void* ud);
TimerCtx nw_events_get_timer(void* timer_ptr);
void nw_events_remove_timer(void* timer_ptr);

#endif
