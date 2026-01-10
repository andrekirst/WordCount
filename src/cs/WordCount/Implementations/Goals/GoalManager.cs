using System;
using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces.Goals;
using WordCount.Models.Goals;
using WordCount.Models.Results;

namespace WordCount.Implementations.Goals
{
    public class GoalManager : IGoalManager
    {
        private readonly IGoalRepository _repository;
        private readonly IProgressTracker _progressTracker;
        private readonly IStreakCalculator _streakCalculator;
        private readonly IMilestoneChecker _milestoneChecker;
        private GoalData _data;

        public GoalManager(
            IGoalRepository repository,
            IProgressTracker progressTracker,
            IStreakCalculator streakCalculator,
            IMilestoneChecker milestoneChecker)
        {
            _repository = repository;
            _progressTracker = progressTracker;
            _streakCalculator = streakCalculator;
            _milestoneChecker = milestoneChecker;
            _data = _repository.Load();
        }

        public Goal CreateGoal(GoalType type, int targetWordCount, string projectName = null)
        {
            if (targetWordCount <= 0)
            {
                throw new ArgumentException("Target word count must be greater than zero.", nameof(targetWordCount));
            }

            var goal = new Goal
            {
                Type = type,
                TargetWordCount = targetWordCount,
                ProjectName = projectName,
                IsActive = true
            };

            switch (type)
            {
                case GoalType.Daily:
                    goal.StartDate = DateTime.UtcNow.Date;
                    goal.EndDate = DateTime.UtcNow.Date.AddDays(1);
                    break;

                case GoalType.Weekly:
                    var today = DateTime.UtcNow.Date;
                    goal.StartDate = today.AddDays(-(int)today.DayOfWeek);
                    goal.EndDate = goal.StartDate.Value.AddDays(7);
                    break;

                case GoalType.Project:
                    goal.StartDate = DateTime.UtcNow.Date;
                    break;
            }

            // Deactivate other active goals of the same type
            foreach (var existingGoal in _data.Goals.Where(g => g.Type == type && g.IsActive))
            {
                existingGoal.IsActive = false;
            }

            _data.Goals.Add(goal);
            _milestoneChecker.CheckForNewMilestones(_data);
            _repository.Save(_data);

            return goal;
        }

        public Goal GetActiveGoal()
        {
            return _data.Goals.FirstOrDefault(g => g.IsActive);
        }

        public IEnumerable<Goal> GetAllGoals()
        {
            return _data.Goals.OrderByDescending(g => g.CreatedAt);
        }

        public void DeactivateGoal(string goalId)
        {
            var goal = _data.Goals.FirstOrDefault(g => g.Id == goalId);
            if (goal != null)
            {
                goal.IsActive = false;
                _repository.Save(_data);
            }
        }

        public GoalProgressResult GetGoalProgress(string goalId = null)
        {
            var goal = goalId != null
                ? _data.Goals.FirstOrDefault(g => g.Id == goalId)
                : GetActiveGoal();

            if (goal == null)
            {
                return null;
            }

            return _progressTracker.CalculateGoalProgress(_data, goal);
        }

        public void RecordProgress(int wordCount, string sourceFile = null)
        {
            if (wordCount <= 0)
            {
                return;
            }

            var activeGoal = GetActiveGoal();
            if (activeGoal == null)
            {
                return;
            }

            _progressTracker.RecordProgress(_data, wordCount, activeGoal.Id, sourceFile);
            _streakCalculator.UpdateStreak(_data);

            // Check if goal is completed for the first time
            var progress = _progressTracker.CalculateGoalProgress(_data, activeGoal);
            if (progress.IsCompleted && !activeGoal.CompletedAt.HasValue)
            {
                activeGoal.CompletedAt = DateTime.UtcNow;
                _data.TotalGoalsCompleted++;
            }

            _milestoneChecker.CheckForNewMilestones(_data);
            _repository.Save(_data);
        }

        public GoalData GetGoalData()
        {
            return _data;
        }
    }
}
