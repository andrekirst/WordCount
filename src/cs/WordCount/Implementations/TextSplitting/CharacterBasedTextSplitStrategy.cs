using System.Collections.Generic;
using System.Text;
using WordCount.Extensions;
using WordCount.Helpers;
using WordCount.Interfaces;

namespace WordCount.Implementations.TextSplitting
{
    /// <summary>
    /// Text splitting strategy for character-based languages like Chinese, Japanese, and Korean.
    /// Treats each CJK character as a word, while preserving space-separated words for mixed text.
    /// </summary>
    public class CharacterBasedTextSplitStrategy : ITextSplitStrategy
    {
        /// <summary>
        /// Splits the input text into words using character-based rules for CJK languages.
        /// Each CJK character is treated as an individual word, while Latin/alphabetic
        /// characters are grouped into words based on whitespace separation.
        /// </summary>
        /// <param name="text">The text to split into words.</param>
        /// <returns>A list of words extracted from the text.</returns>
        public List<string> SplitText(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return new List<string>();
            }

            var words = new List<string>();
            var currentWord = new StringBuilder();
            var isCollectingLatinWord = false;

            foreach (var c in text)
            {
                if (UnicodePatterns.IsCjkCharacter(c))
                {
                    // If we were collecting a Latin word, add it first
                    if (isCollectingLatinWord && currentWord.Length > 0)
                    {
                        AddWord(words, currentWord);
                        currentWord.Clear();
                        isCollectingLatinWord = false;
                    }

                    // Each CJK character is treated as a word
                    words.Add(c.ToString());
                }
                else if (char.IsLetter(c))
                {
                    // Collect Latin/alphabetic characters into words
                    isCollectingLatinWord = true;
                    currentWord.Append(c);
                }
                else if (IsWordContinuationCharacter(c) && isCollectingLatinWord && currentWord.Length > 0)
                {
                    // Handle hyphens and apostrophes within words (e.g., "pre-condition", "Hamlet's")
                    currentWord.Append(c);
                }
                else
                {
                    // Whitespace or other separator - add collected word if any
                    if (isCollectingLatinWord && currentWord.Length > 0)
                    {
                        AddWord(words, currentWord);
                        currentWord.Clear();
                        isCollectingLatinWord = false;
                    }
                }
            }

            // Don't forget the last word
            if (currentWord.Length > 0)
            {
                AddWord(words, currentWord);
            }

            return words;
        }

        /// <summary>
        /// Checks if the character can continue a word (hyphens, apostrophes).
        /// </summary>
        private static bool IsWordContinuationCharacter(char c)
        {
            return c == '-' || c == '\'';
        }

        /// <summary>
        /// Adds a word to the list after trimming trailing hyphens.
        /// </summary>
        private static void AddWord(List<string> words, StringBuilder wordBuilder)
        {
            var word = wordBuilder.ToString().TrimEnd('-');
            if (word.Length > 0)
            {
                words.Add(word);
            }
        }
    }
}
