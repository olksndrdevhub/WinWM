# WinWM

> Window manager for Windows written purely in C# that's simple, lightweight and portable.

This is a fork of [Aviyal](https://github.com/TheAjaykrishnanR/aviyal) - a dynamic tiling window manager for Windows.

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

## Usage

Build from source (see Building section below) and run the executable.
For live debug output, run from a terminal (`cmd.exe` or `pwsh.exe`).

## Configuration

Configuration file `winwm.json` will be created at first run. You can modify the default settings there,
including adding new keybindings etc. Look at the example config file in `Src/winwm.json`

For the original project's config documentation, see [Aviyal Config.md](https://github.com/TheAjaykrishnanR/aviyal/blob/master/Docs/Config.md)
## Default keybindings

- `FOCUS NEXT WORKSPACE`: `LCONTROL, LSHIFT, L`
- `FOCUS PREVIOUS WORKSPACE`: `LCONTROL, LSHIFT, H`
- `FOCUS WINDOW RIGHT`: `LCONTROL, L`
- `FOCUS WINDOW LEFT`: `LCONTROL, H`
- `FOCUS WINDOW TOP`: `LCONTROL, K`
- `FOCUS WINDOW BOTTOM`: `LCONTROL, J`
- `SHIFT WINDOW NEXT WORKSPACE`: `LMENU (ALT), LSHIFT, L`
- `SHIFT WINDOW PREVIOUS WORKSPACE`: `LMENU (ALT), LSHIFT, H`
- `TOGGLE WINDOW FLOATING`: `LCONTROL, LSHIFT, Z`
- `TOGGLE WINDOW STACKED`: `LCONTROL, LSHIFT, S`
- `SWAP WINDOW RIGHT`: `LMENU (ALT), L`
- `SWAP WINDOW LEFT`: `LMENU (ALT), H`
- `RESTART APPLICATION`: `LCONTROL, LSHIFT, R` (hot reload for config)
- `REFRESH TILING`: `LCONTROL, LSHIFT, U`
- `EXIT WINWM`: `LCONTROL, LSHIFT, Q`

By default `9` workspaces are initialized.

## Building

WinWM can be built using a custom C# AOT compiler called [dflat](https://github.com/TheAjaykrishnanR/dflat)
If you have `dflat` in path, building is as simple as:

```
git clone <your-repo-url>
cd WinWM/Src
./Build.ps1
```

You will find the AOT compiled executable at `bin\winwm.exe`

For development ease, such as LSP a dotnet `csproj` file is also provided which allows language
support in IDEs. This allows you to build WinWM just like any other dotnet application.

If that's what you prefer, build it as:
```
git clone <your-repo-url>
cd WinWM/Src
dotnet build
```

You can find the executable at `bin\Debug\net9.0-windows\`

## Contributing

PRs welcome !
