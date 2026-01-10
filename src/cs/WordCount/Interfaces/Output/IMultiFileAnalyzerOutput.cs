using WordCount.Models.Results;

namespace WordCount.Interfaces.Output
{
    /// <summary>
    /// Defines the contract for displaying multi-file analysis results.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface format and display the results of
    /// multi-file analysis operations, including aggregate statistics and
    /// individual file details in a dashboard-style presentation.
    /// </remarks>
    public interface IMultiFileAnalyzerOutput
    {
        /// <summary>
        /// Displays the multi-file analysis result in a formatted dashboard view.
        /// </summary>
        /// <param name="result">The analysis result to display. If null or empty, no output is produced.</param>
        void DisplayResult(MultiFileAnalysisResult result);
    }
}
