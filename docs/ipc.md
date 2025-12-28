# IPC (Inter-Process Communication) Documentation

WinWM includes a WebSocket server for remote control and state querying. This allows external programs to control WinWM and retrieve information about windows and workspaces.

## WebSocket Server

### Configuration

Configure the server port in `winwm.json`:

```json
{
  "serverPort": 6969
}
```

The server automatically starts when WinWM launches and listens on `localhost` only (not accessible from network).

### Connection

Connect to: `ws://localhost:6969`

Example using JavaScript:
```javascript
const ws = new WebSocket('ws://localhost:6969');

ws.onopen = () => {
  console.log('Connected to WinWM');
};

ws.onmessage = (event) => {
  console.log('Received:', event.data);
};
```

---

## Sending Commands

Send JSON messages to execute WinWM commands.

### Command Format

```json
{
  "command": "COMMAND_NAME",
  "arguments": []
}
```

- `command`: Name of the command to execute (see [Commands Reference](commands.md))
- `arguments`: Array of string arguments (usually empty)

### Examples

**Focus next workspace:**
```json
{
  "command": "FOCUS_NEXT_WORKSPACE",
  "arguments": []
}
```

**Close focused window:**
```json
{
  "command": "CLOSE_FOCUSED_WINDOW",
  "arguments": []
}
```

**Toggle floating window:**
```json
{
  "command": "TOGGLE_FLOATING_WINDOW",
  "arguments": []
}
```

---

## Querying State

Send state query requests to retrieve information about WinWM's current state.

### Query Format

```json
{
  "query": "state"
}
```

### Response Format

The server responds with a JSON object containing:

```json
{
  "currentWorkspace": 0,
  "totalWorkspaces": 9,
  "focusedWindow": {
    "hWnd": 12345,
    "title": "Notepad",
    "exe": "notepad.exe",
    "isFloating": false
  },
  "windows": [
    {
      "hWnd": 12345,
      "title": "Notepad",
      "exe": "notepad.exe",
      "workspace": 0,
      "isFloating": false
    }
    // ... more windows
  ]
}
```

**Note:** The exact response format may vary. Check the source code or test queries for the current schema.

---

## Example: Python Client

Simple Python script to control WinWM:

```python
import websocket
import json

def send_command(ws, command, arguments=[]):
    message = {
        "command": command,
        "arguments": arguments
    }
    ws.send(json.dumps(message))

def get_state(ws):
    message = {"query": "state"}
    ws.send(json.dumps(message))
    return json.loads(ws.recv())

# Connect to WinWM
ws = websocket.create_connection("ws://localhost:6969")

# Focus next workspace
send_command(ws, "FOCUS_NEXT_WORKSPACE")

# Get current state
state = get_state(ws)
print(f"Current workspace: {state['currentWorkspace']}")
print(f"Total windows: {len(state['windows'])}")

ws.close()
```

Install websocket-client: `pip install websocket-client`

---

## Example: JavaScript/Node.js Client

```javascript
const WebSocket = require('ws');

const ws = new WebSocket('ws://localhost:6969');

ws.on('open', () => {
  // Send command
  ws.send(JSON.stringify({
    command: 'FOCUS_RIGHT_WINDOW',
    arguments: []
  }));

  // Query state
  setTimeout(() => {
    ws.send(JSON.stringify({ query: 'state' }));
  }, 100);
});

ws.on('message', (data) => {
  const response = JSON.parse(data);
  console.log('State:', response);
});
```

Install ws: `npm install ws`

---

## Example Use Cases

### Custom Status Bar

Query WinWM state periodically to display current workspace, window count, etc. in a status bar (like Polybar, i3status).

```python
import websocket
import json
import time

ws = websocket.create_connection("ws://localhost:6969")

while True:
    ws.send(json.dumps({"query": "state"}))
    state = json.loads(ws.recv())

    print(f"Workspace: {state['currentWorkspace'] + 1}/9 | Windows: {len(state['windows'])}")
    time.sleep(1)
```

### External Hotkey Manager

Use AutoHotkey or similar tools to define custom shortcuts that control WinWM via WebSocket.

**AutoHotkey example:**
```ahk
; Custom hotkey: Win + Shift + W to focus next workspace
#^w::
{
  ws := ComObjCreate("WebSocket")
  ws.Connect("ws://localhost:6969")
  ws.Send('{"command": "FOCUS_NEXT_WORKSPACE", "arguments": []}')
  ws.Close()
}
```

### Workspace Switcher GUI

Build a graphical workspace switcher application that connects to WinWM and displays thumbnails of each workspace.

### Integration with Other Tools

- **Polybar/i3blocks**: Display WinWM status in Linux-style status bars
- **Rainmeter**: Windows desktop customization
- **Stream Deck**: Physical buttons to control workspaces
- **Voice control**: Integrate with voice command software

---

## Available Commands via IPC

All commands from [Commands Reference](commands.md) can be sent via IPC:

### Window Focus
- `FOCUS_LEFT_WINDOW`
- `FOCUS_RIGHT_WINDOW`
- `FOCUS_TOP_WINDOW`
- `FOCUS_BOTTOM_WINDOW`

### Window Movement
- `SHIFT_FOCUSED_WINDOW_LEFT`
- `SHIFT_FOCUSED_WINDOW_RIGHT`
- `SHIFT_WINDOW_PREVIOUS_WORKSPACE`
- `SHIFT_WINDOW_NEXT_WORKSPACE`

### Workspace Navigation
- `FOCUS_PREVIOUS_WORKSPACE`
- `FOCUS_NEXT_WORKSPACE`
- `FOCUS_WORKSPACE_1` through `FOCUS_WORKSPACE_9`

### Window Actions
- `CLOSE_FOCUSED_WINDOW`
- `TOGGLE_FLOATING_WINDOW`
- `TOGGLE_FULLSCREEN_WINDOW`
- `SWAP_WITH_MASTER`
- `TOGGLE_WORKSPACE_LAYOUT`

### WinWM Control
- `RESTART`
- `UPDATE`
- `EXIT`

---

## Security Considerations

- Server listens on `localhost` only - not accessible from network
- No authentication required (assumes trusted local environment)
- Consider firewall rules if running in multi-user environment

---

## Troubleshooting

**Can't connect to WebSocket:**
1. Check WinWM is running
2. Verify correct port in config (`serverPort`)
3. Check firewall/antivirus isn't blocking localhost connections

**Commands not working:**
1. Check command name spelling (case-sensitive)
2. Verify command exists in [Commands Reference](commands.md)
3. Check WinWM logs (`winwm.log`) for errors

**State queries return empty:**
1. Ensure WinWM has managed some windows
2. Check query format is correct
3. Try sending a command first to verify connection

---

## See Also

- [Commands Reference](commands.md) - All available commands
- [Configuration Guide](configuration.md) - Configure server port
