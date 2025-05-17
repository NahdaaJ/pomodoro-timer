# Pomodoro Timer 🍅
<div align="centre">
  <a href="https://github.com/NahdaaJ/pomodoro-timer/releases/download/v1.0.0/PomodoroTimer_Installer.exe">
    <img src="https://img.shields.io/badge/Download%20for%20Windows-.exe-C08585?style=for-the-badge" alt="Download Badge"/>
  </a>
</div>
A simple pomodoro timer desktop app for study sessions! :) <br />
Built with WPF and .NET 8.

## Appearance
To design the appearance of the app, I learn how to use the basic functions of Figma. Below is the most up to date design of the app!
<div align="center">
  <img src="ReadmeIcons\app-image.png" height="400"/>
</div>

## Features
- **Preset Study and Break Timers** - Quick start buttons for common study (15, 20, 25, 50 min) and break (5, 10, 30, 60 min) intervals.
- **Motivational Quotes** - Displays a random motivational quote for study or break sessions.
- **Pause, Resume, Stop** - Easily pause, resume, or stop your timer.
- **Sound Notification** - Plays a sound when the timer finishes.
- **Custom Volume Control** - Adjust timer alarm volume with slider or buttons.
- **Minimal, Custom UI** - Clean, borderless window with custom icons and rounded corners.

## Getting Started
### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows OS (WPF is Windows-only)

### Build & Run

1. **Clone the repository**
    ```bash
    git clone https://github.com/nahdaaj/pomodoro-timer.git
    cd pomodoro-timer
    ```
2. **Build the project** - Open the solution in Visual Studio and build the solution.
3. **Run the app** - Run the app using the play button at the top of Visual Studio.

## Project Structure

- `PomodoroTimer/`
  - `MainWindow.xaml` - Main UI layout (WPF XAML)
  - `MainWindow.xaml.cs` - Main window logic (C#)
  - `Models/` - Data models (e.g., `ButtonData`)
  - `Services/` - Timer and quote logic
  - `Assets/` - Icons, fonts, and sound files

## Customization

- **Quotes**- Edit `Services/QuoteProvider.cs` to add or change motivational quotes.

- **Sounds** - Replace `Assets/Sounds/sound1.wav` with your own notification sound.

- **Icons & Colors** - Update icons in `Assets/Icons/` and styles in your XAML resources.

## What I've Learnt During This Project
- Learnt what events are and how to utilise them in my app
- How to use WPF to create apps, including UI/UX and code behind
- Implementing MVVM patterns in C#
- Creating and applying custom styles and templates in XAML
- Managing media playback and volume in .NET
- Handling resource dictionaries and theming
- Writing clean, maintainable code
- How to use the DispatcherTimer to create timers and other time related logic.

## License

This project is provided for personal and educational use.  
For other uses, please contact the author.

### Created by **Nahdaa Jawed**
<div>
<a href="mailto:nahdaajawed@gmail.com" target="_blank">
  <img src="ReadmeIcons\email.png" height="45"/>
</a>
<a href="https://www.linkedin.com/in/nahdaa-jawed/" target="_blank">
  <img src="ReadmeIcons\linkedin.png" height="45" style="margin-right:10px;margin-left:10px"/>
</a>
<a href="https://github.com/NahdaaJ" target="_blank">
  <img src="ReadmeIcons\github.png" height="45"/>
</a>
  
</div>
