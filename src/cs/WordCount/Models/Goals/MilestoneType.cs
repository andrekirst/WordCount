namespace WordCount.Models.Goals
{
    /// <summary>
    /// Defines the types of achievement milestones that can be earned.
    /// </summary>
    public enum MilestoneType
    {
        /// <summary>
        /// Awarded when the user sets their first writing goal.
        /// </summary>
        FirstGoalSet,

        /// <summary>
        /// Awarded when the user completes their first goal.
        /// </summary>
        FirstGoalCompleted,

        /// <summary>
        /// Awarded for maintaining a 3-day writing streak.
        /// </summary>
        ThreeDayStreak,

        /// <summary>
        /// Awarded for maintaining a 7-day (full week) writing streak.
        /// </summary>
        SevenDayStreak,

        /// <summary>
        /// Awarded for maintaining a 30-day writing streak.
        /// </summary>
        ThirtyDayStreak,

        /// <summary>
        /// Awarded for writing 1,000 total words.
        /// </summary>
        TotalWords1000,

        /// <summary>
        /// Awarded for writing 5,000 total words.
        /// </summary>
        TotalWords5000,

        /// <summary>
        /// Awarded for writing 10,000 total words.
        /// </summary>
        TotalWords10000,

        /// <summary>
        /// Awarded for writing 50,000 total words (NaNoWriMo equivalent).
        /// </summary>
        TotalWords50000,

        /// <summary>
        /// Awarded for writing 100,000 total words (novelist tier).
        /// </summary>
        TotalWords100000,

        /// <summary>
        /// Awarded for completing 5 goals.
        /// </summary>
        GoalsCompleted5,

        /// <summary>
        /// Awarded for completing 10 goals.
        /// </summary>
        GoalsCompleted10,

        /// <summary>
        /// Awarded for completing 25 goals (Goal Master tier).
        /// </summary>
        GoalsCompleted25
    }
}
