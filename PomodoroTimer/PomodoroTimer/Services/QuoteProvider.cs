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
        private static readonly string _pauseTimer = "Timer Paused";

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
            "Work hard in silence, let success make the noise.",
            "The secret to getting ahead is getting started.",
            "Dreams don’t work unless you do.",
            "It’s not about having time. It’s about making time.",
            "I am focused. I am capable.",
            "One step at a time.",
            "The best time to start was in the past, the second best time is now.",
            "Done is better than perfect.",
            "A little progress each day adds up to big results.",
            "Start where you are. Use what you have. Do what you can.",
            "Don’t let perfection paralyze progress.",
            "You're closer than you think.",
            "Consistency beats intensity.",
            "You're not behind. You're just building.",
            "Focus on the next step, not the whole staircase.",
            "Effort compounds. Keep going."
        };

        private static readonly List<string> breakQuotes = new List<string>
        {
            "Sometimes the most productive thing you can do is relax.",
            "Take a deep breath. You’re doing great.",
            "Rest, recover, recharge. You’ll come back stronger.",
            "Breaks help your brain bloom.",
            "Even a short rest resets your mind.",
            "You're doing great so far.",
            "Take a deep breath.",
            "I deserve to take care of myself.",
            "Breathe in calm, breathe out stress.",
            "Step away, so you can step back stronger.",
            "You’re allowed to pause. It’s part of the process.",
            "This break is your reset button.",
            "Close your eyes. Smile. You're okay.",
            "Relax your shoulders. Unclench your jaw.",
            "Your brain needs this. Enjoy it.",
            "Rest is fuel for your next win.",
            "Let your thoughts wander. That’s how creativity is born.",
            "Peace is productive too.",
            "This moment of rest is your gift to yourself.",
            "Slow down so you can speed up later.",
            "You're recharging—not wasting time.",
            "Let your mind breathe.",
            "Stillness is strength.",
            "You’ve earned this moment of peace.",
            "Sip some water. Stretch a little.",
            "Be proud of how far you've come—breaks included.",
            "Give your mind space to grow.",
            "A calm mind is a sharp mind.",
            "Even machines need to cool down. So do you."
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
        public static string PauseTimer => _pauseTimer;

    }
}
