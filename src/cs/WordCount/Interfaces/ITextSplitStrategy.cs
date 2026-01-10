using System.Collections.Generic;

namespace WordCount.Interfaces
{
    /// <summary>
    /// Interface for text splitting strategies.
    /// Different implementations handle different types of languages:
    /// - Space-based for Western languages (English, German, French, etc.)
    /// - Character-based for CJK languages (Chinese, Japanese, Korean)
    /// </summary>
    public interface ITextSplitStrategy
    {
        /// <summary>
        /// Splits the input text into individual words.
        /// </summary>
        /// <param name="text">The text to split into words. Can be null or empty.</param>
        /// <returns>
        /// A list of words extracted from the text.
        /// Returns an empty list if the input is null, empty, or contains no recognizable words.
        /// </returns>
        /// <remarks>
        /// The definition of a "word" depends on the implementation:
        /// <list type="bullet">
        ///   <item><description>Space-based: Words are sequences of letters separated by whitespace</description></item>
        ///   <item><description>Character-based: Each CJK character is treated as an individual word</description></item>
        /// </list>
        /// </remarks>
        List<string> SplitText(string text);
    }
}
