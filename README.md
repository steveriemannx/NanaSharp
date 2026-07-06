# NanaSharp

C# bindings for the [Nana C++ GUI library](https://github.com/cnjinhao/nana).
Write cross-platform desktop GUI apps in C# that run on **Windows, macOS, Linux, and FreeBSD**.

```csharp
using Nana;

var form  = new Form("Hello NanaSharp!", 400, 300);
var label = new Label(form, "What's your name?", 10, 10, 380, 30);
var input = new TextBox(form, 10, 50, 200, 28);
var btn   = new Button(form, "Greet", 10, 90, 100, 30);

btn.Click += (_, _) => label.Caption = $"Hello, {input.Text}!";

form.Show();
Application.Run();
```

## Quick Start

### macOS / Linux / FreeBSD

```bash
# 1. Clone with submodule
git clone --recursive https://github.com/steveriemannx/NanaSharp.git
cd NanaSharp

# 2. Build C++ wrapper + C# library + copy native lib
./build.sh

# 3. Run the demo
dotnet run --project samples/HelloWorld
```

`build.sh` handles CMake, dotnet build, and copies the native library
(`libnanawrap.dylib` / `.so`) into the output directory automatically.

### Windows

```cmd
git clone --recursive https://github.com/steveriemannx/NanaSharp.git
cd NanaSharp

cmake -S native\nanawrap -B native\nanawrap\build
cmake --build native\nanawrap\build --config Release

dotnet build src\NanaSharp\NanaSharp.csproj -c Release
dotnet build samples\HelloWorld\HelloWorld.csproj -c Release

copy native\nanawrap\build\Release\nanawrap.dll samples\HelloWorld\bin\Release\net10.0\

cd samples\HelloWorld
dotnet run
```

## Usage

### Create your own app

```bash
dotnet new console -n MyApp
cd MyApp
dotnet add reference ../NanaSharp/src/NanaSharp/NanaSharp.csproj
```

Write your code with `using Nana;`. The native library must be in the output directory:

| Platform | Native library | Copy command |
|----------|---------------|-------------|
| Windows | `nanawrap.dll` | `copy native\nanawrap\build\Release\nanawrap.dll MyApp\bin\Release\net10.0\` |
| macOS | `libnanawrap.dylib` | `cp native/nanawrap/build/libnanawrap.dylib MyApp/bin/Release/net10.0/` |
| Linux / FreeBSD | `libnanawrap.so` | `cp native/nanawrap/build/libnanawrap.so MyApp/bin/Release/net10.0/` |

> `build.sh` does this copy automatically for the included samples.

## Prerequisites

| Platform | Dependencies |
|----------|-------------|
| **Windows** | Visual Studio 2022 with C++ desktop workload, or MinGW-w64 |
| **macOS** | Xcode Command Line Tools (`xcode-select --install`) |
| **Linux** | `sudo apt install libx11-dev libxft-dev libfontconfig1-dev libpng-dev libjpeg-dev cmake` |
| **FreeBSD** | `pkg install xorg-libraries fontconfig freetype2 png jpeg cmake` |

All platforms need [.NET SDK](https://dotnet.microsoft.com/download) (8.0+).

## Architecture

```
NanaSharp (C#)
  └─ P/Invoke → nanawrap (C ABI shared lib)
                   └─ Nana (C++ GUI library)
                        └─ Platform: Win32 / Cocoa / X11
```

## Project Structure

```
NanaSharp/
├── nana/                  ← git submodule (Nana C++ library)
├── native/nanawrap/       ← C ABI wrapper (CMake → libnanawrap)
├── src/NanaSharp/         ← C# binding library (namespace: Nana)
├── samples/HelloWorld/   ← demo application
├── build.sh               ← one-command build
└── README.md
```

## Widget Coverage

`Form` `Button` `Label` `TextBox` `CheckBox` `ComboBox`
`ListBox` `Slider` `ProgressBar` `Picture` `Group` `Panel`
`Menu` `Menubar` `Toolbar` `TreeBox` `Timer` `Place` (layout)

## License

BSD 2-Clause.
