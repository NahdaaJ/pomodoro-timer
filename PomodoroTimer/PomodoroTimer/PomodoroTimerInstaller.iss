; PomodoroTimer Inno Setup Script
; Requires Inno Setup 6.x or later

[Setup]
AppName=Pomodoro Timer
AppVersion=1.0.0
DefaultDirName={autopf}\PomodoroTimer
DefaultGroupName=PomodoroTimer
UninstallDisplayIcon={app}\PomodoroTimer.exe
OutputDir=OutputInstaller
OutputBaseFilename=PomodoroTimer_Installer
Compression=lzma2
SolidCompression=yes

WizardStyle=modern
AppPublisher=Nahdaa Jawed
AppPublisherURL=https://github.com/NahdaaJ
AppSupportURL=https://github.com/NahdaaJ/pomodoro-timer
AppUpdatesURL=https://github.com/NahdaaJ/pomodoro-timer/releases

[Files]
; Main application and dependencies
Source: "bin\Release\net8.0-windows\PomodoroTimer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\*.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\*.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\DesktopIcon.ico"; DestDir: "{app}"; Flags: ignoreversion

; Assets (fonts, icons, sounds, etc.)
Source: "bin\Release\net8.0-windows\Assets\*"; DestDir: "{app}\Assets"; Flags: ignoreversion recursesubdirs createallsubdirs


[Icons]
Name: "{group}\Pomodoro Timer"; Filename: "{app}\PomodoroTimer.exe"; IconFilename: "{app}\DesktopIcon.ico"
Name: "{commondesktop}\Pomodoro Timer"; Filename: "{app}\PomodoroTimer.exe"; IconFilename: "{app}\DesktopIcon.ico"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop shortcut"; GroupDescription: "Additional icons:"

[Run]
Filename: "{app}\PomodoroTimer.exe"; Description: "Launch PomodoroTimer"; Flags: nowait postinstall skipifsilent

