using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PomodoroTimer.Services
{
    public class TimerService
    {
        private DispatcherTimer _timer; // ticks every second
        private TimeSpan _remaining; // keeps track of how much time is left

        public event Action? TimerFinished; // runs when timer ends
        public event Action<TimeSpan>? TimerTick; // runs every second to update the remaining time

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

            if (_remaining <= TimeSpan.Zero)
            {
                _timer.Stop();
                TimerFinished?.Invoke(); // Raise the event
            }
            else
            {
                TimerTick?.Invoke(_remaining); // Notify remaining time
            }
        }
        
        public void StopTimer()
        {
            _timer.Stop();
            _remaining = TimeSpan.Zero;
        }

    }
}
