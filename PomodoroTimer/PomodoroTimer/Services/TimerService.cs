using System.Windows.Threading;

namespace PomodoroTimer.Services
{
    public class TimerService
    {
        private DispatcherTimer _timer; // ticks every second
        private TimeSpan _remaining; // keeps track of how much time is left
        public bool isPaused = false; // keeps track of whether the timer is paused
        private int _secondsSinceLastQuote = 0; // keeps track of how many seconds have passed since the last quote update

        public event Action? TimerFinished; // runs when timer ends
        public event Action<TimeSpan>? TimerTick; // runs every second to update the remaining time
        public event Action? QuoteUpdate; // runs every 5 minutes to update the quote


        public TimerService(DispatcherTimer timer)
        {
            _timer = timer;
        }

        public void StartTimer(TimeSpan duration)
        {
            _remaining = duration;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick -= Timer_Tick;
            _timer.Tick += Timer_Tick;
            _timer.Start();

            TimerTick?.Invoke(_remaining);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _remaining = _remaining.Subtract(TimeSpan.FromSeconds(1));
            _secondsSinceLastQuote++;

            if (_remaining <= TimeSpan.Zero)
            {
                _timer.Stop();
                TimerFinished?.Invoke(); // Raise the event
            }
            else
            {
                TimerTick?.Invoke(_remaining); // Notify remaining time
            }

            if (_secondsSinceLastQuote >= 300)
            {
                QuoteUpdate?.Invoke(); // Notify to update quote
                _secondsSinceLastQuote = 0; // Reset the counter
            }
        }
        
        public void StopTimer()
        {
            _timer.Stop();
            _remaining = TimeSpan.Zero;
        }

        public void PauseTimer()
        {
            _timer.Stop();
            isPaused = true;
        }

        public void ResumeTimer()
        {
            _timer.Start();
            TimerTick?.Invoke(_remaining);
            isPaused = false;
        }

    }
}
