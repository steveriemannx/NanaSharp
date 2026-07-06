/**
 * wrap_timer.cpp — Timer wrapper
 */
#include "nw_internal.h"
#include <nana/gui.hpp>
#include <nana/gui/timer.hpp>
#include <new>
#include <chrono>

nw_timer_handle nw_timer_create(unsigned interval_ms,
                                 nw_timer_callback cb, void* user_data) {
    try {
        auto* timer = new nana::timer;
        nw_events_store_timer(timer, cb, user_data);
        timer->interval(std::chrono::milliseconds{interval_ms});
        timer->elapse([timer]() {
            auto ctx = nw_events_get_timer(timer);
            if (ctx.fn) ctx.fn(ctx.user_data);
        });
        return static_cast<nw_timer_handle>(timer);
    } catch (...) {
        return nullptr;
    }
}

void nw_timer_destroy(nw_timer_handle timer) {
    nw_events_remove_timer(timer);
    delete static_cast<nana::timer*>(timer);
}

void nw_timer_start(nw_timer_handle timer) {
    if (!timer) return;
    static_cast<nana::timer*>(timer)->start();
}

void nw_timer_stop(nw_timer_handle timer) {
    if (!timer) return;
    static_cast<nana::timer*>(timer)->stop();
}
