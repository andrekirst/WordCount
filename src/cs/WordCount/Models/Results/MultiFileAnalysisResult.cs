using System.Collections.Generic;

namespace WordCount.Models.Results
{
    /// <summary>
    /// Represents the complete result of analyzing multiple files in a folder.
    /// </summary>
    /// <remarks>
    /// This model contains both individual file results and aggregated statistics
    /// across all analyzed files, providing a comprehensive view of the analysis.
    /// </remarks>
    public class MultiFileAnalysisResult
    {
        /// <summary>
        /// Gets or sets the list of individual file analysis results.
        /// </summary>
        public List<FileAnalysisResult> FileResults { get; set; } = new List<FileAnalysisResult>();

        /// <summary>
        /// Gets or sets the aggregated statistics across all analyzed files.
        /// </summary>
        public AggregateStatistics AggregateStatistics { get; set; } = new AggregateStatistics();

        /// <summary>
        /// Gets or sets the total number of files that were successfully analyzed.
        /// </summary>
        /// <remarks>
        /// This may be less than the total files in the folder if some files failed to process.
        /// </remarks>
        public int TotalFilesAnalyzed { get; set; }

        /// <summary>
        /// Gets or sets the path to the folder that was analyzed.
        /// </summary>
        public string FolderPath { get; set; } = string.Empty;
    }
}
