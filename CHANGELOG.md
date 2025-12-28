# Current
- Renamed from Aviyal to WinWM
- Added EXIT command to kill the window manager (Ctrl + Shift + Q)
- Windows border colors (active/inactive) feature added for Windows 11
- **Refactored keybindings system**: Introduced configurable `modKey` (modifier key)
  - Default changed from hardcoded keys to configurable Alt key (LMENU)
  - Support for "modKey" placeholder in config JSON
  - Updated default keybindings to use arrow keys instead of HJKL
  - Fixed keybinding conflicts (changed workspace switching to Ctrl + Shift + Arrows)
- **Added comprehensive documentation** in `docs/` folder:
  - Complete configuration reference with examples
  - Keybindings customization guide
  - Commands reference
  - Window rules guide
  - Layouts documentation
  - IPC/WebSocket documentation
  - Fully commented default config example
- Improved exit behavior: Windows from workspace 1 stay visible, others minimized to taskbar
- Added `MINIMIZE_FOCUSED_WINDOW` command with default keybinding (Alt + M)

# Old (based on Aviyal v0.1.7-fix2)
- Fixed window reorders after waking up from hibernation
- Fixed redundant ShouldWindowBeIgnored() making event handlers more responsive
- Fixed event handler ordering
- Fixed exe querrying speed due to faulty logic in GetExePathFromHWND()
