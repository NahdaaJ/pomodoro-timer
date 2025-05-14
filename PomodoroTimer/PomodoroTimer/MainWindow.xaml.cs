using PomodoroTimer.Models;
using PomodoroTimer.Services;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static PomodoroTimer.Services.QuoteProvider;

namespace PomodoroTimer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private DispatcherTimer dispatchTimer;
    private TimerService? _activeTimerService;
    private string _currentTimerType = string.Empty;
    private SoundPlayer _player;

    public MainWindow()
    {
        InitializeComponent();

        this.MouseLeftButtonDown += (sender, e) =>
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        };

        _player = new SoundPlayer("./Assets/Sounds/sound1.wav");
    }

    private void StartTimerClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ButtonData buttonData)
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
        if (sender is Button button && button.Name == "StopButton")
        {
            StopResetTimer();
        }
        else
        {
            MessageBox.Show("Incorrect stop button data.");
        }
    }

    private void PausePlayTimerClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Name == "PauseButton" && _activeTimerService != null)
        {
            if (_activeTimerService.isPaused == true)
            {
                _activeTimerService.ResumeTimer();
                QuoteBlock.Text = _currentTimerType == "Study"
                    ? QuoteProvider.GetQuote(QuoteType.Study)
                    : QuoteProvider.GetQuote(QuoteType.Break);
            }
            else
            {
                _activeTimerService.PauseTimer();
                QuoteBlock.Text = QuoteProvider.PauseTimer;
            }
        }
    }

    private void CreateNewTimer(TimeSpan duration)
    {
        _activeTimerService = new TimerService(new DispatcherTimer());
        _activeTimerService.TimerTick += UpdateTimerDisplayBlock;
        _activeTimerService.TimerFinished += TimerFinished;


        // cannot use functions with parameters if i want to unsubscribe later
        if (_currentTimerType == "Study")
        {
            UpdateStudyQuote();
            _activeTimerService.QuoteUpdate += UpdateStudyQuote;
            TimerDisplayBlock.Opacity = 1;

        }
        else if (_currentTimerType == "Break")
        {
            UpdateBreakQuote();
            _activeTimerService.QuoteUpdate += UpdateBreakQuote;
            TimerDisplayBlock.Opacity = 0.3;
        }
        else
        {
            throw new ArgumentException($"Unsupported timer type: {_currentTimerType}");
        }
            _activeTimerService.StartTimer(duration);
    }
    private void UpdateStudyQuote()
    {
        QuoteBlock.Text = QuoteProvider.GetQuote(QuoteType.Study);
    }

    private void UpdateBreakQuote()
    {
        QuoteBlock.Text = QuoteProvider.GetQuote(QuoteType.Break);
    }

    private void StopResetTimer()
    {
        if (_activeTimerService != null)
        {
            _activeTimerService.StopTimer();
            _activeTimerService.TimerTick -= UpdateTimerDisplayBlock;
            _activeTimerService.TimerFinished -= TimerFinished;
            _activeTimerService.QuoteUpdate -= UpdateStudyQuote;
            _activeTimerService.QuoteUpdate -= UpdateBreakQuote;
            TimerDisplayBlock.Text = QuoteProvider.DefaultTimer;
            QuoteBlock.Text = QuoteProvider.DefaultString;
            _player.Stop();
        }
    }

    private void UpdateTimerDisplayBlock(TimeSpan remaining)
    {
        TimerDisplayBlock.Text = remaining.ToString(@"hh\:mm\:ss");
    }

    private void TimerFinished()
    {
        QuoteBlock.Text = QuoteProvider.TimerFinishedString;
        TimerDisplayBlock.Text = QuoteProvider.DefaultTimer;
        _player.Play();
    }

}