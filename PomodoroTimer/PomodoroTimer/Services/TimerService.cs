using System;
using System.Windows.Threading;

namespace PomodoroTimer.Services
{
    public class TimerService
    {
        private readonly DispatcherTimer _timer;
        private TimeSpan _remaining;
        private int _secondsSinceLastQuote;

        public bool IsPaused { get; private set; }

        public event Action? TimerFinished;
        public event Action<TimeSpan>? TimerTick;
        public event Action? QuoteUpdate;

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
                TimerFinished?.Invoke();
            }
            else
            {
                TimerTick?.Invoke(_remaining);
            }

            if (_secondsSinceLastQuote >= 300)
            {
                QuoteUpdate?.Invoke();
                _secondsSinceLastQuote = 0;
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
            IsPaused = true;
        }

        public void ResumeTimer()
        {
            _timer.Start();
            TimerTick?.Invoke(_remaining);
            IsPaused = false;
        }
    }
}
