using WordCount.Extensions;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;

namespace WordCount.Implementations.Output
{
    /// <summary>
    /// Formats and displays multi-file analysis results in a dashboard-style console output.
    /// </summary>
    /// <remarks>
    /// The output format includes:
    /// <list type="bullet">
    /// <item><description>Dashboard header with folder information</description></item>
    /// <item><description>Aggregate statistics section with totals across all files</description></item>
    /// <item><description>Individual file results with per-file statistics</description></item>
    /// </list>
    /// All text output is localized using resource strings.
    /// </remarks>
    public class MultiFileAnalyzerOutput : IMultiFileAnalyzerOutput
    {
        /// <summary>
        /// The width of the dashboard header separator line in characters.
        /// </summary>
        private const int DashboardHeaderWidth = 60;

        /// <summary>
        /// The width of section separator lines in characters.
        /// </summary>
        private const int SectionSeparatorWidth = 40;

        private IDisplayOutput DisplayOutput { get; }
        private ILanguageResource LanguageResource { get; }
        private ILanguageDecision LanguageDecision { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFileAnalyzerOutput"/> class.
        /// </summary>
        /// <param name="displayOutput">The output handler for writing to the console.</param>
        /// <param name="languageResource">The resource provider for localized strings.</param>
        /// <param name="languageDecision">The language decision provider for culture-specific formatting.</param>
        public MultiFileAnalyzerOutput(
            IDisplayOutput displayOutput,
            ILanguageResource languageResource,
            ILanguageDecision languageDecision)
        {
            DisplayOutput = displayOutput;
            LanguageResource = languageResource;
            LanguageDecision = languageDecision;
        }

        /// <inheritdoc />
        public void DisplayResult(MultiFileAnalysisResult result)
        {
            if (result == null || result.TotalFilesAnalyzed == 0)
            {
                return;
            }

            var currentCulture = LanguageDecision.DecideLanguage().Culture;

            // Display Dashboard Header
            DisplayOutput.WriteLine();
            DisplayOutput.WriteResourceLine("MULTI_FILE_DASHBOARD");
            DisplayOutput.WriteLine(new string('=', DashboardHeaderWidth));

            // Display Folder Info
            DisplayOutput.WriteResourceLine("FOLDER_ANALYZED", result.FolderPath);
            DisplayOutput.WriteResourceLine("TOTAL_FILES_ANALYZED", result.TotalFilesAnalyzed);
            DisplayOutput.WriteLine();

            // Display Aggregate Statistics
            DisplayOutput.WriteResourceLine("AGGREGATE_STATISTICS");
            DisplayOutput.WriteLine(new string('-', SectionSeparatorWidth));

            var aggregateLabelMaxLength = LanguageResource.DetectLongestResourceString(
                new[] { "TOTAL_WORDS", "TOTAL_UNIQUE_WORDS", "OVERALL_AVERAGE_WORD_LENGTH", "TOTAL_CHAPTERS", "TOTAL_FILE_SIZE" });

            WriteAggregateStatistic("TOTAL_WORDS", result.AggregateStatistics.TotalWords.ToString(), aggregateLabelMaxLength);
            WriteAggregateStatistic("TOTAL_UNIQUE_WORDS", result.AggregateStatistics.TotalUniqueWords.ToString(), aggregateLabelMaxLength);
            WriteAggregateStatistic("OVERALL_AVERAGE_WORD_LENGTH",
                $"{result.AggregateStatistics.AverageWordLength.ToString("N2", currentCulture)} {LanguageResource.GetResourceStringById("CHARACTERS")}",
                aggregateLabelMaxLength);
            WriteAggregateStatistic("TOTAL_CHAPTERS", result.AggregateStatistics.TotalChapters.ToString(), aggregateLabelMaxLength);
            WriteAggregateStatistic("TOTAL_FILE_SIZE", FormatFileSize(result.AggregateStatistics.TotalFileSizeBytes), aggregateLabelMaxLength);

            DisplayOutput.WriteLine();

            // Display Individual File Results
            DisplayOutput.WriteResourceLine("INDIVIDUAL_FILE_RESULTS");
            DisplayOutput.WriteLine(new string('-', SectionSeparatorWidth));

            var fileLabelMaxLength = LanguageResource.DetectLongestResourceString(
                new[] { "NUMBER_OF_WORDS", "UNIQUE", "AVERAGE_WORD_LENGTH", "CHAPTERS", "FILE_SIZE" });

            foreach (var fileResult in result.FileResults)
            {
                DisplayOutput.WriteLine();
                DisplayOutput.WriteResourceLine("FILE_NAME", fileResult.FileName);

                WriteFileStat("NUMBER_OF_WORDS", fileResult.AnalysisResult.NumberOfWords.ToString(), fileLabelMaxLength);
                WriteFileStat("UNIQUE", fileResult.AnalysisResult.NumberOfUniqueWords.ToString(), fileLabelMaxLength);
                WriteFileStat("AVERAGE_WORD_LENGTH",
                    $"{fileResult.AnalysisResult.AverageWordLength.ToString("N2", currentCulture)} {LanguageResource.GetResourceStringById("CHARACTERS")}",
                    fileLabelMaxLength);
                WriteFileStat("CHAPTERS", fileResult.AnalysisResult.NumberOfChapters.ToString(), fileLabelMaxLength);
                WriteFileStat("FILE_SIZE", FormatFileSize(fileResult.FileSizeBytes), fileLabelMaxLength);
            }

            DisplayOutput.WriteLine();
            DisplayOutput.WriteLine(new string('=', DashboardHeaderWidth));
        }

        /// <summary>
        /// Writes a single aggregate statistic line with aligned formatting.
        /// </summary>
        /// <param name="resourceKey">The resource key for the statistic label.</param>
        /// <param name="value">The value to display.</param>
        /// <param name="maxLength">The maximum label length for alignment.</param>
        private void WriteAggregateStatistic(string resourceKey, string value, int maxLength)
        {
            var resourceValue = LanguageResource.GetResourceStringById(resourceKey);
            var output = resourceValue.FillRightWithPoints(maxLength);
            DisplayOutput.WriteLine($"- {output} {value}");
        }

        /// <summary>
        /// Writes a single file statistic line with aligned formatting (indented).
        /// </summary>
        /// <param name="resourceKey">The resource key for the statistic label.</param>
        /// <param name="value">The value to display.</param>
        /// <param name="maxLength">The maximum label length for alignment.</param>
        private void WriteFileStat(string resourceKey, string value, int maxLength)
        {
            var resourceValue = LanguageResource.GetResourceStringById(resourceKey);
            var output = resourceValue.FillRightWithPoints(maxLength);
            DisplayOutput.WriteLine($"  - {output} {value}");
        }

        /// <summary>
        /// Formats a file size in bytes to a human-readable string.
        /// </summary>
        /// <param name="bytes">The file size in bytes.</param>
        /// <returns>A formatted string like "1.5 KB" or "2.3 MB".</returns>
        /// <example>
        /// FormatFileSize(1536) returns "1.5 KB"
        /// FormatFileSize(1048576) returns "1 MB"
        /// </example>
        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;

            // Progressively divide by 1024 until the value is under 1024
            // or we've reached the largest unit (GB)
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
