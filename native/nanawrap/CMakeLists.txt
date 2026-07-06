# CMake configuration for nanawrap — C ABI wrapper around Nana C++ GUI library
# Produces libnanawrap.so (Unix/FreeBSD) or libnanawrap.dylib (macOS) or nanawrap.dll (Windows)
#
# Usage from NanaSharp: P/Invoke with [DllImport("libnanawrap")]

cmake_minimum_required(VERSION 3.12 FATAL_ERROR)
project(nanawrap VERSION 0.1.0
        DESCRIPTION "C ABI wrapper for the Nana C++ GUI library"
        HOMEPAGE_URL "https://github.com/cnjinhao/nana"
        LANGUAGES CXX)

# ── Dependencies ──────────────────────────────────────────────────────

# Nana is a git submodule at the repository root
if(NOT TARGET nana)
    add_subdirectory(${CMAKE_CURRENT_LIST_DIR}/../../nana
                     ${CMAKE_CURRENT_BINARY_DIR}/nana-build)
endif()

# ── Library target ────────────────────────────────────────────────────

if(WIN32)
    # Must be set BEFORE add_library so P/Invoke can find DLL symbols
    set(CMAKE_WINDOWS_EXPORT_ALL_SYMBOLS ON)
endif()

add_library(nanawrap SHARED)

target_compile_features(nanawrap PUBLIC cxx_std_17)

target_include_directories(nanawrap
    PUBLIC
        $<BUILD_INTERFACE:${CMAKE_CURRENT_LIST_DIR}/include>
    PRIVATE
        ${CMAKE_CURRENT_LIST_DIR}/include
)

target_link_libraries(nanawrap PUBLIC nana)

# ── Source files ──────────────────────────────────────────────────────

set(NANAWRAP_SOURCES
    src/wrap_form.cpp
    src/wrap_button.cpp
    src/wrap_label.cpp
    src/wrap_textbox.cpp
    src/wrap_checkbox.cpp
    src/wrap_combox.cpp
    src/wrap_listbox.cpp
    src/wrap_slider.cpp
    src/wrap_progress.cpp
    src/wrap_menu.cpp
    src/wrap_toolbar.cpp
    src/wrap_treebox.cpp
    src/wrap_picture.cpp
    src/wrap_group.cpp
    src/wrap_panel.cpp
    src/wrap_place.cpp
    src/wrap_timer.cpp
    src/wrap_events.cpp
    src/wrap_app.cpp
    src/util.cpp
)

target_sources(nanawrap PRIVATE ${NANAWRAP_SOURCES})

# ── Platform-specific settings ────────────────────────────────────────

if(UNIX)
    target_compile_options(nanawrap PRIVATE -fPIC)
endif()

if(APPLE)
    set_target_properties(nanawrap PROPERTIES
        MACOSX_RPATH ON
        INSTALL_NAME_DIR "@rpath"
    )
endif()

# ── Install ───────────────────────────────────────────────────────────

include(GNUInstallDirs)

install(TARGETS nanawrap
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
    ARCHIVE DESTINATION ${CMAKE_INSTALL_LIBDIR}
)

install(DIRECTORY include/
    DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}
)

# ── Export information ────────────────────────────────────────────────

message(STATUS "nanawrap — C wrapper for Nana GUI")
message(STATUS "  Build type:  ${CMAKE_BUILD_TYPE}")
message(STATUS "  System:      ${CMAKE_SYSTEM_NAME}")
message(STATUS "  Output:      libnanawrap")
