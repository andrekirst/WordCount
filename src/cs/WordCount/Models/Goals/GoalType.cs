namespace WordCount.Models.Goals
{
    /// <summary>
    /// Defines the types of word count goals available.
    /// </summary>
    public enum GoalType
    {
        /// <summary>
        /// A daily goal that resets each day at midnight UTC.
        /// Progress is measured from start of day to end of day.
        /// </summary>
        Daily,

        /// <summary>
        /// A weekly goal that resets at the start of each week (Sunday).
        /// Progress is measured from Sunday to Saturday.
        /// </summary>
        Weekly,

        /// <summary>
        /// A project-based goal with a custom target and optional end date.
        /// Does not automatically reset and accumulates progress over time.
        /// </summary>
        Project
    }
}
