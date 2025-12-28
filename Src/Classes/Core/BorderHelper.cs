using System;

/// <summary>
/// Utility class for managing window border colors on Windows 11
/// </summary>
public static class BorderHelper
{
    // Cached Windows version check result
    private static bool? _supportsWindowsBorders = null;

    /// <summary>
    /// Checks if the current Windows version supports DWM border colors (Windows 11 Build 22000+)
    /// </summary>
    public static bool SupportsWindowBorders()
    {
        if (_supportsWindowsBorders == null)
        {
            // Windows 11 starts at build 22000
            int buildNumber = Environment.OSVersion.Version.Build;
            _supportsWindowsBorders = buildNumber >= 22000;

            if (!_supportsWindowsBorders.Value)
            {
                Logger.Log(
                    $"Window border colors are not supported on this Windows version (Build {buildNumber}). "
                    + "Requires Windows 11 Build 22000 or later. Border feature will be disabled."
                );
            }
        }

        return _supportsWindowsBorders.Value;
    }

    /// <summary>
    /// Converts a hex color string (e.g., "#00FF00" or "00FF00") to COLORREF format (0x00BBGGRR)
    /// </summary>
    /// <param name="hexColor">Hex color string like "#00FF00"</param>
    /// <returns>COLORREF integer value</returns>
    public static int HexToColorRef(string hexColor)
    {
        // Remove # if present
        hexColor = hexColor.TrimStart('#');

        if (hexColor.Length != 6)
        {
            throw new ArgumentException(
                $"Invalid hex color format: '{hexColor}'. Expected format: #RRGGBB or RRGGBB"
            );
        }

        try
        {
            // Parse RGB components
            int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);

            // Convert to COLORREF format (0x00BBGGRR) - note the reversed byte order
            return (b << 16) | (g << 8) | r;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(
                $"Failed to parse hex color '{hexColor}': {ex.Message}",
                ex
            );
        }
    }

    /// <summary>
    /// Special constant to reset border color to system default
    /// </summary>
    public const int DWMWA_COLOR_DEFAULT = unchecked((int)0xFFFFFFFF);

    /// <summary>
    /// Sets the border color of a window (Windows 11+ only)
    /// </summary>
    /// <param name="hWnd">Window handle</param>
    /// <param name="hexColor">Hex color string like "#00FF00"</param>
    /// <returns>True if successful, false otherwise</returns>
    public static bool SetBorderColor(nint hWnd, string hexColor)
    {
        // Check Windows version compatibility
        if (!SupportsWindowBorders())
        {
            return false;
        }

        // Validate window handle before attempting to set border
        if (!User32.IsWindow(hWnd))
        {
            return false;
        }

        try
        {
            // Convert hex to COLORREF
            int colorRef = HexToColorRef(hexColor);

            // Call DWM API
            int result = Dwmapi.DwmSetWindowAttribute(
                hWnd,
                DWMWINDOWATTRIBUTE.DWMWA_BORDER_COLOR,
                ref colorRef,
                sizeof(int)
            );

            if (result != 0)
            {
                Logger.Log(
                    $"DwmSetWindowAttribute failed with HRESULT: 0x{result:X8} for window {hWnd}"
                );
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Logger.Log($"Failed to set border color for window {hWnd}: {ex.Message}", ex: ex);
            return false;
        }
    }

    /// <summary>
    /// Resets the border color to system default
    /// </summary>
    /// <param name="hWnd">Window handle</param>
    /// <returns>True if successful, false otherwise</returns>
    public static bool ResetBorderColor(nint hWnd)
    {
        if (!SupportsWindowBorders())
        {
            return false;
        }

        // Validate window handle before attempting to reset border
        if (!User32.IsWindow(hWnd))
        {
            return false;
        }

        try
        {
            int defaultColor = DWMWA_COLOR_DEFAULT;

            int result = Dwmapi.DwmSetWindowAttribute(
                hWnd,
                DWMWINDOWATTRIBUTE.DWMWA_BORDER_COLOR,
                ref defaultColor,
                sizeof(int)
            );

            if (result != 0)
            {
                Logger.Log(
                    $"DwmSetWindowAttribute (reset) failed with HRESULT: 0x{result:X8} for window {hWnd}"
                );
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Logger.Log($"Failed to reset border color for window {hWnd}: {ex.Message}", ex: ex);
            return false;
        }
    }
}
