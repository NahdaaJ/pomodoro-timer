using PomodoroTimer.Models;
using PomodoroTimer.Services;
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
    }

    private void StartTimerClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ButtonData buttonData)
        {
            StopResetTimer();            
            CreateNewTimer(buttonData.Duration, buttonData.Type);
            
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
            }
            else
            {
                _activeTimerService.PauseTimer();
            }
        }
    }

    private void CreateNewTimer(TimeSpan duration, string quoteType)
    {
        _activeTimerService = new TimerService(new DispatcherTimer());
        _activeTimerService.TimerTick += UpdateTimerDisplayBlock;
        _activeTimerService.TimerFinished += TimerFinished;


        // cannot use functions with parameters if i want to unsubscribe later
        if (quoteType == "Study")
        {
            UpdateStudyQuote();
            _activeTimerService.QuoteUpdate += UpdateStudyQuote;

        }
        else if (quoteType == "Break")
        {
            UpdateBreakQuote();
            _activeTimerService.QuoteUpdate += UpdateBreakQuote;

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
    }

}