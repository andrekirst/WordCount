using System.Collections.Generic;
using WordCount.Extensions;
using WordCount.Interfaces;

namespace WordCount.Implementations.TextSplitting
{
    /// <summary>
    /// Text splitting strategy for space-separated languages like English, German, French, etc.
    /// Uses regex to split on word boundaries, excluding numbers.
    /// </summary>
    public class SpaceBasedTextSplitStrategy : ITextSplitStrategy
    {
        /// <summary>
        /// Regex pattern for extracting words from text.
        /// Matches word boundaries, excludes digits, and handles abbreviations.
        /// </summary>
        private const string WordPattern = @"((\b[^\s\d]+\b)((?<=\.\w).)?)";

        /// <summary>
        /// Splits the input text into words using space-based rules.
        /// Words are identified by word boundaries, with numbers excluded.
        /// </summary>
        /// <param name="text">The text to split into words.</param>
        /// <returns>A list of words extracted from the text.</returns>
        public List<string> SplitText(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return new List<string>();
            }

            return text.SplitByRegex(WordPattern);
        }
    }
}
