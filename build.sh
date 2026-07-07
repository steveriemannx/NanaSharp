#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "$0")" && pwd)"
JOBS="$(nproc 2>/dev/null || sysctl -n hw.ncpu 2>/dev/null || echo 4)"
SRC="$ROOT/native/nanawrap"
BUILD_DBG="$SRC/debug"
BUILD_REL="$SRC/release"

# ── Native library (C++) — both Debug and Release ─────────────────────

echo "==> Building native library (Debug + Release) ..."

cmake -S "$SRC" -B "$BUILD_DBG" -DCMAKE_BUILD_TYPE=Debug
cmake --build "$BUILD_DBG" --config Debug -- -j"$JOBS"

cmake -S "$SRC" -B "$BUILD_REL" -DCMAKE_BUILD_TYPE=Release
cmake --build "$BUILD_REL" --config Release -- -j"$JOBS"

# ── Managed library (C#) — both Debug and Release ─────────────────────

echo "==> Building NanaSharp C# library (Debug + Release) ..."
dotnet build "$ROOT/src/NanaSharp/NanaSharp.csproj"
dotnet build "$ROOT/src/NanaSharp/NanaSharp.csproj" -c Release

echo "==> Building HelloWorld example (Debug + Release) ..."
dotnet build "$ROOT/samples/HelloWorld/HelloWorld.csproj"
dotnet build "$ROOT/samples/HelloWorld/HelloWorld.csproj" -c Release

# ── Copy native library to output dirs ─────────────────────────────────

HELLO_CS_PROJ="$ROOT/samples/HelloWorld/HelloWorld.csproj"
TFM=$(sed -n 's/.*<TargetFramework>\([^<]*\)<\/TargetFramework>.*/\1/p' "$HELLO_CS_PROJ")

DST_DBG="$ROOT/samples/HelloWorld/bin/Debug/$TFM"
DST_REL="$ROOT/samples/HelloWorld/bin/Release/$TFM"
mkdir -p "$DST_DBG" "$DST_REL"

# Find the built library in a build directory (handles multi-config
# generators like Xcode/MSVC which add a Debug/Release subdirectory).
find_lib() {
    local dir="$1"
    for name in libnanawrap.dylib libnanawrap.so nanawrap.dll; do
        local path
        path=$(find "$dir" -name "$name" -type f 2>/dev/null | head -1)
        if [ -n "${path:-}" ]; then
            echo "$path"
            return 0
        fi
    done
    return 1
}

LIB_DBG=$(find_lib "$BUILD_DBG" || true)
LIB_REL=$(find_lib "$BUILD_REL" || true)

if [ -n "${LIB_DBG:-}" ]; then
    cp "$LIB_DBG" "$DST_DBG/"
    echo "   Debug:    $LIB_DBG -> $DST_DBG/"
fi
if [ -n "${LIB_REL:-}" ]; then
    cp "$LIB_REL" "$DST_REL/"
    echo "   Release:  $LIB_REL -> $DST_REL/"
fi

echo ""
echo "==> All done!"
echo "    Run:  dotnet run --project samples/HelloWorld"
echo "    Or:   dotnet run -c Release --project samples/HelloWorld"
