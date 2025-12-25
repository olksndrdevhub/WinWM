# WinWM Documentation

Welcome to the WinWM documentation! This folder contains comprehensive guides for configuring and using WinWM.

## Documentation Index

### Getting Started
- **[../README.md](../README.md)** - Project overview, features, and building instructions

### Configuration
- **[configuration.md](configuration.md)** - Complete configuration reference with detailed explanations
- **[default-config.json](default-config.json)** - Fully commented example configuration file

### Usage Guides
- **[keybindings.md](keybindings.md)** - Default shortcuts and customization guide
- **[commands.md](commands.md)** - Complete list of all available WinWM commands
- **[ipc.md](ipc.md)** - WebSocket server and remote control documentation

### Advanced Topics
- **[window-rules.md](window-rules.md)** - Per-application window behavior configuration
- **[layouts.md](layouts.md)** - Understanding and choosing tiling layouts

## Quick Links

### Common Tasks

**Change the modifier key (default: Windows key)**
1. Edit `winwm.json`
2. Set `"modKey": "LMENU"` (or `LCONTROL`, `RWIN`, etc.)
3. Press `Ctrl + Shift + R` to reload

**Add a custom keyboard shortcut**
1. See [keybindings.md](keybindings.md) for key names
2. See [commands.md](commands.md) for available commands
3. Add entry to `keymaps` array in `winwm.json`
4. Press `Ctrl + Shift + R` to reload

**Make a specific app always float**
1. Edit `winwm.json`
2. Add rule to `rules` array (see [window-rules.md](window-rules.md))
3. Press `Ctrl + Shift + R` to reload

**Enable window border colors (Windows 11+)**
1. Edit `winwm.json`
2. Set `windowBorders.enabled: true`
3. Set `windowBorders.activeBorderColor: "#00FF00"` (or your preferred color)
4. Press `Ctrl + Shift + R` to reload

## Configuration File Location

WinWM looks for `winwm.json` in:

- **Portable mode**: Same directory as `winwm.exe`
- **Program Files installation**: `%LOCALAPPDATA%\..\winwm\winwm.json`

The config file is created automatically on first run with default settings.

## Getting Help

- **Issues & Bugs**: Report at [GitHub Issues](https://github.com/olksndrdevhub/WinWM/issues)
- **Configuration examples**: See [default-config.json](default-config.json)
- **Original Aviyal docs**: [Aviyal Documentation](https://github.com/TheAjaykrishnanR/aviyal)

## Contributing to Documentation

Found an error or want to improve these docs? Pull requests are welcome!

Documentation is written in Markdown and should be:
- Clear and concise
- Include practical examples
- Cover both basic and advanced usage
- Cross-reference related documents
