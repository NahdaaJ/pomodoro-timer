using PomodoroTimer.Models;
using PomodoroTimer.Services;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static PomodoroTimer.Services.QuoteProvider;

namespace PomodoroTimer;

public partial class MainWindow : Window
{
    private TimerService? _activeTimerService;
    private string _currentTimerType = string.Empty;
    private readonly MediaPlayer _player = new();

    public MainWindow()
    {
        InitializeComponent();

        this.MouseLeftButtonDown += (_, e) =>
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        };

        VolumeSlider.Value = 0.3;
        _player.Volume = VolumeSlider.Value;
    }

    private void StartTimerClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: ButtonData buttonData })
        {
            StopResetTimer();
            _currentTimerType = buttonData.Type;
            CreateNewTimer(buttonData.Duration);
        }
        else
        {
            MessageBox.Show("Incorrect timer button data.");
        }
    }

    private void StopTimerClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Name: "StopButton" })
            StopResetTimer();
        else
            MessageBox.Show("Incorrect stop button data.");
    }

    private void PausePlayTimerClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Name: "PauseButton" } && _activeTimerService != null)
        {
            if (_activeTimerService.IsPaused)
            {
                _activeTimerService.ResumeTimer();
                QuoteBlock.Text = _currentTimerType == "Study"
                    ? QuoteProvider.GetQuote(QuoteType.Study)
                    : QuoteProvider.GetQuote(QuoteType.Break);
                SetPauseButtonImage("pause.png");
            }
            else if (!_activeTimerService.IsPaused)
            {
                _activeTimerService.PauseTimer();
                QuoteBlock.Text = QuoteProvider.PauseTimer;
                SetPauseButtonImage("play.png");
            }
        }
    }

    private void CreateNewTimer(TimeSpan duration)
    {
        _activeTimerService = new TimerService(new DispatcherTimer());
        _activeTimerService.TimerTick += UpdateTimerDisplayBlock;
        _activeTimerService.TimerFinished += TimerFinished;

        switch (_currentTimerType)
        {
            case "Study":
                SetupQuote(UpdateStudyQuote, 1);
                break;

            case "Break":
                SetupQuote(UpdateBreakQuote, 0.3);
                break;

            default:
                throw new ArgumentException($"Unsupported timer type: {_currentTimerType}");
        }

        _activeTimerService.StartTimer(duration);
    }

    private void SetupQuote(Action updateAction, double opacity)
    {
        updateAction();
        _activeTimerService!.QuoteUpdate += updateAction;
        TimerDisplayBlock.Opacity = opacity;
    }

    private void UpdateStudyQuote() =>
        QuoteBlock.Text = QuoteProvider.GetQuote(QuoteType.Study);

    private void UpdateBreakQuote() =>
        QuoteBlock.Text = QuoteProvider.GetQuote(QuoteType.Break);

    private void StopResetTimer()
    {
        _player.Stop();
        _player.Close();

        if (_activeTimerService == null) return;

        _activeTimerService.StopTimer();
        _activeTimerService.TimerTick -= UpdateTimerDisplayBlock;
        _activeTimerService.TimerFinished -= TimerFinished;
        _activeTimerService.QuoteUpdate -= UpdateStudyQuote;
        _activeTimerService.QuoteUpdate -= UpdateBreakQuote;
        _activeTimerService = null;

        TimerDisplayBlock.Text = QuoteProvider.DefaultTimer;
        QuoteBlock.Text = QuoteProvider.DefaultString;
    }

    private void UpdateTimerDisplayBlock(TimeSpan remaining) =>
        TimerDisplayBlock.Text = remaining.ToString(@"hh\:mm\:ss");

    private void TimerFinished()
    {
        QuoteBlock.Text = QuoteProvider.TimerFinishedString;
        TimerDisplayBlock.Text = QuoteProvider.DefaultTimer;
        StopResetTimer();
        _player.Open(new Uri("Assets/Sounds/sound1.wav", UriKind.Relative));
        _player.Play();
        
    }

    private void UpdateVolume(object sender, RoutedPropertyChangedEventArgs<double> e) =>
        _player.Volume = VolumeSlider.Value;

    private void VolumeButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;

        double newValue = Math.Round(VolumeSlider.Value, 1);

        newValue += button.Name switch
        {
            "VolumeUpBtn" when newValue < 1 => 0.1,
            "VolumeDownBtn" when newValue > 0 => -0.1,
            _ => 0
        };

        VolumeSlider.Value = newValue;
        _player.Volume = newValue;
    }

    private void SetPauseButtonImage(string iconFileName)
    {
        PauseButtonImage.Source = new BitmapImage(new Uri($"/Assets/Icons/{iconFileName}", UriKind.Relative));
    }

    private void MinimiseWindow_Click(object sender, RoutedEventArgs e) =>
        this.WindowState = WindowState.Minimized;

    private void CloseWindow_Click(object sender, RoutedEventArgs e) =>
        this.Close();

    private void OpenGithub(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://github.com/nahdaaj",
            UseShellExecute = true
        });
    }
}
