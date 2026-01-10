using System.Text.RegularExpressions;

namespace WordCount.Helpers
{
    /// <summary>
    /// Contains compiled Unicode regex patterns for language detection and text processing.
    /// Centralizes all CJK and language-specific patterns to ensure consistency and performance.
    /// </summary>
    public static class UnicodePatterns
    {
        #region CJK Character Ranges (BMP Plane Only)
        // Note: CJK Extension B (\u20000-\u2A6DF) is on supplementary plane and requires
        // surrogate pair handling - excluded for simplicity as most common CJK is in BMP

        /// <summary>
        /// Chinese characters (CJK Unified Ideographs and Extension A)
        /// Range: U+4E00-U+9FFF (CJK Unified Ideographs) and U+3400-U+4DBF (Extension A)
        /// </summary>
        public const string ChineseRange = @"\u4E00-\u9FFF\u3400-\u4DBF";

        /// <summary>
        /// Japanese Hiragana characters
        /// Range: U+3040-U+309F
        /// </summary>
        public const string HiraganaRange = @"\u3040-\u309F";

        /// <summary>
        /// Japanese Katakana characters
        /// Range: U+30A0-U+30FF
        /// </summary>
        public const string KatakanaRange = @"\u30A0-\u30FF";

        /// <summary>
        /// Korean Hangul syllables and Jamo
        /// Range: U+AC00-U+D7AF (Hangul Syllables) and U+1100-U+11FF (Hangul Jamo)
        /// </summary>
        public const string KoreanRange = @"\uAC00-\uD7AF\u1100-\u11FF";

        /// <summary>
        /// German-specific characters (umlauts and eszett)
        /// </summary>
        public const string GermanIndicators = @"äöüßÄÖÜ";

        #endregion

        #region Compiled Patterns

        /// <summary>
        /// Pattern for matching Chinese characters
        /// </summary>
        public static readonly Regex ChinesePattern = new($"[{ChineseRange}]", RegexOptions.Compiled);

        /// <summary>
        /// Pattern for matching Japanese Hiragana characters
        /// </summary>
        public static readonly Regex HiraganaPattern = new($"[{HiraganaRange}]", RegexOptions.Compiled);

        /// <summary>
        /// Pattern for matching Japanese Katakana characters
        /// </summary>
        public static readonly Regex KatakanaPattern = new($"[{KatakanaRange}]", RegexOptions.Compiled);

        /// <summary>
        /// Pattern for matching Korean characters
        /// </summary>
        public static readonly Regex KoreanPattern = new($"[{KoreanRange}]", RegexOptions.Compiled);

        /// <summary>
        /// Pattern for matching German indicator characters
        /// </summary>
        public static readonly Regex GermanPattern = new($"[{GermanIndicators}]", RegexOptions.Compiled);

        /// <summary>
        /// Combined pattern for all CJK characters (Chinese, Japanese Hiragana/Katakana, Korean)
        /// </summary>
        public static readonly Regex CjkPattern = new(
            $"[{ChineseRange}{HiraganaRange}{KatakanaRange}{KoreanRange}]",
            RegexOptions.Compiled);

        #endregion

        #region Helper Methods

        /// <summary>
        /// Checks if a character is a CJK (Chinese, Japanese, Korean) character.
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <returns>
        /// <c>true</c> if the character is within CJK Unicode ranges;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method checks against the combined CJK pattern which includes:
        /// Chinese (CJK Unified Ideographs), Japanese (Hiragana and Katakana),
        /// and Korean (Hangul) character ranges.
        /// </remarks>
        public static bool IsCjkCharacter(char c)
        {
            return CjkPattern.IsMatch(c.ToString());
        }

        /// <summary>
        /// Counts the number of matches for a regex pattern in the specified text.
        /// </summary>
        /// <param name="text">The text to search within.</param>
        /// <param name="pattern">The compiled regex pattern to match.</param>
        /// <returns>The number of matches found in the text.</returns>
        public static int CountMatches(string text, Regex pattern)
        {
            return pattern.Matches(text).Count;
        }

        #endregion
    }
}
