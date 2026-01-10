using System.Collections.Generic;
using WordCount.Models.Goals;
using WordCount.Models.Results;

namespace WordCount.Interfaces.Goals
{
    /// <summary>
    /// Manages word count goals including creation, tracking, and progress calculation.
    /// This is the main entry point for goal-related operations.
    /// </summary>
    public interface IGoalManager
    {
        /// <summary>
        /// Creates a new word count goal of the specified type.
        /// </summary>
        /// <param name="type">The type of goal (Daily, Weekly, or Project).</param>
        /// <param name="targetWordCount">The target number of words to achieve. Must be greater than zero.</param>
        /// <param name="projectName">Optional project name for project-based goals.</param>
        /// <returns>The newly created goal with auto-generated ID and timestamps.</returns>
        /// <exception cref="System.ArgumentException">Thrown when targetWordCount is less than or equal to zero.</exception>
        /// <remarks>
        /// Creating a new goal of the same type will automatically deactivate any existing active goals of that type.
        /// Daily goals reset each day, weekly goals reset at the start of each week.
        /// </remarks>
        Goal CreateGoal(GoalType type, int targetWordCount, string projectName = null);

        /// <summary>
        /// Gets the currently active goal, if any.
        /// </summary>
        /// <returns>The active goal, or null if no goal is currently active.</returns>
        Goal GetActiveGoal();

        /// <summary>
        /// Gets all goals (active and inactive), ordered by creation date descending.
        /// </summary>
        /// <returns>An enumerable of all goals.</returns>
        IEnumerable<Goal> GetAllGoals();

        /// <summary>
        /// Deactivates a goal by its ID.
        /// </summary>
        /// <param name="goalId">The unique identifier of the goal to deactivate.</param>
        /// <remarks>
        /// If the goalId is not found, this method does nothing.
        /// </remarks>
        void DeactivateGoal(string goalId);

        /// <summary>
        /// Gets the progress for a specific goal or the active goal.
        /// </summary>
        /// <param name="goalId">Optional goal ID. If null, returns progress for the active goal.</param>
        /// <returns>The progress result including current word count, percentage, and remaining words, or null if no goal found.</returns>
        GoalProgressResult GetGoalProgress(string goalId = null);

        /// <summary>
        /// Records progress towards the active goal.
        /// </summary>
        /// <param name="wordCount">The number of words to record. Must be greater than zero.</param>
        /// <param name="sourceFile">Optional source file name for tracking purposes.</param>
        /// <remarks>
        /// This method also updates streak tracking and checks for new milestone achievements.
        /// If no active goal exists or wordCount is &lt;= 0, this method does nothing.
        /// </remarks>
        void RecordProgress(int wordCount, string sourceFile = null);

        /// <summary>
        /// Gets the complete goal data including all goals, progress entries, milestones, and streak information.
        /// </summary>
        /// <returns>The complete goal data object.</returns>
        GoalData GetGoalData();
    }
}
