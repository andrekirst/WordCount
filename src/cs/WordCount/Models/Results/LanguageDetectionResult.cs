namespace WordCount.Models.Results
{
    /// <summary>
    /// Represents the result of automatic language detection.
    /// Contains information about the detected language and how text should be processed.
    /// </summary>
    public class LanguageDetectionResult
    {
        /// <summary>
        /// Creates a new instance with default values (English, space-based).
        /// </summary>
        public LanguageDetectionResult()
        {
        }

        /// <summary>
        /// Creates a new instance with the specified language and character-based flag.
        /// </summary>
        /// <param name="detectedLanguage">The ISO 639-1 language code (e.g., "en", "zh", "ja").</param>
        /// <param name="isCharacterBased">Whether character-based word counting should be used.</param>
        public LanguageDetectionResult(string detectedLanguage, bool isCharacterBased)
        {
            DetectedLanguage = detectedLanguage;
            IsCharacterBased = isCharacterBased;
        }

        /// <summary>
        /// Gets or sets the detected ISO 639-1 language code.
        /// </summary>
        /// <value>
        /// The language code. Supported values:
        /// <list type="bullet">
        ///   <item><description>"en" - English (default)</description></item>
        ///   <item><description>"de" - German</description></item>
        ///   <item><description>"zh" - Chinese</description></item>
        ///   <item><description>"ja" - Japanese</description></item>
        ///   <item><description>"ko" - Korean</description></item>
        /// </list>
        /// </value>
        public string DetectedLanguage { get; set; } = "en";

        /// <summary>
        /// Gets or sets whether character-based word counting should be used.
        /// </summary>
        /// <value>
        /// <c>true</c> for CJK languages where each character counts as a word;
        /// <c>false</c> for space-separated languages where words are delimited by whitespace.
        /// </value>
        public bool IsCharacterBased { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the language was automatically detected from the text.
        /// </summary>
        /// <value>
        /// <c>true</c> if the language was detected automatically;
        /// <c>false</c> if the language was specified manually or defaulted.
        /// </value>
        public bool IsAutoDetected { get; set; } = false;
    }
}
