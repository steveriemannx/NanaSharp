#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "$0")" && pwd)"
BUILD_TYPE="${1:-Release}"

echo "==> Building Nana C++ library + nanawrap C wrapper ($BUILD_TYPE) ..."
cmake -S "$ROOT/native/nanawrap" \
      -B "$ROOT/native/nanawrap/build" \
      -DCMAKE_BUILD_TYPE="$BUILD_TYPE"
cmake --build "$ROOT/native/nanawrap/build" --config "$BUILD_TYPE" -- -j"$(nproc 2>/dev/null || sysctl -n hw.ncpu 2>/dev/null || echo 4)"

echo "==> Building NanaSharp C# library ..."
dotnet build "$ROOT/src/NanaSharp/NanaSharp.csproj" -c "$BUILD_TYPE"

echo "==> Building HelloWorld example ..."
dotnet build "$ROOT/examples/HelloWorld/HelloWorld.csproj" -c "$BUILD_TYPE"

# Copy the native library alongside the example
LIB_SRC="$ROOT/native/nanawrap/build/libnanawrap.dylib"
LIB_SRC_SO="$ROOT/native/nanawrap/build/libnanawrap.so"
LIB_SRC_WIN="$ROOT/native/nanawrap/build/Release/nanawrap.dll"
LIB_DST="$ROOT/examples/HelloWorld/bin/$BUILD_TYPE/net8.0/"

if [ -f "$LIB_SRC" ]; then
    cp "$LIB_SRC" "$LIB_DST"
elif [ -f "$LIB_SRC_SO" ]; then
    cp "$LIB_SRC_SO" "$LIB_DST"
elif [ -f "$LIB_SRC_WIN" ]; then
    cp "$LIB_SRC_WIN" "$LIB_DST"
fi

echo ""
echo "==> All done!"
echo "    Run:  dotnet run --project examples/HelloWorld"
