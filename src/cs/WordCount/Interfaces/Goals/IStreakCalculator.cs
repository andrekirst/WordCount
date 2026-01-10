using WordCount.Models.Goals;

namespace WordCount.Interfaces.Goals
{
    /// <summary>
    /// Calculates and tracks writing streaks (consecutive days of activity).
    /// A streak is maintained when writing progress is recorded on consecutive days.
    /// </summary>
    public interface IStreakCalculator
    {
        /// <summary>
        /// Updates the streak information based on current progress entries.
        /// </summary>
        /// <param name="data">The goal data containing progress entries and current streak info.</param>
        /// <remarks>
        /// This method should be called after recording new progress.
        /// It will:
        /// - Start a new streak if there's activity today and no existing streak
        /// - Increment the current streak if today is consecutive with the last activity
        /// - Reset the streak to 1 if there's a gap in activity
        /// - Update the longest streak if current exceeds it
        /// </remarks>
        void UpdateStreak(GoalData data);

        /// <summary>
        /// Gets the current streak information after updating it.
        /// </summary>
        /// <param name="data">The goal data to analyze.</param>
        /// <returns>The current streak information including current streak days and longest streak.</returns>
        /// <remarks>
        /// This method calls UpdateStreak internally before returning the result.
        /// </remarks>
        Streak GetCurrentStreak(GoalData data);

        /// <summary>
        /// Checks if the current streak is still active (not broken by a gap).
        /// </summary>
        /// <param name="data">The goal data to check.</param>
        /// <returns>
        /// True if the last activity was today or yesterday, indicating the streak is active.
        /// False if there's a gap of 2+ days or no activity exists.
        /// </returns>
        bool IsStreakActive(GoalData data);
    }
}
