using System;

namespace WordCount.Models.Goals
{
    /// <summary>
    /// Represents a single progress entry recording words written towards a goal.
    /// </summary>
    public class ProgressEntry
    {
        /// <summary>
        /// Gets or sets the unique identifier for this progress entry.
        /// Auto-generated as a GUID string on construction.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the goal this progress belongs to.
        /// </summary>
        public string GoalId { get; set; }

        /// <summary>
        /// Gets or sets the UTC date and time when this progress was recorded.
        /// Auto-set to the current UTC time on construction.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the number of words recorded in this entry.
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        /// Gets or sets the optional source file name where the words came from.
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressEntry"/> class with default values.
        /// </summary>
        public ProgressEntry()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.UtcNow;
        }
    }
}
