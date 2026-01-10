using System;

namespace WordCount.Models.Goals
{
    /// <summary>
    /// Represents an achievement milestone that has been earned.
    /// </summary>
    public class Milestone
    {
        /// <summary>
        /// Gets or sets the type of milestone achieved.
        /// </summary>
        public MilestoneType Type { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when this milestone was achieved.
        /// Auto-set to the current UTC time on construction.
        /// </summary>
        public DateTime AchievedAt { get; set; }

        /// <summary>
        /// Gets or sets the human-readable description of the milestone.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether this milestone is new (not yet acknowledged by the user).
        /// Set to true on construction. Set to false after displaying to the user.
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Milestone"/> class with default values.
        /// </summary>
        public Milestone()
        {
            AchievedAt = DateTime.UtcNow;
            IsNew = true;
        }
    }
}
