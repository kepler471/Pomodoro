# (WIP)

Currently this does not have a timer implemented, the repo only includes some preliminary GUIs.

# Pomodoro
A simple productivity timer for Windows. Inspired by the [gnome-pomodoro shell extension](https://gnomepomodoro.org/).

The app will run as a tray icon application, and will create a full screen overlay when the user should take a break.

This overlay will be easily bypassed by simply clicking if the user needs to use their computer.

Aiming for cross-platform but is currently tested for Windows only.



└── Pomodoro

    ├── Pomodoro
    │   ├── Pomodoro
    │   │   ├── AnalogClock.fs
    │   │   ├── MainForm.fs
    │   │   └── Pomodoro.fsproj
    │   ├── Pomodoro.Desktop
    │   │   ├── Info.plist
    │   │   ├── MacIcon.icns
    │   │   ├── Pomodoro.Desktop.fsproj
    │   │   └── Program.fs
    │   └── Pomodoro.Tests
    │       ├── Pomodoro.Tests.fsproj
    │       ├── Program.fs
    │       ├── TestFormCommand.fs
    │       ├── TestStackLayout.fs
    │       └── TestTableLayout.fs
    ├── Pomodoro.sln
    └── README.md

