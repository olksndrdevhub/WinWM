clear

# Create bin directory if it doesn't exist
if (-Not (Test-Path "bin")) {
	New-Item -ItemType Directory -Path "bin" | Out-Null
}

# Remove old executable if it exists
if (Test-Path "bin\winwm.exe") {
	Remove-Item "bin\winwm.exe" -Force
}

$target = "exe"
if($args[0] -eq "winexe") {
	$target = "winexe"
}

dflat Main.cs `
	  Classes\Core\Interfaces\IWinWM.cs `
	  Classes\Core\Interfaces\IJson.cs `
	  Classes\Core\Interfaces\IAnimation.cs `
	  Classes\Core\WinWM.cs `
	  Classes\Core\Config.cs `
	  Classes\Core\Globals.cs `
	  Classes\Core\Layouts.cs `
	  Classes\Core\Logger.cs `
	  Classes\Core\Paths.cs `
	  Classes\Core\Server.cs `
	  Classes\Core\State.cs `
	  Classes\Core\Utils.cs `
	  Classes\Core\Animation.cs `
	  Classes\Core\BorderHelper.cs `
	  Classes\Hooks\Keys.cs `
	  Classes\Hooks\Mouse.cs `
	  Classes\Hooks\Windows.cs `
	  Classes\Win32\Delegates.cs `
	  Classes\Win32\Enums.cs `
	  Classes\Win32\Functions.cs `
	  Classes\Win32\Structs.cs `
	  /target:$target `
	  /out winwm.exe `

# Only move if the build succeeded
if (Test-Path "winwm.exe") {
	Move-Item "winwm.exe" "bin\winwm.exe" -Force
	Write-Host "Build successful! Output: bin\winwm.exe" -ForegroundColor Green
} else {
	Write-Host "Build failed or dflat not found. Try using 'dotnet build' instead." -ForegroundColor Yellow
	exit 1
}
