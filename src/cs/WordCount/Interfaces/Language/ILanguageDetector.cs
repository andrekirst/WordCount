using WordCount.Models.Results;

namespace WordCount.Interfaces.Language
{
    /// <summary>
    /// Interface for detecting the language of input text.
    /// Implementations analyze text to determine the primary language and whether
    /// character-based word counting should be used (for CJK languages).
    /// </summary>
    public interface ILanguageDetector
    {
        /// <summary>
        /// Detects the language of the provided text.
        /// </summary>
        /// <param name="text">The text to analyze for language detection. Can be null or empty.</param>
        /// <returns>
        /// A <see cref="LanguageDetectionResult"/> containing:
        /// - The detected language code (e.g., "en", "de", "zh", "ja", "ko")
        /// - Whether character-based counting should be used
        /// - Whether the detection was automatic
        /// </returns>
        /// <remarks>
        /// Supported languages:
        /// <list type="bullet">
        ///   <item><description>English (en) - default, space-based</description></item>
        ///   <item><description>German (de) - detected by umlauts/eszett, space-based</description></item>
        ///   <item><description>Chinese (zh) - detected by CJK ideographs, character-based</description></item>
        ///   <item><description>Japanese (ja) - detected by Hiragana/Katakana, character-based</description></item>
        ///   <item><description>Korean (ko) - detected by Hangul, character-based</description></item>
        /// </list>
        /// </remarks>
        LanguageDetectionResult DetectLanguage(string text);
    }
}
