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
dotnet build "$ROOT/samples/HelloWorld/HelloWorld.csproj" -c "$BUILD_TYPE"

# Copy the native library alongside the example
LIB_SRC="$ROOT/native/nanawrap/build/libnanawrap.dylib"
LIB_SRC_SO="$ROOT/native/nanawrap/build/libnanawrap.so"
LIB_SRC_WIN="$ROOT/native/nanawrap/build/Release/nanawrap.dll"

# Read TargetFramework from the .csproj so we don't hardcode it
HELLO_CS_PROJ="$ROOT/samples/HelloWorld/HelloWorld.csproj"
TFM=$(sed -n 's/.*<TargetFramework>\([^<]*\)<\/TargetFramework>.*/\1/p' "$HELLO_CS_PROJ")
LIB_DST="$ROOT/samples/HelloWorld/bin/$BUILD_TYPE/$TFM/"

if [ -f "$LIB_SRC" ]; then
    cp "$LIB_SRC" "$LIB_DST"
elif [ -f "$LIB_SRC_SO" ]; then
    cp "$LIB_SRC_SO" "$LIB_DST"
elif [ -f "$LIB_SRC_WIN" ]; then
    cp "$LIB_SRC_WIN" "$LIB_DST"
fi

echo ""
echo "==> All done!"
echo "    Run:  dotnet run --project samples/HelloWorld"
