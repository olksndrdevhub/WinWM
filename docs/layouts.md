# Tiling Layouts Guide

WinWM supports multiple tiling layout algorithms. This guide explains each layout and when to use them.

## Configuring Layouts

Set the layout in `winwm.json`:

```json
{
  "layout": "dwindle"
}
```

After changing the layout, press `Ctrl + Shift + R` to reload the configuration.

---

## Available Layouts

### Dwindle Layout (Default)

**Binary Space Partitioning (BSP)** - Splits the screen alternately horizontal and vertical.

```
┌─────────┬─────────┐
│         │    2    │
│    1    ├─────────┤
│         │    3    │
└─────────┴─────────┘
```

**How it works:**
- First window takes full screen
- Second window splits the space (horizontal or vertical)
- Each new window splits the current space in the opposite direction
- Creates a balanced, tree-like structure

**Best for:**
- General-purpose use
- Balanced screen usage
- Multiple windows of similar importance

**Configure:**
```json
{
  "layout": "dwindle"
}
```

---

### Stack Layout

**Master-Stack** - One large master window with others stacked vertically.

```
┌─────────┬───┐
│         │ 2 │
│         ├───┤
│    1    │ 3 │
│ (Master)├───┤
│         │ 4 │
└─────────┴───┘
```

**How it works:**
- First window is the "master" (takes ~60-70% of screen width)
- Additional windows stack vertically on the right
- Toggle stacked windows to swap with master

**Best for:**
- Primary application workflows (coding, writing, browsing)
- One main focus with reference windows
- IDE-like layouts

**Configure:**
```json
{
  "layout": "stack"
}
```

**Stack-specific commands:**
- `TOGGLE_STACKED_WINDOW` (`Ctrl + Shift + S`) - Swap stacked window with master

---

## Layout Comparison

| Feature | Dwindle | Stack |
|---------|---------|-------|
| **Focus** | Balanced | One primary window |
| **Screen usage** | Even split | Master gets most space |
| **Windows** | All equal importance | Master + helpers |
| **Best for** | Multitasking | Single-task focus |
| **Typical use** | Research, comparison | Coding, writing |

---

## Working with Layouts

### Common Operations (All Layouts)

**Moving focus:**
- `Mod + Arrow Keys` - Navigate between windows

**Rearranging windows:**
- `Mod + Shift + Left/Right` - Swap window positions
- Changes the tiling order

**Floating windows:**
- `Mod + F` - Toggle floating for focused window
- Floating windows overlay tiled windows and can be moved freely

**Manual refresh:**
- `Ctrl + Shift + U` - Force layout recalculation if windows get out of sync

---

## Choosing a Layout

### Use **Dwindle** if you:
- Work with many windows of equal importance
- Frequently compare documents side-by-side
- Want balanced screen real estate
- Do research, data analysis, or multi-source work

### Use **Stack** if you:
- Have one main application with supporting windows
- Write code with reference documentation open
- Want a primary window to dominate the screen
- Prefer IDE-like workspace organization

---

## Layout Tips

1. **Try both layouts** - Switch between them to see what fits your workflow
   - Dwindle: `"layout": "dwindle"`
   - Stack: `"layout": "stack"`

2. **Use floating windows** - Not everything needs to be tiled
   - Calculator, media players, chat apps work well floating
   - See [Window Rules](window-rules.md) to auto-float specific apps

3. **Adjust gaps** - Customize spacing to your preference
   ```json
   {
     "inner": 10,  // Gap between windows
     "left": 5,
     "top": 5,
     "right": 5,
     "bottom": 5
   }
   ```

4. **Use multiple workspaces** - Different layouts for different tasks
   - Workspace 1: Dwindle for research
   - Workspace 2: Stack for coding
   - (Note: Currently all workspaces use the same layout)

---

## Advanced: Per-Workspace Layouts (Future)

Currently, all workspaces use the same layout. Per-workspace layout configuration may be added in the future.

---

## See Also

- [Configuration Guide](configuration.md) - Full config options including gaps and margins
- [Keybindings Reference](keybindings.md) - Shortcuts for window management
- [Window Rules](window-rules.md) - Auto-float specific applications
