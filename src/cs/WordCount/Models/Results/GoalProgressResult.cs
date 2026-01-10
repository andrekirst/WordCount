using System;
using WordCount.Models.Goals;

namespace WordCount.Models.Results
{
    /// <summary>
    /// Contains the calculated progress information for a word count goal.
    /// </summary>
    public class GoalProgressResult
    {
        /// <summary>
        /// Gets or sets the goal this progress relates to.
        /// </summary>
        public Goal Goal { get; set; }

        /// <summary>
        /// Gets or sets the current word count achieved towards the goal.
        /// </summary>
        public int CurrentWordCount { get; set; }

        /// <summary>
        /// Gets or sets the target word count for the goal.
        /// </summary>
        public int TargetWordCount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the goal completed (0-100).
        /// Capped at 100 even if the target is exceeded.
        /// </summary>
        public double PercentageComplete { get; set; }

        /// <summary>
        /// Gets or sets the number of words still needed to complete the goal.
        /// Returns 0 if the goal is complete or exceeded.
        /// </summary>
        public int WordsRemaining { get; set; }

        /// <summary>
        /// Gets or sets whether the goal has been completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the date when the goal was completed.
        /// Null if the goal is not yet complete.
        /// </summary>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// Gets or sets the number of days remaining until the goal's end date.
        /// Only applicable for weekly and project goals with end dates.
        /// </summary>
        public int DaysRemaining { get; set; }

        /// <summary>
        /// Gets or sets the average number of words written per day towards this goal.
        /// </summary>
        public double AverageWordsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the number of words per day required to complete the goal on time.
        /// Calculated as WordsRemaining / DaysRemaining.
        /// Only meaningful when DaysRemaining > 0 and WordsRemaining > 0.
        /// </summary>
        public double RequiredWordsPerDayToComplete { get; set; }
    }
}
