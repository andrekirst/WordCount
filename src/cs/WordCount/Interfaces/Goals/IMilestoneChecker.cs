using System.Collections.Generic;
using WordCount.Models.Goals;

namespace WordCount.Interfaces.Goals
{
    /// <summary>
    /// Checks for and manages achievement milestones based on writing progress.
    /// Milestones are awarded once and tracked permanently.
    /// </summary>
    /// <remarks>
    /// Available milestones include:
    /// - Goal milestones: FirstGoalSet, FirstGoalCompleted, GoalsCompleted5/10/25
    /// - Streak milestones: ThreeDayStreak, SevenDayStreak, ThirtyDayStreak
    /// - Word count milestones: TotalWords1000/5000/10000/50000/100000
    /// </remarks>
    public interface IMilestoneChecker
    {
        /// <summary>
        /// Checks for newly achieved milestones based on current goal data.
        /// </summary>
        /// <param name="data">The goal data to check against milestone criteria.</param>
        /// <returns>
        /// An enumerable of newly achieved milestones. Empty if no new milestones were earned.
        /// Each milestone is marked with IsNew = true.
        /// </returns>
        /// <remarks>
        /// This method automatically adds new milestones to the data.Milestones collection.
        /// Milestones are only awarded once - calling this repeatedly with the same data
        /// will not create duplicate milestones.
        /// </remarks>
        IEnumerable<Milestone> CheckForNewMilestones(GoalData data);

        /// <summary>
        /// Gets all achieved milestones, ordered by achievement date (newest first).
        /// </summary>
        /// <param name="data">The goal data containing the milestones.</param>
        /// <returns>All milestones that have been achieved, ordered by AchievedAt descending.</returns>
        IEnumerable<Milestone> GetAllMilestones(GoalData data);

        /// <summary>
        /// Gets only the milestones that are marked as new (not yet acknowledged).
        /// </summary>
        /// <param name="data">The goal data containing the milestones.</param>
        /// <returns>Milestones where IsNew = true, ordered by AchievedAt descending.</returns>
        /// <remarks>
        /// Use this to display newly earned milestones to the user.
        /// After displaying, set IsNew = false to acknowledge them.
        /// </remarks>
        IEnumerable<Milestone> GetNewMilestones(GoalData data);
    }
}
