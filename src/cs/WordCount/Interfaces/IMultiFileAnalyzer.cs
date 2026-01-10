using WordCount.Models.Results;

namespace WordCount.Interfaces
{
    /// <summary>
    /// Defines the contract for analyzing multiple text files in a folder.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface provide batch analysis capabilities,
    /// allowing users to analyze entire folders of documents and receive
    /// both individual and aggregate statistics.
    /// </remarks>
    public interface IMultiFileAnalyzer
    {
        /// <summary>
        /// Analyzes all files matching the specified pattern in a folder.
        /// </summary>
        /// <param name="folderPath">The path to the folder containing files to analyze.</param>
        /// <param name="filePattern">The glob pattern to match files (e.g., "*.txt").</param>
        /// <returns>A result containing individual file analyses and aggregate statistics.</returns>
        MultiFileAnalysisResult AnalyzeFolder(string folderPath, string filePattern);
    }
}
