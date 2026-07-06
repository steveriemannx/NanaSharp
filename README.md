# NanaSharp

C# bindings for the [Nana C++ GUI library](https://github.com/cnjinhao/nana).
Write cross-platform desktop GUI apps in C# that run on **macOS, Linux, FreeBSD, and Windows**.

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

```bash
git clone --recursive https://github.com/steveriemannx/NanaSharp.git
cd NanaSharp
./build.sh
dotnet run --project examples/HelloWorld
```

## Prerequisites

| Platform | Dependencies |
|----------|-------------|
| **macOS** | Xcode Command Line Tools (`xcode-select --install`) |
| **Linux** | `sudo apt install libx11-dev libxft-dev libfontconfig1-dev libpng-dev libjpeg-dev cmake` |
| **FreeBSD** | `pkg install xorg-libraries fontconfig freetype2 png jpeg cmake` |
| **Windows** | Visual Studio 2022 with C++ desktop workload, or MinGW-w64 |

All platforms need [.NET 8.0 SDK](https://dotnet.microsoft.com/download).

## Architecture

```
NanaSharp (C#)
  └─ P/Invoke → nanawrap (C ABI shared lib)
                   └─ Nana (C++ GUI library)
                        └─ Platform: Cocoa / X11 / Win32
```

## Project Structure

```
NanaSharp/
├── nana/                  ← git submodule (Nana C++ library)
├── native/nanawrap/       ← C ABI wrapper (CMake → libnanawrap)
├── src/NanaSharp/         ← C# binding library (namespace: Nana)
├── examples/HelloWorld/   ← demo application
├── build.sh               ← one-command build
└── README.md
```

## Widget Coverage

`Form` `Button` `Label` `TextBox` `CheckBox` `ComboBox`
`ListBox` `Slider` `ProgressBar` `Picture` `Group` `Panel`
`Menu` `Menubar` `Toolbar` `TreeBox` `Timer` `Place` (layout)

## License

BSL-1.0 — same as Nana.
