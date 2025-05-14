using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Services
{
    public static class QuoteProvider
    {
        private static readonly string _defaultString = "Let's Go!";
        private static readonly string _timerFinishedString = "Time's up! Take a break.";
        private static readonly string _defaultTimer = "00:00:00";
        private static readonly List<string> studyQuotes = new List<string>
        {
            "Progress, not perfection.",
            "Small steps every day lead to big change.",
            "You don’t need to finish everything. You just need to keep going.",
            "Every expert was once a beginner.",
            "Learning is not linear. Keep going.",
            "Today’s effort builds tomorrow’s skill.",
            "Your future self is watching and thanking you.",
            "It’s okay to go slow. Just don’t stop.",
            "Studying isn’t punishment—it’s self-investment.",
            "Do it for your future self.",
            "Studying doesn't suck as much as failing.",
            "Imagine where you would be next year if you start now.",
            "Nothing changes if nothing changes.",
            "Remember the goal.",
            "Success is a decision.",
            "Be stronger than your excuses.",
            "Don't wish for it, work for it.",
            "Small steps are better than no steps.",
            "Keep going, because you did not come this far just to come this far.",
        };

        private static readonly List<string> breakQuotes = new List<string>
        {
            "Test1",
            "Test2",
            "Test3",
            "Test4",
            "Test5",
            "Test6",
        };

        private static readonly Random random = new Random();

        public enum QuoteType
        {
            Study,
            Break
        }

        public static string GetQuote(QuoteType quoteType)
        {
            return quoteType switch
            {
                QuoteType.Study => studyQuotes[random.Next(studyQuotes.Count)],
                QuoteType.Break => breakQuotes[random.Next(breakQuotes.Count)],
                _ => string.Empty
            };
        }

        public static string DefaultString => _defaultString;
        public static string TimerFinishedString => _timerFinishedString;
        public static string DefaultTimer => _defaultTimer;

    }
}
