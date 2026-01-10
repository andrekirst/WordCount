using System.Collections.Generic;

namespace WordCount.Models.Results
{
    /// <summary>
    /// Represents aggregated statistics across multiple analyzed files.
    /// </summary>
    /// <remarks>
    /// These statistics provide a summary view of word counts, unique words,
    /// and other metrics combined from all files in a multi-file analysis.
    /// </remarks>
    public class AggregateStatistics
    {
        /// <summary>
        /// Gets or sets the total word count across all analyzed files.
        /// </summary>
        public int TotalWords { get; set; }

        /// <summary>
        /// Gets or sets the count of unique words across all analyzed files.
        /// </summary>
        /// <remarks>
        /// Words are compared case-insensitively, so "Hello" and "hello" count as one unique word.
        /// </remarks>
        public int TotalUniqueWords { get; set; }

        /// <summary>
        /// Gets or sets the weighted average word length across all files.
        /// </summary>
        /// <remarks>
        /// Calculated as: Σ(avgWordLength × wordCount) / Σ(wordCount)
        /// This ensures files with more words have proportionally more influence on the average.
        /// </remarks>
        public double AverageWordLength { get; set; }

        /// <summary>
        /// Gets or sets the total chapter count across all analyzed files.
        /// </summary>
        public int TotalChapters { get; set; }

        /// <summary>
        /// Gets or sets the list of all distinct words across all files, sorted alphabetically.
        /// </summary>
        public List<string> AllDistinctWords { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the total file size in bytes of all analyzed files.
        /// </summary>
        public long TotalFileSizeBytes { get; set; }
    }
}
