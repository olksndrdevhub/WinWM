# Window Rules Guide

Window rules allow you to define per-application behavior, such as making specific programs always float or ignoring certain windows from tiling.

## Rule Structure

Each rule in the `rules` array has four properties:

```json
{
  "type": "float",
  "method": "exe",
  "identifierType": "equals",
  "identifier": "notepad.exe"
}
```

### Rule Properties

#### `type` - Action to take

The behavior to apply when a window matches:

- **`"float"`** - Window starts as floating (not tiled)
- **`"ignore"`** - Window is completely ignored by WinWM (not managed at all)

#### `method` - How to identify the window

What property to check:

- **`"exe"`** - Match by executable filename (e.g., `notepad.exe`, `chrome.exe`)
- **`"title"`** - Match by window title text
- **`"class"`** - Match by window class name

#### `identifierType` - Matching method

How to compare the identifier value:

- **`"equals"`** - Exact match (case-sensitive)
- **`"contains"`** - Partial match (substring search)
- **`"startsWith"`** - Matches if it begins with the identifier
- **`"endsWith"`** - Matches if it ends with the identifier

#### `identifier` - Value to match

The actual string to compare against (depends on `method`).

---

## Examples

### Float Specific Applications

Make calculator always open as floating:

```json
{
  "rules": [
    {
      "type": "float",
      "method": "exe",
      "identifierType": "equals",
      "identifier": "calc.exe"
    }
  ]
}
```

Float all Spotify windows:

```json
{
  "type": "float",
  "method": "exe",
  "identifierType": "equals",
  "identifier": "spotify.exe"
}
```

### Ignore Windows

Ignore all Windows Settings windows:

```json
{
  "type": "ignore",
  "method": "title",
  "identifierType": "contains",
  "identifier": "Settings"
}
```

Ignore Windows Task Manager:

```json
{
  "type": "ignore",
  "method": "exe",
  "identifierType": "equals",
  "identifier": "Taskmgr.exe"
}
```

### Using Title Matching

Float any window with "Picture-in-Picture" in the title:

```json
{
  "type": "float",
  "method": "title",
  "identifierType": "contains",
  "identifier": "Picture-in-Picture"
}
```

Float dialog boxes (titles often start with specific text):

```json
{
  "type": "float",
  "method": "title",
  "identifierType": "startsWith",
  "identifier": "Open"
}
```

### Multiple Rules

You can combine multiple rules. Rules are checked in order:

```json
{
  "rules": [
    {
      "type": "float",
      "method": "exe",
      "identifierType": "equals",
      "identifier": "calc.exe"
    },
    {
      "type": "ignore",
      "method": "title",
      "identifierType": "contains",
      "identifier": "Settings"
    },
    {
      "type": "float",
      "method": "exe",
      "identifierType": "equals",
      "identifier": "mspaint.exe"
    }
  ]
}
```

---

## Finding Window Information

To create rules, you need to know the window's executable name, title, or class.

### Method 1: Using PowerShell

```powershell
Get-Process | Where-Object {$_.MainWindowTitle -ne ""} | Select-Object ProcessName, MainWindowTitle
```

This shows all running windows with their executable names and titles.

### Method 2: Check WinWM Logs

WinWM logs window information when windows are shown. Check `winwm.log`:

```
WindowShown, wnd: Google Chrome, hWnd: 12345, exe: chrome.exe
```

### Method 3: Using Spy++

For advanced users, use [Spy++](https://learn.microsoft.com/en-us/visualstudio/debugger/introducing-spy-increment) (included with Visual Studio) to inspect window properties.

---

## Common Use Cases

### Float All Dialog Boxes

Many dialog boxes have "Dialog" in their class name:

```json
{
  "type": "float",
  "method": "class",
  "identifierType": "contains",
  "identifier": "Dialog"
}
```

### Float Media Players

```json
{
  "rules": [
    {
      "type": "float",
      "method": "exe",
      "identifierType": "equals",
      "identifier": "vlc.exe"
    },
    {
      "type": "float",
      "method": "exe",
      "identifierType": "equals",
      "identifier": "spotify.exe"
    }
  ]
}
```

### Ignore System Windows

```json
{
  "rules": [
    {
      "type": "ignore",
      "method": "exe",
      "identifierType": "equals",
      "identifier": "explorer.exe"
    },
    {
      "type": "ignore",
      "method": "title",
      "identifierType": "contains",
      "identifier": "Task Switching"
    }
  ]
}
```

---

## Tips

1. **Test incrementally**: Add one rule at a time and reload (`Ctrl + Shift + R`) to verify it works

2. **Use `contains` for flexibility**: Partial matching is more forgiving than exact matches
   - ✅ `"identifierType": "contains", "identifier": "Chrome"`
   - ⚠️ `"identifierType": "equals", "identifier": "Google Chrome - Full Title"`

3. **Prefer `exe` method**: Executable names are more stable than window titles
   - ✅ `"method": "exe"` - Reliable, doesn't change
   - ⚠️ `"method": "title"` - May change based on document name, etc.

4. **Check logs**: When a rule doesn't work, check `winwm.log` to see what WinWM sees

5. **Order matters**: Rules are evaluated in order; first match wins

---

## Automatically Ignored Windows

WinWM automatically ignores certain window types without needing rules:

- Windows without taskbar buttons (tool windows)
- Windows without titles
- Windows marked as `WS_EX_TOOLWINDOW`
- Certain system windows

---

## See Also

- [Configuration Guide](configuration.md) - Full configuration reference
- [Commands Reference](commands.md) - Toggle floating manually with `TOGGLE_FLOATING_WINDOW`
