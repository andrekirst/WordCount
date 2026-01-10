namespace WordCount.Models.Results
{
    /// <summary>
    /// Represents the analysis result for a single file in a multi-file analysis.
    /// </summary>
    /// <remarks>
    /// Contains both file metadata (name, path, size) and the word count analysis results.
    /// </remarks>
    public class FileAnalysisResult
    {
        /// <summary>
        /// Gets or sets the name of the file (without directory path).
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full path to the file.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the size of the file in bytes.
        /// </summary>
        public long FileSizeBytes { get; set; }

        /// <summary>
        /// Gets or sets the word count analysis result for this file.
        /// </summary>
        public WordCountAnalyzerResult AnalysisResult { get; set; } = new WordCountAnalyzerResult();
    }
}
