using System;
using WordCount.Models.Goals;
using WordCount.Models.Results;

namespace WordCount.Interfaces.Goals
{
    /// <summary>
    /// Tracks and calculates progress towards word count goals.
    /// Provides methods for recording progress and calculating statistics.
    /// </summary>
    public interface IProgressTracker
    {
        /// <summary>
        /// Records a progress entry for a specific goal.
        /// </summary>
        /// <param name="data">The goal data container to update.</param>
        /// <param name="wordCount">The number of words to record.</param>
        /// <param name="goalId">The ID of the goal this progress belongs to.</param>
        /// <param name="sourceFile">Optional source file name for tracking purposes.</param>
        /// <remarks>
        /// This method creates a new ProgressEntry and adds it to the data.
        /// It also updates the TotalWordsWritten counter.
        /// </remarks>
        void RecordProgress(GoalData data, int wordCount, string goalId, string sourceFile = null);

        /// <summary>
        /// Gets the total word count for a goal within a specific time period.
        /// </summary>
        /// <param name="data">The goal data to search.</param>
        /// <param name="goalId">The ID of the goal to get progress for.</param>
        /// <param name="startDate">The start date of the period (inclusive).</param>
        /// <param name="endDate">The end date of the period (exclusive).</param>
        /// <returns>The sum of word counts within the specified period.</returns>
        int GetProgressForPeriod(GoalData data, string goalId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the total word count for a goal for the current day (UTC).
        /// </summary>
        /// <param name="data">The goal data to search.</param>
        /// <param name="goalId">The ID of the goal to get progress for.</param>
        /// <returns>The sum of word counts recorded today.</returns>
        int GetTodayProgress(GoalData data, string goalId);

        /// <summary>
        /// Gets the total word count for a goal for the current week (Sunday to Saturday, UTC).
        /// </summary>
        /// <param name="data">The goal data to search.</param>
        /// <param name="goalId">The ID of the goal to get progress for.</param>
        /// <returns>The sum of word counts recorded this week.</returns>
        int GetWeekProgress(GoalData data, string goalId);

        /// <summary>
        /// Calculates the average words written per day for a specific goal.
        /// </summary>
        /// <param name="data">The goal data to analyze.</param>
        /// <param name="goalId">The ID of the goal to calculate for.</param>
        /// <returns>
        /// The average words per day, calculated as total words divided by the number of days
        /// between the first and last progress entry. Returns 0 if no entries exist.
        /// </returns>
        double GetAverageWordsPerDay(GoalData data, string goalId);

        /// <summary>
        /// Calculates the complete progress information for a goal.
        /// </summary>
        /// <param name="data">The goal data to use for calculation.</param>
        /// <param name="goal">The goal to calculate progress for.</param>
        /// <returns>
        /// A GoalProgressResult containing current word count, target, percentage complete,
        /// words remaining, days remaining, and required daily pace to complete on time.
        /// </returns>
        GoalProgressResult CalculateGoalProgress(GoalData data, Goal goal);
    }
}
