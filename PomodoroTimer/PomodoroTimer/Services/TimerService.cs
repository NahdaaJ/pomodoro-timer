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
        private DispatcherTimer _timer;
        public event Action? TimerFinished;

        public TimerService(DispatcherTimer timer)
        {
            _timer = timer;
        }

        public void StartTimer(TimeSpan duration)
        {
            _timer.Interval = duration;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop();
            TimerFinished?.Invoke();
        }

    }
}
