using PomodoroTimer.Models;
using PomodoroTimer.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PomodoroTimer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private DispatcherTimer dispatchTimer;
    private TimerService _activeTimerService;

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


        //dispatchTimer = new DispatcherTimer();
        //dispatchTimer.Interval = TimeSpan.FromMinutes(5);
        //dispatchTimer.Tick += UpdateQuote;


        //dispatchTimer.Start();
    }

    private void UpdateQuote(object sender, EventArgs e)
    {
        QuoteBlock.Text = QuoteProvider.GetStudyQuote();

    }

    private void StartTimerClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ButtonData buttonData)
        {
            StopResetTimer(); // Stop any existing timer

            QuoteBlock.Text = $"Starting {buttonData.Type} Timer for {buttonData.Duration.ToString()}";
            
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
            }
            else
            {
                _activeTimerService.PauseTimer();
            }
        }
    }

    private void CreateNewTimer(TimeSpan duration)
    {
        _activeTimerService = new TimerService(new DispatcherTimer());
        _activeTimerService.TimerTick += UpdateTimerDisplayBlock;
        _activeTimerService.TimerFinished += TimerFinished;
        _activeTimerService.StartTimer(duration);
    }

    private void StopResetTimer()
    {
        if (_activeTimerService != null)
        {
            _activeTimerService.StopTimer();
            _activeTimerService.TimerTick -= UpdateTimerDisplayBlock;
            _activeTimerService.TimerFinished -= TimerFinished;
            TimerDisplayBlock.Text = "00:00";
            QuoteBlock.Text = "Let's Go!";
        }
    }

    private void UpdateTimerDisplayBlock(TimeSpan remaining)
    {
        TimerDisplayBlock.Text = remaining.ToString(@"mm\:ss");
    }

    private void TimerFinished()
    {
        QuoteBlock.Text = "Time's up! Take a break.";
        TimerDisplayBlock.Text = "00:00";
    }

}