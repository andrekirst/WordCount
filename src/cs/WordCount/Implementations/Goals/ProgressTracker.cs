using System;
using System.Linq;
using WordCount.Interfaces.Goals;
using WordCount.Models.Goals;
using WordCount.Models.Results;

namespace WordCount.Implementations.Goals
{
    public class ProgressTracker : IProgressTracker
    {
        public void RecordProgress(GoalData data, int wordCount, string goalId, string sourceFile = null)
        {
            var entry = new ProgressEntry
            {
                GoalId = goalId,
                WordCount = wordCount,
                SourceFile = sourceFile,
                Date = DateTime.UtcNow
            };

            data.ProgressEntries.Add(entry);
            data.TotalWordsWritten += wordCount;
        }

        public int GetProgressForPeriod(GoalData data, string goalId, DateTime startDate, DateTime endDate)
        {
            return data.ProgressEntries
                .Where(p => p.GoalId == goalId &&
                           p.Date >= startDate &&
                           p.Date <= endDate)
                .Sum(p => p.WordCount);
        }

        public int GetTodayProgress(GoalData data, string goalId)
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);
            return GetProgressForPeriod(data, goalId, today, tomorrow);
        }

        public int GetWeekProgress(GoalData data, string goalId)
        {
            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            return GetProgressForPeriod(data, goalId, startOfWeek, endOfWeek);
        }

        public double GetAverageWordsPerDay(GoalData data, string goalId)
        {
            var goalEntries = data.ProgressEntries.Where(p => p.GoalId == goalId).ToList();

            if (!goalEntries.Any())
            {
                return 0;
            }

            var firstEntry = goalEntries.Min(p => p.Date);
            var lastEntry = goalEntries.Max(p => p.Date);
            var days = (lastEntry - firstEntry).Days + 1;

            if (days <= 0)
            {
                days = 1;
            }

            var totalWords = goalEntries.Sum(p => p.WordCount);
            return (double)totalWords / days;
        }

        public GoalProgressResult CalculateGoalProgress(GoalData data, Goal goal)
        {
            var result = new GoalProgressResult
            {
                Goal = goal,
                TargetWordCount = goal.TargetWordCount
            };

            switch (goal.Type)
            {
                case GoalType.Daily:
                    result.CurrentWordCount = GetTodayProgress(data, goal.Id);
                    result.DaysRemaining = 0;
                    break;

                case GoalType.Weekly:
                    result.CurrentWordCount = GetWeekProgress(data, goal.Id);
                    var today = DateTime.UtcNow.Date;
                    var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);
                    result.DaysRemaining = (endOfWeek - today).Days;
                    break;

                case GoalType.Project:
                    result.CurrentWordCount = data.ProgressEntries
                        .Where(p => p.GoalId == goal.Id)
                        .Sum(p => p.WordCount);
                    if (goal.EndDate.HasValue)
                    {
                        result.DaysRemaining = Math.Max(0, (goal.EndDate.Value - DateTime.UtcNow).Days);
                    }
                    break;
            }

            result.WordsRemaining = Math.Max(0, goal.TargetWordCount - result.CurrentWordCount);
            result.PercentageComplete = goal.TargetWordCount > 0
                ? Math.Min(100, (double)result.CurrentWordCount / goal.TargetWordCount * 100)
                : 0;

            result.IsCompleted = result.CurrentWordCount >= goal.TargetWordCount;
            result.AverageWordsPerDay = GetAverageWordsPerDay(data, goal.Id);

            if (result.DaysRemaining > 0 && result.WordsRemaining > 0)
            {
                result.RequiredWordsPerDayToComplete = (double)result.WordsRemaining / result.DaysRemaining;
            }

            if (result.IsCompleted && !result.CompletedDate.HasValue)
            {
                result.CompletedDate = DateTime.UtcNow;
            }

            return result;
        }
    }
}
