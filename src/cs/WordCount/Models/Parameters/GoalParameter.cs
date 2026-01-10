using WordCount.Models.Goals;

namespace WordCount.Models.Parameters
{
    /// <summary>
    /// Represents parsed command-line parameters for goal-related operations.
    /// </summary>
    public class GoalParameter : BaseParameter
    {
        /// <summary>
        /// Gets or sets the type of goal to create (Daily, Weekly, or Project).
        /// Null if no goal creation was requested.
        /// </summary>
        public GoalType? GoalType { get; set; }

        /// <summary>
        /// Gets or sets the target word count for the goal.
        /// Null if no goal creation was requested.
        /// </summary>
        public int? TargetWordCount { get; set; }

        /// <summary>
        /// Gets or sets the project name for project-based goals.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets whether the --goal-progress flag was specified.
        /// When true, displays current goal progress.
        /// </summary>
        public bool ShowProgress { get; set; }

        /// <summary>
        /// Gets or sets whether the --goal-stats flag was specified.
        /// When true, displays goal statistics and milestones.
        /// </summary>
        public bool ShowStats { get; set; }
    }
}
