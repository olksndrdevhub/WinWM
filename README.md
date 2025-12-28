# WinWM

> Window manager for Windows written purely in C# that's simple, lightweight and portable.

This is a fork of [Aviyal](https://github.com/TheAjaykrishnanR/aviyal) - a dynamic tiling window manager for Windows.

## DISCLAIMER
This is my personal project for my personal use and is not affiliated with the original Aviyal project in any way, while built on top of it.
Project built mostly using AI tools and may be buggy. Use at your own risk.


![showcase_1](https://github.com/TheAjaykrishnanR/aviyal/blob/master/Imgs/showcase.png)

## Features

1. Workspaces
2. Workspace animations (Horizontal and vertical)
3. Dynamic Tiling : `Dwindle`, `Stack`
4. Toggle floating
5. Close focused window
6. Shift focus
7. Configuration using json
8. Hot reloading
9. Qerry state using websocket and execute commands
10. Launch apps using hotkeys
11. **Window border colors (Windows 11+ only)** - Customizable border colors for focused/unfocused windows

## Usage

Build from source (see Building section below) and run the executable.
For live debug output, run from a terminal (`cmd.exe` or `pwsh.exe`).

## Configuration

Configuration file `winwm.json` will be created at first run with sensible defaults.

**Quick links:**
- **[Complete Configuration Guide](docs/configuration.md)** - Detailed explanations of all settings
- **[Default Config with Comments](docs/default-config.json)** - Fully commented example config
- **[Keybindings Guide](docs/keybindings.md)** - Customize keyboard shortcuts
- **[Window Rules](docs/window-rules.md)** - Per-application behavior

### Key Features

**Modifier Key:** Configurable primary modifier (default: Alt key)
```json
{"modKey": "LMENU"}
```

**Window Borders (Windows 11+):** Custom border colors for focused windows
```json
{
  "windowBorders": {
    "enabled": true,
    "activeBorderColor": "#00FF00"
  }
}
```

**Window Rules:** Per-app floating, ignore rules
```json
{
  "rules": [
    {"type": "float", "method": "exe", "identifierType": "equals", "identifier": "calc.exe"}
  ]
}
```

See [docs/configuration.md](docs/configuration.md) for complete details.

## Default keybindings

**Window Focus** (Mod = Alt key by default):
- `FOCUS WINDOW LEFT`: `Mod + Left Arrow`
- `FOCUS WINDOW RIGHT`: `Mod + Right Arrow`
- `FOCUS WINDOW TOP`: `Mod + Up Arrow`
- `FOCUS WINDOW BOTTOM`: `Mod + Down Arrow`

**Move Windows in Workspace**:
- `SWAP WINDOW LEFT`: `Mod + Shift + Left Arrow`
- `SWAP WINDOW RIGHT`: `Mod + Shift + Right Arrow`

**Move Windows Between Workspaces**:
- `SHIFT WINDOW TO PREVIOUS WORKSPACE`: `Ctrl + Mod + Left Arrow`
- `SHIFT WINDOW TO NEXT WORKSPACE`: `Ctrl + Mod + Right Arrow`

**Switch Workspaces**:
- `FOCUS PREVIOUS WORKSPACE`: `Ctrl + Shift + Left Arrow`
- `FOCUS NEXT WORKSPACE`: `Ctrl + Shift + Right Arrow`
- `JUMP TO WORKSPACE 1-9`: `Mod + 1-9`

**Window Actions**:
- `CLOSE FOCUSED WINDOW`: `Mod + Q`
- `MINIMIZE FOCUSED WINDOW`: `Mod + M`
- `TOGGLE WINDOW FLOATING`: `Mod + F`
- `TOGGLE WINDOW STACKED`: `Ctrl + Shift + S`

**WinWM Commands**:
- `RESTART APPLICATION`: `Ctrl + Shift + R` (hot reload config)
- `REFRESH TILING`: `Ctrl + Shift + U`
- `EXIT WINWM`: `Ctrl + Shift + Q`

By default `9` workspaces are initialized and the modifier key is `LMENU` (Left Alt key).

## Building

WinWM can be built using a custom C# AOT compiler called [dflat](https://github.com/TheAjaykrishnanR/dflat)
If you have `dflat` in path, building is as simple as:

```
git clone https://github.com/olksndrdevhub/WinWM.git
cd WinWM/Src
./Build.ps1
```

You will find the AOT compiled executable at `bin\winwm.exe`

For development ease, such as LSP a dotnet `csproj` file is also provided which allows language
support in IDEs. This allows you to build WinWM just like any other dotnet application.

If that's what you prefer, build it as:
```
git clone https://github.com/olksndrdevhub/WinWM.git
cd WinWM/Src
dotnet build
```

You can find the executable at `bin\Debug\net9.0-windows\`

## Contributing

PRs welcome !
