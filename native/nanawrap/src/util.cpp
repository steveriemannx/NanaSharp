/**
 * util.cpp — Shared utilities for the nanawrap C wrapper
 */
#include <nanawrap/nanawrap.h>
#include <cstring>
#include <cstdlib>
#include <string>

void nw_free_string(char* str) {
    std::free(str);
}

/* Allocate a copy of a std::string that the caller must free with nw_free_string */
char* nw_strdup(const std::string& s) {
    char* buf = static_cast<char*>(std::malloc(s.size() + 1));
    if (buf) {
        std::memcpy(buf, s.c_str(), s.size() + 1);
    }
    return buf;
}

char* nw_strdup(const char* s) {
    if (!s) return nullptr;
    char* buf = static_cast<char*>(std::malloc(std::strlen(s) + 1));
    if (buf) {
        std::memcpy(buf, s, std::strlen(s) + 1);
    }
    return buf;
}

/* Internal helper — not in the public header */
const char* nw_safe_str(const char* s) {
    return s ? s : "";
}
