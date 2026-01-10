using System.Collections.Generic;

namespace WordCount.Models.Goals
{
    /// <summary>
    /// Represents the complete goal tracking data including goals, progress, milestones, and streaks.
    /// This is the root object for persistence and is serialized to JSON.
    /// </summary>
    public class GoalData
    {
        /// <summary>
        /// Gets or sets the list of all goals (both active and inactive).
        /// </summary>
        public List<Goal> Goals { get; set; }

        /// <summary>
        /// Gets or sets the list of all progress entries across all goals.
        /// </summary>
        public List<ProgressEntry> ProgressEntries { get; set; }

        /// <summary>
        /// Gets or sets the list of all achieved milestones.
        /// </summary>
        public List<Milestone> Milestones { get; set; }

        /// <summary>
        /// Gets or sets the total number of words written across all time.
        /// This is a running total that never decreases.
        /// </summary>
        public int TotalWordsWritten { get; set; }

        /// <summary>
        /// Gets or sets the total number of goals that have been completed.
        /// </summary>
        public int TotalGoalsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the current streak information.
        /// </summary>
        public Streak CurrentStreak { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoalData"/> class with empty collections.
        /// </summary>
        public GoalData()
        {
            Goals = new List<Goal>();
            ProgressEntries = new List<ProgressEntry>();
            Milestones = new List<Milestone>();
            CurrentStreak = new Streak();
        }
    }
}
