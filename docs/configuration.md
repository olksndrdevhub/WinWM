# WinWM Configuration Guide

WinWM uses a JSON configuration file (`winwm.json`) that is created automatically on first run. The config file location depends on where WinWM is installed:

- **Portable mode**: Same directory as `winwm.exe`
- **Program Files**: `%LOCALAPPDATA%\..\winwm\winwm.json`

## Configuration Hot-Reload

WinWM supports hot-reloading of configuration. After editing `winwm.json`, press `Ctrl + Shift + R` to reload without restarting.

## Complete Configuration Reference

### Layout Settings

```json
"layout": "dwindle"
```

The tiling layout algorithm to use:
- `"dwindle"`: Binary space partitioning (default) - splits windows alternately horizontal/vertical
- `"stack"`: Master-stack layout - one main window with others stacked

---

### Margin Settings

```json
"left": 5,
"top": 5,
"right": 5,
"bottom": 5,
"inner": 5
```

- `left`, `top`, `right`, `bottom`: Outer margins (pixels) between screen edges and tiled windows
- `inner`: Gap (pixels) between tiled windows
- Set to `0` for no gaps

---

### Workspace Settings

```json
"workspaces": 9
```

Number of virtual workspaces to create (1-9 recommended).

---

### Workspace Animations

```json
"workspaceAnimations": false,
"workspaceAnimationsDuration": 500,
"workspaceAnimationsDirection": "horizontal"
```

- `workspaceAnimations`: Enable/disable sliding animations when switching workspaces
- `workspaceAnimationsDuration`: Animation duration in milliseconds
- `workspaceAnimationsDirection`: `"horizontal"` (left/right) or `"vertical"` (up/down)

---

### Floating Window Settings

```json
"floatingWindowSize": "800x400"
```

Default size for floating windows in `WIDTHxHEIGHT` format (pixels).

---

### Server Settings

```json
"serverPort": 6969
```

WebSocket server port for remote control and state queries. See [IPC Documentation](ipc.md) for details.

---

### Modifier Key

```json
"modKey": "LMENU"
```

The primary modifier key for window management shortcuts (similar to `$mod` in i3/Sway).

**Available options:**
- `"LMENU"`: Left Alt (default - recommended to avoid conflicts with Windows shortcuts)
- `"LCONTROL"`: Left Ctrl
- `"LWIN"`: Left Windows key (not recommended - conflicts with Start menu and other Windows shortcuts)
- `"RWIN"`: Right Windows key
- `"RMENU"`: Right Alt
- Any other valid VK key name

**Usage in keymaps:** Use `"modKey"` as a placeholder in keymap definitions:
```json
{
  "keys": ["modKey", "Q"],
  "command": "CLOSE_FOCUSED_WINDOW"
}
```

---

### Window Borders (Windows 11+ only)

```json
"windowBorders": {
  "enabled": true,
  "activeBorderColor": "#00FF00",
  "inactiveBorderColor": false
}
```

Custom border colors for focused/unfocused windows using DWM API:

- `enabled`: Master toggle (`true`/`false`)
- `activeBorderColor`:
  - Hex color string (e.g., `"#00FF00"` for green)
  - `false` to disable border color for active window
- `inactiveBorderColor`:
  - Hex color string
  - `false` to disable border color for inactive windows (use system default)

**Requirements:**
- Windows 11 Build 22000 or later
- Automatically disabled on older Windows with a log message

**Note:** Only border *color* is adjustable via DWM API, not width.

---

### Window Rules

```json
"rules": [
  {
    "type": "float",
    "method": "exe",
    "identifierType": "equals",
    "identifier": "notepad.exe"
  }
]
```

Define per-application window behavior:

- `type`: Rule action
  - `"float"`: Window starts as floating
  - `"ignore"`: Window is not managed by WinWM

- `method`: How to identify the window
  - `"exe"`: Match by executable name
  - `"title"`: Match by window title
  - `"class"`: Match by window class

- `identifierType`: Matching method
  - `"equals"`: Exact match
  - `"contains"`: Partial match (substring)
  - `"startsWith"`: Prefix match
  - `"endsWith"`: Suffix match

- `identifier`: The value to match against

**Examples:**
```json
// Float all calculator windows
{
  "type": "float",
  "method": "exe",
  "identifierType": "equals",
  "identifier": "calc.exe"
}

// Ignore all windows with "Settings" in title
{
  "type": "ignore",
  "method": "title",
  "identifierType": "contains",
  "identifier": "Settings"
}
```

---

### Keymaps

```json
"keymaps": [
  {
    "keys": ["modKey", "Q"],
    "command": "CLOSE_FOCUSED_WINDOW",
    "arguments": []
  }
]
```

Define keyboard shortcuts:

- `keys`: Array of key names in order (modifiers first, then the key)
  - Use `"modKey"` placeholder for the configured modifier key
  - See [Keybindings Reference](keybindings.md) for all available keys

- `command`: Action to execute
  - See [Commands Reference](commands.md) for all available commands

- `arguments`: Array of string arguments (rarely used, usually empty `[]`)

**Key naming conventions:**
- Modifiers: `LCONTROL`, `RCONTROL`, `LSHIFT`, `RSHIFT`, `LMENU` (Alt), `RMENU`
- Letters: `A`-`Z`
- Numbers: `NUM0`-`NUM9` (top row), `NUMPAD0`-`NUMPAD9` (numpad)
- Arrows: `LEFT`, `RIGHT`, `UP`, `DOWN`
- Function: `F1`-`F12`
- Special: `SPACE`, `RETURN`, `TAB`, `ESCAPE`, `BACK` (Backspace)

---

## Example: Full Default Configuration

See [default-config.json](default-config.json) for a complete, commented example configuration file.

## See Also

- [Keybindings Reference](keybindings.md) - Default shortcuts and customization
- [Commands Reference](commands.md) - All available WinWM commands
- [IPC Documentation](ipc.md) - WebSocket server and remote control
