# Commands Reference

This document lists all available WinWM commands that can be used in keybindings or sent via IPC.

## Window Focus Commands

Move focus between windows in the current workspace.

### `FOCUS_LEFT_WINDOW`
Focus the window to the left of the currently focused window.

**Default binding:** `Mod + Left Arrow`

### `FOCUS_RIGHT_WINDOW`
Focus the window to the right of the currently focused window.

**Default binding:** `Mod + Right Arrow`

### `FOCUS_TOP_WINDOW`
Focus the window above the currently focused window.

**Default binding:** `Mod + Up Arrow`

### `FOCUS_BOTTOM_WINDOW`
Focus the window below the currently focused window.

**Default binding:** `Mod + Down Arrow`

---

## Window Movement Commands

Move and rearrange windows within and between workspaces.

### `SHIFT_FOCUSED_WINDOW_LEFT`
Swap the focused window with the window to its left.

**Default binding:** `Mod + Shift + Left Arrow`

### `SHIFT_FOCUSED_WINDOW_RIGHT`
Swap the focused window with the window to its right.

**Default binding:** `Mod + Shift + Right Arrow`

### `SHIFT_WINDOW_PREVIOUS_WORKSPACE`
Move the focused window to the previous workspace (wraps around).

**Default binding:** `Ctrl + Mod + Left Arrow`

### `SHIFT_WINDOW_NEXT_WORKSPACE`
Move the focused window to the next workspace (wraps around).

**Default binding:** `Ctrl + Mod + Right Arrow`

---

## Workspace Commands

Switch between virtual workspaces.

### `FOCUS_PREVIOUS_WORKSPACE`
Switch to the previous workspace (wraps around to last if on first).

**Default binding:** `Ctrl + Shift + Left Arrow`

### `FOCUS_NEXT_WORKSPACE`
Switch to the next workspace (wraps around to first if on last).

**Default binding:** `Ctrl + Shift + Right Arrow`

### `FOCUS_WORKSPACE_1` through `FOCUS_WORKSPACE_9`
Jump directly to workspace 1-9.

**Default bindings:** `Mod + 1` through `Mod + 9`

---

## Window State Commands

Change window properties and behavior.

### `CLOSE_FOCUSED_WINDOW`
Close the currently focused window.

**Default binding:** `Mod + Q`

### `TOGGLE_FLOATING_WINDOW`
Toggle floating mode for the focused window. Floating windows are not tiled and can be moved/resized freely.

**Default binding:** `Mod + F`

### `TOGGLE_STACKED_WINDOW`
Toggle stacked mode for the focused window. Only applicable in stack layout.

**Default binding:** `Ctrl + Shift + S`

---

## WinWM Control Commands

Manage WinWM itself.

### `RESTART`
Restart WinWM and hot-reload the configuration file. Windows remain in their current state.

**Default binding:** `Ctrl + Shift + R`

**Use case:** Apply config changes without fully exiting WinWM.

### `UPDATE`
Force update/refresh the tiling layout. Useful if windows get out of sync.

**Default binding:** `Ctrl + Shift + U`

### `EXIT`
Shut down WinWM cleanly:
1. Unhooks all event listeners
2. Shows windows from workspace 1 normally
3. Minimizes windows from other workspaces (accessible via taskbar)
4. Resets all window border colors to system defaults
5. Exits the process

**Default binding:** `Ctrl + Shift + Q`

---

## Using Commands via IPC

Commands can be triggered remotely via the WebSocket server. See [IPC Documentation](ipc.md) for details.

Example WebSocket message:
```json
{
  "command": "FOCUS_RIGHT_WINDOW"
}
```

---

## Command Naming Convention

Commands follow a consistent naming pattern:

- `FOCUS_*` - Change focus to a window or workspace
- `SHIFT_*` - Move/rearrange windows
- `TOGGLE_*` - Toggle a window state on/off
- `CLOSE_*` - Close/destroy a window

---

## See Also

- [Keybindings Reference](keybindings.md) - How to bind commands to keyboard shortcuts
- [IPC Documentation](ipc.md) - Remote control via WebSocket
- [Configuration Guide](configuration.md) - Full configuration reference
