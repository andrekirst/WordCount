namespace WordCount.LiveEditor.Services;

/// <summary>
/// Represents analysis results for a single file.
/// </summary>
public class FileAnalysisDto
{
    /// <summary>Gets or sets the name of the analyzed file.</summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>Gets or sets the total word count in the file.</summary>
    public int WordCount { get; set; }

    /// <summary>Gets or sets the count of unique (distinct) words in the file.</summary>
    public int UniqueWordCount { get; set; }

    /// <summary>Gets or sets the average word length in the file.</summary>
    public double AverageWordLength { get; set; }

    /// <summary>Gets or sets the total character count in the file.</summary>
    public int CharacterCount { get; set; }

    /// <summary>Gets or sets the number of paragraphs in the file.</summary>
    public int ParagraphCount { get; set; }
}

/// <summary>
/// Represents aggregate statistics computed across multiple files.
/// </summary>
public class AggregateStatisticsDto
{
    /// <summary>Gets or sets the total number of files analyzed.</summary>
    public int TotalFiles { get; set; }

    /// <summary>Gets or sets the total word count across all files.</summary>
    public int TotalWords { get; set; }

    /// <summary>Gets or sets the sum of unique word counts from each file (not cross-file distinct).</summary>
    public int TotalUniqueWords { get; set; }

    /// <summary>Gets or sets the weighted average word length across all files.</summary>
    public double AverageWordLength { get; set; }

    /// <summary>Gets or sets the total character count across all files.</summary>
    public int TotalCharacters { get; set; }

    /// <summary>Gets or sets the total paragraph count across all files.</summary>
    public int TotalParagraphs { get; set; }
}

/// <summary>
/// Represents the complete result of a multi-file analysis operation.
/// </summary>
public class MultiFileAnalysisDto
{
    /// <summary>Gets or sets the list of individual file analysis results.</summary>
    public List<FileAnalysisDto> FileResults { get; set; } = new();

    /// <summary>Gets or sets the aggregate statistics across all analyzed files.</summary>
    public AggregateStatisticsDto AggregateStatistics { get; set; } = new();
}

/// <summary>
/// Service interface for analyzing multiple files/documents simultaneously.
/// </summary>
public interface IMultiFileAnalysisService
{
    /// <summary>
    /// Analyzes multiple text files and returns both individual and aggregate statistics.
    /// </summary>
    /// <param name="fileContents">Dictionary mapping file names to their text content.</param>
    /// <returns>A result containing individual file statistics and aggregate metrics.</returns>
    MultiFileAnalysisDto AnalyzeMultipleTexts(Dictionary<string, string> fileContents);
}

/// <summary>
/// Implementation of multi-file analysis service that aggregates statistics across multiple documents.
/// </summary>
public class MultiFileAnalysisService : IMultiFileAnalysisService
{
    private readonly ITextStatisticsService _textStatisticsService;

    /// <summary>
    /// Initializes a new instance of the MultiFileAnalysisService.
    /// </summary>
    /// <param name="textStatisticsService">The text statistics service for analyzing individual files.</param>
    /// <exception cref="ArgumentNullException">Thrown when textStatisticsService is null.</exception>
    public MultiFileAnalysisService(ITextStatisticsService textStatisticsService)
    {
        _textStatisticsService = textStatisticsService ?? throw new ArgumentNullException(nameof(textStatisticsService));
    }

    /// <inheritdoc />
    public MultiFileAnalysisDto AnalyzeMultipleTexts(Dictionary<string, string> fileContents)
    {
        ArgumentNullException.ThrowIfNull(fileContents);

        var result = new MultiFileAnalysisDto();
        var totalWordLengthSum = 0.0;
        var totalWordCount = 0;

        foreach (var (fileName, content) in fileContents)
        {
            var stats = _textStatisticsService.Analyze(content);

            var fileResult = new FileAnalysisDto
            {
                FileName = fileName,
                WordCount = stats.WordCount,
                UniqueWordCount = stats.UniqueWordCount,
                AverageWordLength = stats.AverageWordLength,
                CharacterCount = stats.CharacterCount,
                ParagraphCount = stats.ParagraphCount
            };

            result.FileResults.Add(fileResult);

            // Aggregate statistics
            result.AggregateStatistics.TotalWords += stats.WordCount;
            result.AggregateStatistics.TotalCharacters += stats.CharacterCount;
            result.AggregateStatistics.TotalParagraphs += stats.ParagraphCount;

            // Calculate weighted average word length
            if (stats.WordCount > 0)
            {
                totalWordLengthSum += stats.AverageWordLength * stats.WordCount;
                totalWordCount += stats.WordCount;
            }
        }

        result.AggregateStatistics.TotalFiles = result.FileResults.Count;
        result.AggregateStatistics.AverageWordLength = totalWordCount > 0
            ? Math.Round(totalWordLengthSum / totalWordCount, 2)
            : 0;

        // Count unique words across all files (simplified - would need actual word extraction)
        result.AggregateStatistics.TotalUniqueWords = result.FileResults.Sum(f => f.UniqueWordCount);

        return result;
    }
}
