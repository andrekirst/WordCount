using System.Text.RegularExpressions;
using WordCount.Implementations;
using WordCount.Interfaces;

namespace WordCount.LiveEditor.Services;

/// <summary>
/// Represents comprehensive real-time text statistics for the live editor.
/// Contains word counts, character counts, structural analysis, and language detection results.
/// </summary>
public class TextStatistics
{
    /// <summary>Gets or sets the total number of words in the text.</summary>
    public int WordCount { get; set; }

    /// <summary>Gets or sets the total character count including spaces and whitespace.</summary>
    public int CharacterCount { get; set; }

    /// <summary>Gets or sets the character count excluding spaces, tabs, and newlines.</summary>
    public int CharacterCountNoSpaces { get; set; }

    /// <summary>Gets or sets the number of sentences detected in the text.</summary>
    public int SentenceCount { get; set; }

    /// <summary>Gets or sets the number of paragraphs (text blocks separated by double newlines).</summary>
    public int ParagraphCount { get; set; }

    /// <summary>Gets or sets the count of unique (case-insensitive distinct) words.</summary>
    public int UniqueWordCount { get; set; }

    /// <summary>Gets or sets the average word length, rounded to 2 decimal places.</summary>
    public double AverageWordLength { get; set; }

    /// <summary>Gets or sets the total number of lines in the text.</summary>
    public int LineCount { get; set; }

    /// <summary>Gets or sets the detected language code (e.g., "en", "de", "zh").</summary>
    public string DetectedLanguage { get; set; } = "en";

    /// <summary>Gets or sets a value indicating whether character-based counting is used (for CJK languages).</summary>
    public bool IsCharacterBased { get; set; }

    /// <summary>Gets or sets the human-readable name of the detected language.</summary>
    public string LanguageDisplayName { get; set; } = "English";
}

/// <summary>
/// Service interface for analyzing text and providing real-time statistics.
/// </summary>
public interface ITextStatisticsService
{
    /// <summary>
    /// Analyzes the provided text and returns comprehensive statistics.
    /// </summary>
    /// <param name="text">The text to analyze. Can be null or empty.</param>
    /// <returns>A TextStatistics object containing all computed metrics.</returns>
    TextStatistics Analyze(string text);
}

/// <summary>
/// Implementation of the text statistics analysis service with language-aware word counting.
/// Provides real-time text analysis for words, characters, sentences, paragraphs, and more.
/// </summary>
public class TextStatisticsService : ITextStatisticsService
{
    /// <summary>
    /// Regex pattern for detecting sentence endings (period, exclamation, question mark)
    /// </summary>
    private static readonly Regex SentencePattern = new(@"[.!?]+(?=\s|$)", RegexOptions.Compiled);

    /// <summary>
    /// Regex pattern for detecting paragraph breaks (double newlines)
    /// </summary>
    private static readonly Regex ParagraphPattern = new(@"(\r?\n){2,}", RegexOptions.Compiled);

    /// <summary>
    /// Characters to remove when calculating character count without spaces
    /// </summary>
    private static readonly char[] WhitespaceChars = { ' ', '\n', '\r', '\t' };

    /// <summary>
    /// Language-aware text splitter from the main WordCount project
    /// </summary>
    private readonly ITextSplit _textSplit;

    /// <summary>
    /// Mapping of language codes to display names
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string> LanguageNames = new Dictionary<string, string>
    {
        { "en", "English" },
        { "de", "German" },
        { "zh", "Chinese" },
        { "ja", "Japanese" },
        { "ko", "Korean" }
    };

    /// <summary>
    /// Initializes a new instance of the TextStatisticsService with a default TextSplit implementation.
    /// </summary>
    public TextStatisticsService() : this(new TextSplit())
    {
    }

    /// <summary>
    /// Initializes a new instance of the TextStatisticsService with a custom ITextSplit implementation.
    /// </summary>
    /// <param name="textSplit">The text splitting service to use for word tokenization.</param>
    /// <exception cref="ArgumentNullException">Thrown when textSplit is null.</exception>
    public TextStatisticsService(ITextSplit textSplit)
    {
        _textSplit = textSplit ?? throw new ArgumentNullException(nameof(textSplit));
    }

    /// <summary>
    /// Analyzes the provided text and returns comprehensive statistics.
    /// </summary>
    /// <param name="text">The text to analyze. Can be null or empty.</param>
    /// <returns>A TextStatistics object containing all computed metrics.</returns>
    public TextStatistics Analyze(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new TextStatistics();
        }

        // Use the language-aware text splitting
        var textSplitResult = _textSplit.Split(text);
        var words = textSplitResult.Words;
        var distinctWords = words.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        var languageName = LanguageNames.TryGetValue(textSplitResult.DetectedLanguage, out var name)
            ? name
            : textSplitResult.DetectedLanguage;

        return new TextStatistics
        {
            WordCount = words.Count,
            CharacterCount = text.Length,
            CharacterCountNoSpaces = CountCharactersWithoutWhitespace(text),
            SentenceCount = CountSentences(text),
            ParagraphCount = CountParagraphs(text),
            UniqueWordCount = distinctWords.Count,
            AverageWordLength = CalculateAverageWordLength(words),
            LineCount = CountLines(text),
            DetectedLanguage = textSplitResult.DetectedLanguage,
            IsCharacterBased = textSplitResult.IsCharacterBased,
            LanguageDisplayName = languageName
        };
    }

    /// <summary>
    /// Counts characters in the text excluding whitespace characters.
    /// </summary>
    private static int CountCharactersWithoutWhitespace(string text)
    {
        return text.Count(c => !WhitespaceChars.Contains(c));
    }

    /// <summary>
    /// Calculates the average word length, rounded to 2 decimal places.
    /// </summary>
    private static double CalculateAverageWordLength(IList<string> words)
    {
        if (words.Count == 0)
        {
            return 0;
        }

        return Math.Round(words.Average(w => w.Length), 2);
    }

    /// <summary>
    /// Counts the number of sentences in the text based on sentence-ending punctuation.
    /// </summary>
    private static int CountSentences(string text)
    {
        var matches = SentencePattern.Matches(text);
        return matches.Count > 0 ? matches.Count : (text.Trim().Length > 0 ? 1 : 0);
    }

    /// <summary>
    /// Counts the number of paragraphs in the text, defined as text blocks separated by double newlines.
    /// </summary>
    private static int CountParagraphs(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        // Split by double newlines (paragraph breaks) using compiled pattern
        var paragraphs = ParagraphPattern.Split(text)
            .Count(p => !string.IsNullOrWhiteSpace(p));

        return paragraphs > 0 ? paragraphs : 1;
    }

    /// <summary>
    /// Counts the number of lines in the text, handling different newline formats.
    /// </summary>
    private static int CountLines(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        return text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Length;
    }
}
