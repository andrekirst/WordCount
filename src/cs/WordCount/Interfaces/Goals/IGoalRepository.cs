using WordCount.Models.Goals;

namespace WordCount.Interfaces.Goals
{
    /// <summary>
    /// Repository interface for persisting and retrieving goal data.
    /// Provides abstraction over the storage mechanism (currently JSON file-based).
    /// </summary>
    public interface IGoalRepository
    {
        /// <summary>
        /// Loads all goal data from persistent storage.
        /// </summary>
        /// <returns>
        /// The complete goal data including goals, progress entries, milestones, and streak information.
        /// Returns a new empty GoalData instance if no data exists or if loading fails.
        /// </returns>
        /// <remarks>
        /// This method is fault-tolerant and will return empty data rather than throwing exceptions
        /// for missing files or invalid JSON.
        /// </remarks>
        GoalData Load();

        /// <summary>
        /// Saves goal data to persistent storage.
        /// </summary>
        /// <param name="data">The goal data to save. Cannot be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when data is null.</exception>
        /// <exception cref="System.IO.IOException">Thrown when file write fails.</exception>
        /// <remarks>
        /// The data is stored in JSON format in the user's application data folder
        /// at %APPDATA%/WordCount/goals.json (Windows) or ~/.config/WordCount/goals.json (Linux).
        /// </remarks>
        void Save(GoalData data);
    }
}
