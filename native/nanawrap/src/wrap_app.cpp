/**
 * wrap_app.cpp — Application lifecycle wrappers
 */
#include <nanawrap/nanawrap.h>
#include <nana/gui.hpp>
#include <thread>
#include <chrono>

int nw_exec(void) {
    try {
        nana::exec();
        return NW_OK;
    } catch (...) {
        return NW_ERR;
    }
}

void nw_exit(void) {
    nana::API::exit();
}

void nw_sleep(unsigned milliseconds) {
    std::this_thread::sleep_for(std::chrono::milliseconds(milliseconds));
}
