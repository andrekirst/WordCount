using System;

namespace WordCount.Models.Goals
{
    /// <summary>
    /// Represents streak information tracking consecutive days of writing activity.
    /// </summary>
    public class Streak
    {
        /// <summary>
        /// Gets or sets the current streak count (consecutive days of activity).
        /// Reset to 0 if a day is missed.
        /// </summary>
        public int CurrentStreak { get; set; }

        /// <summary>
        /// Gets or sets the longest streak ever achieved.
        /// This value is never decremented, only updated when current streak exceeds it.
        /// </summary>
        public int LongestStreak { get; set; }

        /// <summary>
        /// Gets or sets the date of the most recent writing activity.
        /// Used to determine if the streak is still active.
        /// </summary>
        public DateTime? LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the current streak began.
        /// Reset when a streak is broken and a new one starts.
        /// </summary>
        public DateTime? StreakStartDate { get; set; }
    }
}
