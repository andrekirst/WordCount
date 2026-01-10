using System;
using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Goals;
using WordCount.Models.Results;

namespace WordCount.Implementations.Output
{
    public class GoalOutput : IGoalOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private const int ProgressBarWidth = 30;
        private const int SeparatorWidth = 50;
        private const int SmallSeparatorWidth = 30;
        private const int MilestoneSeparatorWidth = 40;
        private const int ChartWidth = 40;
        private const int DaysToShow = 7;

        public GoalOutput(
            IDisplayOutput displayOutput,
            ILanguageResource languageResource,
            ILanguageDecision languageDecision)
        {
            _displayOutput = displayOutput;
            // Note: languageResource and languageDecision are available for future localization
        }

        public void DisplayGoalProgress(GoalProgressResult progress)
        {
            if (progress == null)
            {
                _displayOutput.WriteResourceLine("GOAL_NOT_SET");
                return;
            }

            _displayOutput.WriteLine();
            _displayOutput.WriteResourceLine("GOAL_PROGRESS");
            _displayOutput.WriteLine(new string('=', SeparatorWidth));

            var goalTypeText = GetGoalTypeText(progress.Goal.Type);
            _displayOutput.WriteLine($"  {goalTypeText}: {progress.Goal.TargetWordCount:N0} words");

            if (!string.IsNullOrEmpty(progress.Goal.ProjectName))
            {
                _displayOutput.WriteLine($"  Project: {progress.Goal.ProjectName}");
            }

            _displayOutput.WriteLine();

            // Progress bar
            var progressBar = CreateProgressBar(progress.PercentageComplete);
            _displayOutput.WriteLine($"  {progressBar} {progress.PercentageComplete:F1}%");
            _displayOutput.WriteLine();

            // Statistics
            _displayOutput.WriteLine($"  Current: {progress.CurrentWordCount:N0} / {progress.TargetWordCount:N0} words");
            _displayOutput.WriteLine($"  Remaining: {progress.WordsRemaining:N0} words");

            if (progress.Goal.Type != GoalType.Daily && progress.DaysRemaining > 0)
            {
                _displayOutput.WriteLine($"  Days remaining: {progress.DaysRemaining}");
                _displayOutput.WriteLine($"  Required/day: {progress.RequiredWordsPerDayToComplete:N0} words");
            }

            if (progress.AverageWordsPerDay > 0)
            {
                _displayOutput.WriteLine($"  Avg words/day: {progress.AverageWordsPerDay:N1}");
            }

            if (progress.IsCompleted)
            {
                _displayOutput.WriteLine();
                DisplayCompletionCelebration();
            }

            _displayOutput.WriteLine(new string('=', SeparatorWidth));
            _displayOutput.WriteLine();
        }

        public void DisplayStreak(Streak streak)
        {
            _displayOutput.WriteLine();
            _displayOutput.WriteResourceLine("STREAK_INFO");
            _displayOutput.WriteLine(new string('-', SmallSeparatorWidth));

            // Display streak fire emojis based on streak length
            var fireSymbol = GetStreakSymbol(streak.CurrentStreak);
            _displayOutput.WriteLine($"  {fireSymbol} Current streak: {streak.CurrentStreak} days");
            _displayOutput.WriteLine($"  Best streak: {streak.LongestStreak} days");

            if (streak.LastActivityDate.HasValue)
            {
                _displayOutput.WriteLine($"  Last activity: {streak.LastActivityDate.Value:MMM dd, yyyy}");
            }

            _displayOutput.WriteLine();
        }

        public void DisplayMilestones(IEnumerable<Milestone> milestones)
        {
            var milestoneList = milestones.ToList();
            if (!milestoneList.Any())
            {
                return;
            }

            _displayOutput.WriteLine();
            _displayOutput.WriteResourceLine("NEW_MILESTONES");
            _displayOutput.WriteLine(new string('*', MilestoneSeparatorWidth));

            foreach (var milestone in milestoneList)
            {
                var trophy = GetMilestoneTrophy(milestone.Type);
                _displayOutput.WriteLine($"  {trophy} {milestone.Description}");
            }

            _displayOutput.WriteLine(new string('*', MilestoneSeparatorWidth));
            _displayOutput.WriteLine();
        }

        public void DisplayGoalSummary(GoalData data)
        {
            _displayOutput.WriteLine();
            _displayOutput.WriteResourceLine("GOAL_SUMMARY");
            _displayOutput.WriteLine(new string('=', SeparatorWidth));

            _displayOutput.WriteLine($"  Total words written: {data.TotalWordsWritten:N0}");
            _displayOutput.WriteLine($"  Goals completed: {data.TotalGoalsCompleted}");
            _displayOutput.WriteLine($"  Total milestones: {data.Milestones.Count}");

            _displayOutput.WriteLine();
            _displayOutput.WriteLine("  Active Goals:");

            var activeGoals = data.Goals.Where(g => g.IsActive).ToList();
            if (!activeGoals.Any())
            {
                _displayOutput.WriteLine("    (none)");
            }
            else
            {
                foreach (var goal in activeGoals)
                {
                    var typeText = GetGoalTypeText(goal.Type);
                    _displayOutput.WriteLine($"    - {typeText}: {goal.TargetWordCount:N0} words");
                }
            }

            _displayOutput.WriteLine(new string('=', SeparatorWidth));
            _displayOutput.WriteLine();
        }

        public void DisplayProgressChart(GoalData data, string goalId)
        {
            var entries = data.ProgressEntries
                .Where(p => goalId == null || p.GoalId == goalId)
                .GroupBy(p => p.Date.Date)
                .OrderByDescending(g => g.Key)
                .Take(DaysToShow)
                .Reverse()
                .ToList();

            if (!entries.Any())
            {
                return;
            }

            _displayOutput.WriteLine();
            _displayOutput.WriteResourceLine("PROGRESS_CHART");
            _displayOutput.WriteLine($"  Last {DaysToShow} days activity:");
            _displayOutput.WriteLine();

            var maxWords = entries.Max(e => e.Sum(x => x.WordCount));
            if (maxWords == 0) maxWords = 1;

            foreach (var entry in entries)
            {
                var dayWords = entry.Sum(x => x.WordCount);
                var barLength = (int)((double)dayWords / maxWords * ChartWidth);
                var bar = new string('#', Math.Max(1, barLength));
                var dayLabel = entry.Key.ToString("ddd");

                _displayOutput.WriteLine($"  {dayLabel} |{bar.PadRight(ChartWidth)} {dayWords:N0}");
            }

            _displayOutput.WriteLine();
        }

        private string CreateProgressBar(double percentage)
        {
            var filled = (int)(percentage / 100 * ProgressBarWidth);
            filled = Math.Max(0, Math.Min(ProgressBarWidth, filled));
            var empty = ProgressBarWidth - filled;

            var filledChar = '#';
            var emptyChar = '-';

            return $"[{new string(filledChar, filled)}{new string(emptyChar, empty)}]";
        }

        private void DisplayCompletionCelebration()
        {
            _displayOutput.WriteLine("  ****************************");
            _displayOutput.WriteResourceLine("GOAL_COMPLETED");
            _displayOutput.WriteLine("  ****************************");
        }

        private string GetGoalTypeText(GoalType type)
        {
            return type switch
            {
                GoalType.Daily => "Daily Goal",
                GoalType.Weekly => "Weekly Goal",
                GoalType.Project => "Project Goal",
                _ => "Goal"
            };
        }

        private string GetStreakSymbol(int streak)
        {
            if (streak >= 30) return "[*****]";
            if (streak >= 14) return "[****]";
            if (streak >= 7) return "[***]";
            if (streak >= 3) return "[**]";
            if (streak >= 1) return "[*]";
            return "[ ]";
        }

        private string GetMilestoneTrophy(MilestoneType type)
        {
            return type switch
            {
                MilestoneType.TotalWords100000 or MilestoneType.ThirtyDayStreak or MilestoneType.GoalsCompleted25 => "[GOLD]",
                MilestoneType.TotalWords50000 or MilestoneType.SevenDayStreak or MilestoneType.GoalsCompleted10 => "[SILVER]",
                MilestoneType.TotalWords10000 or MilestoneType.ThreeDayStreak or MilestoneType.GoalsCompleted5 => "[BRONZE]",
                _ => "[+]"
            };
        }
    }
}
