using System;

namespace WordCount.Models.Goals
{
    /// <summary>
    /// Represents a word count goal that a writer is working towards.
    /// Goals can be daily, weekly, or project-based with specific target word counts.
    /// </summary>
    public class Goal
    {
        /// <summary>
        /// Gets or sets the unique identifier for this goal.
        /// Auto-generated as a GUID string on construction.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of goal (Daily, Weekly, or Project).
        /// </summary>
        public GoalType Type { get; set; }

        /// <summary>
        /// Gets or sets the target number of words to achieve for this goal.
        /// </summary>
        public int TargetWordCount { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when this goal was created.
        /// Auto-set to the current UTC time on construction.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the start date of the goal period.
        /// For daily goals, this is the current day. For weekly goals, the start of the week.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the goal period.
        /// For daily goals, this is the next day. For weekly goals, the end of the week.
        /// For project goals, this may be null for open-ended projects.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the optional project name for project-based goals.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets whether this goal is currently active.
        /// Only one goal of each type should be active at a time.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when this goal was first completed.
        /// Null if the goal has not been completed yet.
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Goal"/> class with default values.
        /// </summary>
        public Goal()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
