using WordCount.Extensions;
using WordCount.Helpers;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;

namespace WordCount.Implementations.Language
{
    /// <summary>
    /// Detects the language of input text and determines if character-based word counting should be used.
    /// Supports detection of Chinese, Japanese, Korean, German, and defaults to English.
    /// </summary>
    public class LanguageDetector : ILanguageDetector
    {
        /// <summary>
        /// Detects the language of the provided text.
        /// </summary>
        /// <param name="text">The text to analyze for language detection.</param>
        /// <returns>A result containing the detected language and whether character-based counting is needed.</returns>
        public LanguageDetectionResult DetectLanguage(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return new LanguageDetectionResult("en", false);
            }

            var chineseCount = UnicodePatterns.CountMatches(text, UnicodePatterns.ChinesePattern);
            var hiraganaCount = UnicodePatterns.CountMatches(text, UnicodePatterns.HiraganaPattern);
            var katakanaCount = UnicodePatterns.CountMatches(text, UnicodePatterns.KatakanaPattern);
            var koreanCount = UnicodePatterns.CountMatches(text, UnicodePatterns.KoreanPattern);

            var japaneseCount = hiraganaCount + katakanaCount;
            var totalCjkCount = chineseCount + japaneseCount + koreanCount;

            // If there are significant CJK characters, detect the primary language
            if (totalCjkCount > 0)
            {
                // Determine primary CJK language based on character counts
                // Japanese text often mixes Chinese characters (Kanji) with Hiragana/Katakana
                if (japaneseCount > 0)
                {
                    // Presence of Hiragana or Katakana indicates Japanese
                    return new LanguageDetectionResult("ja", true) { IsAutoDetected = true };
                }

                if (koreanCount > chineseCount)
                {
                    return new LanguageDetectionResult("ko", true) { IsAutoDetected = true };
                }

                if (chineseCount > 0)
                {
                    return new LanguageDetectionResult("zh", true) { IsAutoDetected = true };
                }
            }

            // Check for German-specific characters (umlauts, eszett)
            var germanIndicators = UnicodePatterns.CountMatches(text, UnicodePatterns.GermanPattern);
            if (germanIndicators > 0)
            {
                return new LanguageDetectionResult("de", false) { IsAutoDetected = true };
            }

            // Default to English for space-separated languages
            return new LanguageDetectionResult("en", false) { IsAutoDetected = true };
        }
    }
}
