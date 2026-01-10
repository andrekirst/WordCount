namespace WordCount.Models.Parameters
{
    /// <summary>
    /// Represents command-line parameters for multi-file folder analysis.
    /// </summary>
    /// <remarks>
    /// Used with the -f/--folder and -p/--pattern command-line arguments.
    /// </remarks>
    /// <example>
    /// Command line: WordCount -f /documents -p *.md
    /// Results in: FolderPath = "/documents", FilePattern = "*.md"
    /// </example>
    public class FolderParameter : BaseParameter
    {
        /// <summary>
        /// Gets or sets the path to the folder to analyze.
        /// </summary>
        public string FolderPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file pattern to match when scanning the folder.
        /// </summary>
        /// <remarks>
        /// Supports standard glob patterns like "*.txt", "*.md", etc.
        /// Defaults to "*.txt" if not specified.
        /// </remarks>
        public string FilePattern { get; set; } = "*.txt";
    }
}
