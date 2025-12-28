using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

public class Config : IJson<Config>
{
    public string layout { get; set; } = "dwindle";

    // margins
    public int left { get; set; } = 5;
    public int top { get; set; } = 5;
    public int right { get; set; } = 5;
    public int bottom { get; set; } = 5;

    public int inner { get; set; } = 5;
    public int workspaces { get; set; } = 9;
    public string floatingWindowSize { get; set; } = "60%x50%";
    public bool workspaceAnimations = false;
    public int workspaceAnimationsDuration = 500; // milliseconds
    public string workspaceAnimationsDirection = "horizontal";
    public int serverPort = 6969;

    // Modifier key for shortcuts (default: Alt key)
    public VK modKey { get; set; } = VK.LMENU;

    // Window border settings (Windows 11+ only)
    public WindowBorderConfig windowBorders = new();

    public List<WindowRule> rules = new();
    public List<Keymap> keymaps = new();

    public Config()
    {
        // Initialize keymaps with default values using modKey
        InitializeDefaultKeymaps();
    }

    private void InitializeDefaultKeymaps()
    {
        keymaps = new()
        {
            // Focus windows using mod + arrow keys
            new() { keys = [modKey, VK.LEFT], command = COMMAND.FOCUS_LEFT_WINDOW },
            new() { keys = [modKey, VK.UP], command = COMMAND.FOCUS_TOP_WINDOW },
            new() { keys = [modKey, VK.RIGHT], command = COMMAND.FOCUS_RIGHT_WINDOW },
            new() { keys = [modKey, VK.DOWN], command = COMMAND.FOCUS_BOTTOM_WINDOW },

            // Move windows in workspace using mod + shift + arrow left/right
            new() { keys = [modKey, VK.LSHIFT, VK.LEFT], command = COMMAND.SHIFT_FOCUSED_WINDOW_LEFT },
            new() { keys = [modKey, VK.LSHIFT, VK.RIGHT], command = COMMAND.SHIFT_FOCUSED_WINDOW_RIGHT },

            // Move window to next/prev workspace using ctrl + mod + arrow left/right
            new() { keys = [VK.LCONTROL, modKey, VK.LEFT], command = COMMAND.SHIFT_WINDOW_PREVIOUS_WORKSPACE },
            new() { keys = [VK.LCONTROL, modKey, VK.RIGHT], command = COMMAND.SHIFT_WINDOW_NEXT_WORKSPACE },

            // Switch between workspaces using ctrl + shift + arrow left/right
            new() { keys = [VK.LCONTROL, VK.LSHIFT, VK.LEFT], command = COMMAND.FOCUS_PREVIOUS_WORKSPACE },
            new() { keys = [VK.LCONTROL, VK.LSHIFT, VK.RIGHT], command = COMMAND.FOCUS_NEXT_WORKSPACE },

            // Close window with mod + Q
            new() { keys = [modKey, VK.Q], command = COMMAND.CLOSE_FOCUSED_WINDOW },

            // Minimize window with mod + M
            new() { keys = [modKey, VK.M], command = COMMAND.MINIMIZE_FOCUSED_WINDOW },

            // Toggle floating with mod + F
            new() { keys = [modKey, VK.F], command = COMMAND.TOGGLE_FLOATING_WINDOW },

            // Toggle stacked window with mod + S
            new() { keys = [modKey, VK.S], command = COMMAND.TOGGLE_STACKED_WINDOW },

            // Jump to numbered workspace using mod + number
            new() { keys = [modKey, VK.NUM1], command = COMMAND.FOCUS_WORKSPACE_1 },
            new() { keys = [modKey, VK.NUM2], command = COMMAND.FOCUS_WORKSPACE_2 },
            new() { keys = [modKey, VK.NUM3], command = COMMAND.FOCUS_WORKSPACE_3 },
            new() { keys = [modKey, VK.NUM4], command = COMMAND.FOCUS_WORKSPACE_4 },
            new() { keys = [modKey, VK.NUM5], command = COMMAND.FOCUS_WORKSPACE_5 },
            new() { keys = [modKey, VK.NUM6], command = COMMAND.FOCUS_WORKSPACE_6 },
            new() { keys = [modKey, VK.NUM7], command = COMMAND.FOCUS_WORKSPACE_7 },
            new() { keys = [modKey, VK.NUM8], command = COMMAND.FOCUS_WORKSPACE_8 },
            new() { keys = [modKey, VK.NUM9], command = COMMAND.FOCUS_WORKSPACE_9 },

            // WM commands using ctrl + shift
            new() { keys = [VK.LCONTROL, VK.LSHIFT, VK.R], command = COMMAND.RESTART },
            new() { keys = [VK.LCONTROL, VK.LSHIFT, VK.U], command = COMMAND.UPDATE },
            new() { keys = [VK.LCONTROL, VK.LSHIFT, VK.Q], command = COMMAND.EXIT },
        };
    }

    public string ToJson()
    {
        JsonObject j = new()
        {
            ["layout"] = layout,
            ["left"] = left,
            ["top"] = top,
            ["right"] = right,
            ["bottom"] = bottom,
            ["inner"] = inner,
            ["workspaces"] = workspaces,
            ["workspaceAnimations"] = workspaceAnimations,
            ["workspaceAnimationsDuration"] = workspaceAnimationsDuration,
            ["workspaceAnimationsDirection"] = workspaceAnimationsDirection,
            ["floatingWindowSize"] = floatingWindowSize,
            ["serverPort"] = serverPort,
            ["modKey"] = modKey.ToString(),
            ["windowBorders"] = new JsonObject()
            {
                ["enabled"] = windowBorders.enabled,
                ["activeBorderColor"] = windowBorders.activeBorderColor is bool b
                    ? (JsonNode)b
                    : (JsonNode)(windowBorders.activeBorderColor as string ?? ""),
                ["inactiveBorderColor"] = windowBorders.inactiveBorderColor is bool ib
                    ? (JsonNode)ib
                    : (JsonNode)(windowBorders.inactiveBorderColor as string ?? ""),
            },
            ["rules"] = new JsonArray(
                rules
                    .Select(rule => new JsonObject()
                    {
                        ["type"] = rule.type,
                        ["method"] = rule.method,
                        ["identifierType"] = rule.identifierType,
                        ["identifier"] = rule.identifier,
                    })
                    .ToArray()
            ),
            ["keymaps"] = new JsonArray(
                keymaps
                    .Select(keymap => new JsonObject()
                    {
                        ["keys"] = new JsonArray(
                            keymap.keys.Select(key =>
                                // Replace modKey value with "modKey" placeholder in JSON
                                (JsonNode)(key == modKey ? "modKey" : key.ToString())
                            ).ToArray()
                        ),
                        ["command"] = keymap.command.ToString(),
                        ["arguments"] = new JsonArray(
                            keymap.arguments.Select(arg => (JsonNode)arg).ToArray()
                        ),
                    })
                    .ToArray()
            ),
        };
        return j.ToString();
    }

    public static Config FromJson(string json)
    {
        JsonNode node = JsonNode.Parse(json);

        Config config = new();
        config.layout = node["layout"].ToString();
        config.inner = Convert.ToInt32(node["inner"].ToString());
        config.left = Convert.ToInt32(node["left"].ToString());
        config.top = Convert.ToInt32(node["top"].ToString());
        config.right = Convert.ToInt32(node["right"].ToString());
        config.bottom = Convert.ToInt32(node["bottom"].ToString());
        config.workspaces = Convert.ToInt32(node["workspaces"].ToString());
        // Handle both boolean and string formats (for backwards compatibility)
        var workspaceAnimNode = node["workspaceAnimations"];
        if (workspaceAnimNode is JsonValue jsonValue && jsonValue.TryGetValue<bool>(out bool boolValue))
        {
            config.workspaceAnimations = boolValue;
        }
        else
        {
            // Fallback to string parsing (handles "true"/"True"/"false"/"False")
            config.workspaceAnimations = workspaceAnimNode.ToString().ToLower() == "true";
        }
        config.workspaceAnimationsDuration = Convert.ToInt32(
            node["workspaceAnimationsDuration"].ToString()
        );
        config.workspaceAnimationsDirection = node["workspaceAnimationsDirection"].ToString();
        config.floatingWindowSize = node["floatingWindowSize"].ToString();
        config.serverPort = Convert.ToInt32(node["serverPort"].ToString());

        // Parse modKey (optional, defaults to LWIN if not present)
        if (node["modKey"] != null)
        {
            if (Enum.TryParse<VK>(node["modKey"].ToString(), out VK parsedModKey))
            {
                config.modKey = parsedModKey;
            }
        }

        // Parse window borders configuration (optional, may not exist in old configs)
        if (node["windowBorders"] != null)
        {
            var bordersNode = node["windowBorders"];
            config.windowBorders = new WindowBorderConfig();

            if (bordersNode["enabled"] != null)
            {
                config.windowBorders.enabled =
                    bordersNode["enabled"]?.GetValue<bool>() ?? true;
            }

            // Parse activeBorderColor (can be string or bool)
            if (bordersNode["activeBorderColor"] != null)
            {
                var activeColorNode = bordersNode["activeBorderColor"];
                if (activeColorNode is JsonValue activeVal)
                {
                    if (activeVal.TryGetValue<bool>(out bool activeBool))
                    {
                        config.windowBorders.activeBorderColor = activeBool;
                    }
                    else if (activeVal.TryGetValue<string>(out string? activeStr))
                    {
                        config.windowBorders.activeBorderColor = activeStr;
                    }
                }
            }

            // Parse inactiveBorderColor (can be string or bool)
            if (bordersNode["inactiveBorderColor"] != null)
            {
                var inactiveColorNode = bordersNode["inactiveBorderColor"];
                if (inactiveColorNode is JsonValue inactiveVal)
                {
                    if (inactiveVal.TryGetValue<bool>(out bool inactiveBool))
                    {
                        config.windowBorders.inactiveBorderColor = inactiveBool;
                    }
                    else if (inactiveVal.TryGetValue<string>(out string? inactiveStr))
                    {
                        config.windowBorders.inactiveBorderColor = inactiveStr;
                    }
                }
            }
        }

        config.rules = new();
        JsonArray _rules = node["rules"].AsArray();
        _rules
            .ToList()
            .ForEach(_rule =>
            {
                WindowRule rule = new();

                rule.type = _rule["type"].ToString();
                rule.method = _rule["method"].ToString();
                rule.identifierType = _rule["identifierType"].ToString();
                rule.identifier = _rule["identifier"].ToString();

                config.rules.Add(rule);
            });

        config.keymaps = new();
        JsonArray _keymaps = node["keymaps"].AsArray();
        _keymaps
            .ToList()
            .ForEach(_keymap =>
            {
                Keymap keymap = new();

                // keys
                JsonArray _keys = _keymap["keys"].AsArray();
                _keys
                    .ToList()
                    .ForEach(_key =>
                    {
                        string keyStr = _key.ToString();
                        // Replace "modKey" placeholder with actual modKey value
                        if (keyStr.Equals("modKey", StringComparison.OrdinalIgnoreCase))
                        {
                            keymap.keys.Add(config.modKey);
                        }
                        else
                        {
                            Enum.TryParse<VK>(keyStr, true, out VK vkKey);
                            keymap.keys.Add(vkKey);
                        }
                    });
                // command
                string _command = _keymap["command"].ToString();
                Enum.TryParse<COMMAND>(_command, true, out keymap.command);
                // arguments
                JsonArray _arguments = _keymap["arguments"].AsArray();
                _arguments
                    .ToList()
                    .ForEach(_arg =>
                    {
                        keymap.arguments.Add(_arg.ToString());
                    });

                config.keymaps.Add(keymap);
            });

        return config;
    }
}

public class WindowRule
{
    public string type; // ignore, floating
    public string method; // equals, contains
    public string identifierType; // windowProcess, windowTitle, windowClass
    public string identifier; // search string
}

public class WindowBorderConfig
{
    public bool enabled = true;
    public object activeBorderColor = "#00FF00"; // Can be string (hex color) or bool (false to disable)
    public object inactiveBorderColor = false; // Can be string (hex color) or bool (false to disable)

    /// <summary>
    /// Gets the active border color as a hex string, or null if disabled
    /// </summary>
    public string? GetActiveBorderColor()
    {
        if (activeBorderColor is bool enabled && !enabled)
            return null;
        if (activeBorderColor is string hexColor)
            return hexColor;
        return null;
    }

    /// <summary>
    /// Gets the inactive border color as a hex string, or null if disabled
    /// </summary>
    public string? GetInactiveBorderColor()
    {
        if (inactiveBorderColor is bool enabled && !enabled)
            return null;
        if (inactiveBorderColor is string hexColor)
            return hexColor;
        return null;
    }
}
