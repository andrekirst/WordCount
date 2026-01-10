using FluentAssertions;
using WordCount.Implementations.Language;
using WordCount.Interfaces.Language;
using Xunit;

namespace WordCount.Tests.Language;

/// <summary>
/// Unit tests for the LanguageDetector class that automatically detects
/// the language of input text and determines if character-based counting should be used.
/// </summary>
public class LanguageDetectorTests
{
    private readonly ILanguageDetector _sut;

    public LanguageDetectorTests()
    {
        _sut = new LanguageDetector();
    }

    #region English Language Detection

    [Fact]
    public void DetectLanguage_EnglishText_ShouldReturnEnglish()
    {
        // Arrange
        const string text = "Hello world this is a test";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
        result.IsAutoDetected.Should().BeTrue();
    }

    [Fact]
    public void DetectLanguage_EnglishTextWithPunctuation_ShouldReturnEnglish()
    {
        // Arrange
        const string text = "Hello, world! How are you?";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void DetectLanguage_EnglishTextWithNumbers_ShouldReturnEnglish()
    {
        // Arrange
        const string text = "There are 123 items in the list";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    #endregion

    #region German Language Detection

    [Fact]
    public void DetectLanguage_GermanTextWithUmlaut_ShouldReturnGerman()
    {
        // Arrange
        const string text = "Draußen ist es schön";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("de");
        result.IsCharacterBased.Should().BeFalse();
        result.IsAutoDetected.Should().BeTrue();
    }

    [Fact]
    public void DetectLanguage_GermanTextWithEszett_ShouldReturnGerman()
    {
        // Arrange
        const string text = "Die Straße ist lang";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("de");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Theory]
    [InlineData("ä")]
    [InlineData("ö")]
    [InlineData("ü")]
    [InlineData("ß")]
    [InlineData("Ä")]
    [InlineData("Ö")]
    [InlineData("Ü")]
    public void DetectLanguage_TextWithGermanCharacters_ShouldReturnGerman(string germanChar)
    {
        // Arrange
        var text = $"Text with {germanChar} character";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("de");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void DetectLanguage_GermanTextWithUmlauts_ShouldNotBeCharacterBased()
    {
        // Arrange - This is a critical test to ensure German text is NOT treated as character-based
        const string text = "Fröhliche Grüße";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.IsCharacterBased.Should().BeFalse("German text should use space-based counting");
    }

    #endregion

    #region Chinese Language Detection

    [Fact]
    public void DetectLanguage_ChineseText_ShouldReturnChinese()
    {
        // Arrange
        const string text = "你好世界";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
        result.IsAutoDetected.Should().BeTrue();
    }

    [Fact]
    public void DetectLanguage_ChineseTextWithPunctuation_ShouldReturnChinese()
    {
        // Arrange - Chinese punctuation and characters
        const string text = "你好！世界。";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Theory]
    [InlineData("中文", "zh")]
    [InlineData("汉字", "zh")]
    [InlineData("繁體", "zh")]
    public void DetectLanguage_VariousChineseCharacters_ShouldReturnChinese(string text, string expectedLanguage)
    {
        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be(expectedLanguage);
        result.IsCharacterBased.Should().BeTrue();
    }

    #endregion

    #region Japanese Language Detection

    [Fact]
    public void DetectLanguage_JapaneseHiragana_ShouldReturnJapanese()
    {
        // Arrange - "Konnichiwa" in Hiragana
        const string text = "こんにちは";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
        result.IsAutoDetected.Should().BeTrue();
    }

    [Fact]
    public void DetectLanguage_JapaneseKatakana_ShouldReturnJapanese()
    {
        // Arrange - "Computer" in Katakana
        const string text = "コンピューター";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void DetectLanguage_JapaneseMixedWithKanji_ShouldReturnJapanese()
    {
        // Arrange - Japanese text with Kanji (Chinese characters) and Hiragana
        // If Hiragana/Katakana is present, it should be detected as Japanese
        const string text = "日本語はすばらしい";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("ja",
            "Presence of Hiragana should indicate Japanese even with Kanji");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Theory]
    [InlineData("あいうえお")] // Hiragana
    [InlineData("アイウエオ")] // Katakana
    [InlineData("ひらがな")] // Hiragana
    [InlineData("カタカナ")] // Katakana
    public void DetectLanguage_JapaneseKanaCharacters_ShouldReturnJapanese(string text)
    {
        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
    }

    #endregion

    #region Korean Language Detection

    [Fact]
    public void DetectLanguage_KoreanText_ShouldReturnKorean()
    {
        // Arrange - "Hello" in Korean
        const string text = "안녕하세요";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("ko");
        result.IsCharacterBased.Should().BeTrue();
        result.IsAutoDetected.Should().BeTrue();
    }

    [Theory]
    [InlineData("한글")]
    [InlineData("대한민국")]
    [InlineData("서울")]
    public void DetectLanguage_VariousKoreanText_ShouldReturnKorean(string text)
    {
        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("ko");
        result.IsCharacterBased.Should().BeTrue();
    }

    #endregion

    #region Mixed Language Detection

    [Fact]
    public void DetectLanguage_MixedChineseAndEnglish_ShouldReturnChinese()
    {
        // Arrange - Mixed content with Chinese characters
        const string text = "Hello 世界";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void DetectLanguage_MixedJapaneseAndEnglish_ShouldReturnJapanese()
    {
        // Arrange - English with Japanese Hiragana
        const string text = "Hello こんにちは World";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
    }

    #endregion

    #region Edge Cases and Error Handling

    [Theory]
    [InlineData(null)]
    [InlineData("")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public void DetectLanguage_NullOrEmptyText_ShouldReturnEnglishDefault(string? text)
#pragma warning restore CS8632
    {
        // Act
        var result = _sut.DetectLanguage(text!);

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void DetectLanguage_WhitespaceOnly_ShouldReturnEnglishDefault()
    {
        // Arrange
        const string text = "   \t\n  ";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void DetectLanguage_NumbersOnly_ShouldReturnEnglishDefault()
    {
        // Arrange
        const string text = "123456789";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void DetectLanguage_PunctuationOnly_ShouldReturnEnglishDefault()
    {
        // Arrange
        const string text = "!@#$%^&*()";

        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    #endregion

    #region Character-Based Flag Verification

    [Theory]
    [InlineData("Hello world", false)]
    [InlineData("Guten Tag", false)]
    [InlineData("你好", true)]
    [InlineData("こんにちは", true)]
    [InlineData("안녕", true)]
    [InlineData("Grüße", false)]
    public void DetectLanguage_VariousTexts_ShouldSetIsCharacterBasedCorrectly(
        string text, bool expectedIsCharacterBased)
    {
        // Act
        var result = _sut.DetectLanguage(text);

        // Assert
        result.IsCharacterBased.Should().Be(expectedIsCharacterBased);
    }

    #endregion
}
