using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Models
{
    public record class ButtonData
    {
        public TimeSpan Duration { get; init; }
        public string Type { get; init; } = string.Empty;
    }
}
