using System;
using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces.Goals;
using WordCount.Models.Goals;

namespace WordCount.Implementations.Goals
{
    public class MilestoneChecker : IMilestoneChecker
    {
        // Streak thresholds
        private const int ThreeDayStreakThreshold = 3;
        private const int SevenDayStreakThreshold = 7;
        private const int ThirtyDayStreakThreshold = 30;

        // Word count thresholds
        private const int WordMilestone1K = 1000;
        private const int WordMilestone5K = 5000;
        private const int WordMilestone10K = 10000;
        private const int WordMilestone50K = 50000;
        private const int WordMilestone100K = 100000;

        // Goals completed thresholds
        private const int GoalsCompleted5Threshold = 5;
        private const int GoalsCompleted10Threshold = 10;
        private const int GoalsCompleted25Threshold = 25;

        private readonly Dictionary<MilestoneType, string> _milestoneDescriptions = new Dictionary<MilestoneType, string>
        {
            { MilestoneType.FirstGoalSet, "Set your first writing goal!" },
            { MilestoneType.FirstGoalCompleted, "Completed your first goal!" },
            { MilestoneType.ThreeDayStreak, "Maintained a 3-day writing streak!" },
            { MilestoneType.SevenDayStreak, "Achieved a full week writing streak!" },
            { MilestoneType.ThirtyDayStreak, "Amazing 30-day writing streak!" },
            { MilestoneType.TotalWords1000, "Written 1,000 words total!" },
            { MilestoneType.TotalWords5000, "Written 5,000 words total!" },
            { MilestoneType.TotalWords10000, "Written 10,000 words total!" },
            { MilestoneType.TotalWords50000, "Written 50,000 words - NaNoWriMo winner!" },
            { MilestoneType.TotalWords100000, "Written 100,000 words - Novelist!" },
            { MilestoneType.GoalsCompleted5, "Completed 5 writing goals!" },
            { MilestoneType.GoalsCompleted10, "Completed 10 writing goals!" },
            { MilestoneType.GoalsCompleted25, "Completed 25 writing goals - Goal Master!" }
        };

        public IEnumerable<Milestone> CheckForNewMilestones(GoalData data)
        {
            var newMilestones = new List<Milestone>();
            var existingTypes = data.Milestones.Select(m => m.Type).ToHashSet();

            // Check goal-related milestones
            if (data.Goals.Any() && !existingTypes.Contains(MilestoneType.FirstGoalSet))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.FirstGoalSet));
            }

            if (data.TotalGoalsCompleted >= 1 && !existingTypes.Contains(MilestoneType.FirstGoalCompleted))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.FirstGoalCompleted));
            }

            if (data.TotalGoalsCompleted >= GoalsCompleted5Threshold && !existingTypes.Contains(MilestoneType.GoalsCompleted5))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.GoalsCompleted5));
            }

            if (data.TotalGoalsCompleted >= GoalsCompleted10Threshold && !existingTypes.Contains(MilestoneType.GoalsCompleted10))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.GoalsCompleted10));
            }

            if (data.TotalGoalsCompleted >= GoalsCompleted25Threshold && !existingTypes.Contains(MilestoneType.GoalsCompleted25))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.GoalsCompleted25));
            }

            // Check streak milestones
            var streak = data.CurrentStreak.CurrentStreak;

            if (streak >= ThreeDayStreakThreshold && !existingTypes.Contains(MilestoneType.ThreeDayStreak))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.ThreeDayStreak));
            }

            if (streak >= SevenDayStreakThreshold && !existingTypes.Contains(MilestoneType.SevenDayStreak))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.SevenDayStreak));
            }

            if (streak >= ThirtyDayStreakThreshold && !existingTypes.Contains(MilestoneType.ThirtyDayStreak))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.ThirtyDayStreak));
            }

            // Check word count milestones
            var totalWords = data.TotalWordsWritten;

            if (totalWords >= WordMilestone1K && !existingTypes.Contains(MilestoneType.TotalWords1000))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.TotalWords1000));
            }

            if (totalWords >= WordMilestone5K && !existingTypes.Contains(MilestoneType.TotalWords5000))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.TotalWords5000));
            }

            if (totalWords >= WordMilestone10K && !existingTypes.Contains(MilestoneType.TotalWords10000))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.TotalWords10000));
            }

            if (totalWords >= WordMilestone50K && !existingTypes.Contains(MilestoneType.TotalWords50000))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.TotalWords50000));
            }

            if (totalWords >= WordMilestone100K && !existingTypes.Contains(MilestoneType.TotalWords100000))
            {
                newMilestones.Add(CreateMilestone(MilestoneType.TotalWords100000));
            }

            // Add new milestones to data
            foreach (var milestone in newMilestones)
            {
                data.Milestones.Add(milestone);
            }

            return newMilestones;
        }

        public IEnumerable<Milestone> GetAllMilestones(GoalData data)
        {
            return data.Milestones.OrderByDescending(m => m.AchievedAt);
        }

        public IEnumerable<Milestone> GetNewMilestones(GoalData data)
        {
            return data.Milestones.Where(m => m.IsNew).OrderByDescending(m => m.AchievedAt);
        }

        private Milestone CreateMilestone(MilestoneType type)
        {
            return new Milestone
            {
                Type = type,
                Description = _milestoneDescriptions.GetValueOrDefault(type, type.ToString()),
                AchievedAt = DateTime.UtcNow,
                IsNew = true
            };
        }
    }
}
