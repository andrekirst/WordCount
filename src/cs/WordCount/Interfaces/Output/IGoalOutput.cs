using System.Collections.Generic;
using WordCount.Models.Goals;
using WordCount.Models.Results;

namespace WordCount.Interfaces.Output
{
    /// <summary>
    /// Handles the display of goal-related information to the console.
    /// Provides visualization for progress, streaks, milestones, and charts.
    /// </summary>
    public interface IGoalOutput
    {
        /// <summary>
        /// Displays the current progress towards a goal with a visual progress bar.
        /// </summary>
        /// <param name="progress">
        /// The progress result to display. If null, displays a message indicating no goal is set.
        /// </param>
        /// <remarks>
        /// Output includes:
        /// - Goal type and target word count
        /// - Project name (if set)
        /// - ASCII progress bar with percentage
        /// - Current word count vs target
        /// - Words remaining
        /// - Days remaining and required words/day (for weekly/project goals)
        /// - Completion celebration message (if goal is complete)
        /// </remarks>
        void DisplayGoalProgress(GoalProgressResult progress);

        /// <summary>
        /// Displays the current streak information.
        /// </summary>
        /// <param name="streak">The streak data to display.</param>
        /// <remarks>
        /// Output includes:
        /// - Current streak with visual indicator (more stars for longer streaks)
        /// - Best (longest) streak
        /// - Last activity date
        /// </remarks>
        void DisplayStreak(Streak streak);

        /// <summary>
        /// Displays newly achieved milestones with trophy icons.
        /// </summary>
        /// <param name="milestones">The milestones to display. If empty, nothing is displayed.</param>
        /// <remarks>
        /// Trophy icons vary by milestone tier:
        /// - [GOLD] for highest tier milestones (100K words, 30-day streak, 25 goals)
        /// - [SILVER] for mid-tier milestones (50K words, 7-day streak, 10 goals)
        /// - [BRONZE] for entry-tier milestones (10K words, 3-day streak, 5 goals)
        /// - [+] for other milestones
        /// </remarks>
        void DisplayMilestones(IEnumerable<Milestone> milestones);

        /// <summary>
        /// Displays a summary of all goal statistics.
        /// </summary>
        /// <param name="data">The complete goal data to summarize.</param>
        /// <remarks>
        /// Output includes:
        /// - Total words written (lifetime)
        /// - Total goals completed
        /// - Total milestones earned
        /// - List of currently active goals
        /// </remarks>
        void DisplayGoalSummary(GoalData data);

        /// <summary>
        /// Displays an ASCII chart of recent progress over the last 7 days.
        /// </summary>
        /// <param name="data">The goal data containing progress entries.</param>
        /// <param name="goalId">
        /// Optional goal ID to filter progress. If null, shows all progress regardless of goal.
        /// </param>
        /// <remarks>
        /// The chart shows day abbreviations on the left, horizontal bar graphs,
        /// and word counts on the right. Bar lengths are normalized to the maximum daily word count.
        /// </remarks>
        void DisplayProgressChart(GoalData data, string goalId);
    }
}
