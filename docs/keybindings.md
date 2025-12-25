# Keybindings Reference

This document lists all default keybindings and explains how to customize them.

## Default Shortcuts

By default, WinWM uses the **Alt key** as the primary modifier (referenced as `Mod` below). You can change this by setting `modKey` in your config file.

### Window Focus

Navigate between windows using arrow keys:

| Shortcut | Action |
|----------|--------|
| `Mod + Left Arrow` | Focus window to the left |
| `Mod + Right Arrow` | Focus window to the right |
| `Mod + Up Arrow` | Focus window above |
| `Mod + Down Arrow` | Focus window below |

### Move Windows in Workspace

Swap window positions within the current workspace:

| Shortcut | Action |
|----------|--------|
| `Mod + Shift + Left Arrow` | Swap focused window left |
| `Mod + Shift + Right Arrow` | Swap focused window right |

### Move Windows Between Workspaces

Transfer windows to different workspaces:

| Shortcut | Action |
|----------|--------|
| `Ctrl + Mod + Left Arrow` | Move window to previous workspace |
| `Ctrl + Mod + Right Arrow` | Move window to next workspace |

### Switch Workspaces

Navigate between virtual workspaces:

| Shortcut | Action |
|----------|--------|
| `Ctrl + Shift + Left Arrow` | Switch to previous workspace |
| `Ctrl + Shift + Right Arrow` | Switch to next workspace |
| `Mod + 1-9` | Jump to workspace 1-9 |

### Window Actions

Manage individual windows:

| Shortcut | Action |
|----------|--------|
| `Mod + Q` | Close focused window |
| `Mod + F` | Toggle floating mode for focused window |
| `Ctrl + Shift + S` | Toggle stacked mode for focused window |

### WinWM Commands

Control WinWM itself:

| Shortcut | Action |
|----------|--------|
| `Ctrl + Shift + R` | Restart WinWM (hot-reload config) |
| `Ctrl + Shift + U` | Update/refresh tiling layout |
| `Ctrl + Shift + Q` | Exit WinWM |

## Customizing Keybindings

### Changing the Modifier Key

Edit `winwm.json` to change the primary modifier key:

```json
{
  "modKey": "LMENU"  // Use Left Alt instead of Windows key
}
```

Available modifier keys:
- `"LMENU"` - Left Alt (default - recommended)
- `"RMENU"` - Right Alt
- `"LCONTROL"` - Left Ctrl
- `"RCONTROL"` - Right Ctrl
- `"LWIN"` - Left Windows key (not recommended - conflicts with Start menu)
- `"RWIN"` - Right Windows key

### Adding Custom Keybindings

Add entries to the `keymaps` array in `winwm.json`:

```json
{
  "keymaps": [
    {
      "keys": ["modKey", "T"],
      "command": "FOCUS_WORKSPACE_1",
      "arguments": []
    }
  ]
}
```

### Available Key Names

#### Modifiers
- `LCONTROL`, `RCONTROL` - Ctrl keys
- `LSHIFT`, `RSHIFT` - Shift keys
- `LMENU`, `RMENU` - Alt keys
- `LWIN`, `RWIN` - Windows keys

#### Letters
- `A` through `Z`

#### Numbers
- `NUM0` through `NUM9` - Top row number keys
- `NUMPAD0` through `NUMPAD9` - Numpad keys

#### Arrow Keys
- `LEFT`, `RIGHT`, `UP`, `DOWN`

#### Function Keys
- `F1` through `F24`

#### Special Keys
- `SPACE` - Spacebar
- `RETURN` - Enter
- `TAB` - Tab
- `ESCAPE` - Escape
- `BACK` - Backspace
- `DELETE` - Delete
- `INSERT` - Insert
- `HOME`, `END` - Home/End
- `PRIOR`, `NEXT` - Page Up/Page Down

#### Symbols
- `OEM_MINUS` - Minus/Underscore (-)
- `OEM_PLUS` - Plus/Equals (+)
- `OEM_COMMA` - Comma (<)
- `OEM_PERIOD` - Period (>)
- `OEM_1` through `OEM_8` - Various punctuation

### Using the modKey Placeholder

When defining keybindings, use the string `"modKey"` to reference your configured modifier key:

```json
{
  "keys": ["modKey", "LSHIFT", "Q"],
  "command": "CLOSE_FOCUSED_WINDOW",
  "arguments": []
}
```

This allows you to change the modifier key in one place (`modKey` setting) and have all shortcuts update automatically.

### Keybinding Tips

1. **Order matters**: List modifier keys before the main key
   - ✅ Good: `["LCONTROL", "LSHIFT", "Q"]`
   - ❌ Bad: `["Q", "LCONTROL", "LSHIFT"]`

2. **Use modKey for portability**: Using `"modKey"` makes it easy to switch between different modifier keys
   - ✅ Good: `["modKey", "Q"]`
   - ⚠️ Okay but less flexible: `["LWIN", "Q"]`

3. **Avoid conflicts**: Don't override system shortcuts unless necessary
   - ❌ Avoid: `Ctrl + C`, `Ctrl + V`, `Alt + Tab`, `Win + L`

4. **Test after changes**: Press `Ctrl + Shift + R` to hot-reload config and test your new bindings

## See Also

- [Commands Reference](commands.md) - Complete list of available commands
- [Configuration Guide](configuration.md) - Full configuration documentation
