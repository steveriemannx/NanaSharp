#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "$0")" && pwd)"
BUILD_TYPE="${1:-Release}"   # 默认 Release

# ── Native library (C++) ──────────────────────────────────────────────

echo "==> Building native library ($BUILD_TYPE) ..."
cmake -S "$ROOT/native/nanawrap" \
      -B "$ROOT/native/nanawrap/build" \
      -DCMAKE_BUILD_TYPE="$BUILD_TYPE"
cmake --build "$ROOT/native/nanawrap/build" --config "$BUILD_TYPE" -- -j"$(nproc 2>/dev/null || sysctl -n hw.ncpu 2>/dev/null || echo 4)"

# ── Managed library (C#) ──────────────────────────────────────────────
# Build both Debug and Release so `dotnet run` and `dotnet run -c Release` both work.

echo "==> Building NanaSharp C# library (Debug + Release) ..."
dotnet build "$ROOT/src/NanaSharp/NanaSharp.csproj"
dotnet build "$ROOT/src/NanaSharp/NanaSharp.csproj" -c Release

echo "==> Building HelloWorld example (Debug + Release) ..."
dotnet build "$ROOT/samples/HelloWorld/HelloWorld.csproj"
dotnet build "$ROOT/samples/HelloWorld/HelloWorld.csproj" -c Release

# ── Copy native library to output dirs ─────────────────────────────────

HELLO_CS_PROJ="$ROOT/samples/HelloWorld/HelloWorld.csproj"
TFM=$(sed -n 's/.*<TargetFramework>\([^<]*\)<\/TargetFramework>.*/\1/p' "$HELLO_CS_PROJ")

# Platform-specific dylib/so/dll name
if [ -f "$ROOT/native/nanawrap/build/libnanawrap.dylib" ]; then
    LIB_FILE="$ROOT/native/nanawrap/build/libnanawrap.dylib"
elif [ -f "$ROOT/native/nanawrap/build/libnanawrap.so" ]; then
    LIB_FILE="$ROOT/native/nanawrap/build/libnanawrap.so"
elif [ -f "$ROOT/native/nanawrap/build/$BUILD_TYPE/nanawrap.dll" ]; then
    LIB_FILE="$ROOT/native/nanawrap/build/$BUILD_TYPE/nanawrap.dll"
else
    LIB_FILE="$ROOT/native/nanawrap/build/nanawrap.dll"
fi

# Always copy to both Debug and Release so both `dotnet run` and
# `dotnet run -c Release` work out of the box.
DST_DBG="$ROOT/samples/HelloWorld/bin/Debug/$TFM"
DST_REL="$ROOT/samples/HelloWorld/bin/Release/$TFM"
mkdir -p "$DST_DBG" "$DST_REL"
cp "$LIB_FILE" "$DST_DBG/"
cp "$LIB_FILE" "$DST_REL/"

echo ""
echo "==> All done!"
echo "    Run:  dotnet run --project samples/HelloWorld"
echo "    Or:   dotnet run -c Release --project samples/HelloWorld"
