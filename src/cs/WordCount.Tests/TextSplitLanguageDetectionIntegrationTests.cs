using FluentAssertions;
using WordCount.Implementations;
using WordCount.Interfaces;
using Xunit;

namespace WordCount.Tests;

/// <summary>
/// Integration tests for the TextSplit class with automatic language detection.
/// These tests verify that the TextSplit class correctly integrates with the
/// LanguageDetector and appropriate text splitting strategies.
/// </summary>
public class TextSplitLanguageDetectionIntegrationTests
{
    private readonly ITextSplit _sut;

    public TextSplitLanguageDetectionIntegrationTests()
    {
        _sut = new TextSplit();
    }

    #region English Text Integration

    [Fact]
    public void Split_EnglishText_ShouldDetectEnglishAndUseSpaceBasedSplitting()
    {
        // Arrange
        const string text = "Hello world this is a test";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(6);
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void Split_EnglishTextWithPunctuation_ShouldHandleCorrectly()
    {
        // Arrange
        const string text = "Hello, world! How are you?";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().Contain("Hello");
        result.Words.Should().Contain("world");
        result.DetectedLanguage.Should().Be("en");
        result.IsCharacterBased.Should().BeFalse();
    }

    #endregion

    #region German Text Integration

    [Fact]
    public void Split_GermanTextWithUmlauts_ShouldDetectGermanAndUseSpaceBasedSplitting()
    {
        // Arrange
        const string text = "Draußen ist es schön";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(4);
        result.Words.Should().ContainInOrder("Draußen", "ist", "es", "schön");
        result.DetectedLanguage.Should().Be("de");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void Split_GermanTextWithEszett_ShouldPreserveEszett()
    {
        // Arrange
        const string text = "Die Straße ist groß";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().Contain("Straße");
        result.Words.Should().Contain("groß");
        result.DetectedLanguage.Should().Be("de");
        result.IsCharacterBased.Should().BeFalse();
    }

    [Fact]
    public void Split_GermanTextWithDigits_ShouldExcludeDigitsAndPreserveUmlauts()
    {
        // Arrange
        const string text = "Draußen ist es schön 1";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(4);
        result.Words.Should().NotContain("1");
        result.DetectedLanguage.Should().Be("de");
    }

    #endregion

    #region Chinese Text Integration

    [Fact]
    public void Split_ChineseText_ShouldDetectChineseAndUseCharacterBasedSplitting()
    {
        // Arrange
        const string text = "你好世界";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(4);
        result.Words.Should().ContainInOrder("你", "好", "世", "界");
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void Split_ChineseTextWithPunctuation_ShouldIgnorePunctuation()
    {
        // Arrange
        const string text = "你好！世界。";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(4);
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void Split_MixedChineseAndEnglish_ShouldHandleBothCorrectly()
    {
        // Arrange - 1 English word + 2 Chinese characters = 3 words
        const string text = "Hello 世界";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(3);
        result.Words.Should().Contain("Hello");
        result.Words.Should().Contain("世");
        result.Words.Should().Contain("界");
        result.DetectedLanguage.Should().Be("zh");
        result.IsCharacterBased.Should().BeTrue();
    }

    #endregion

    #region Japanese Text Integration

    [Fact]
    public void Split_JapaneseHiragana_ShouldDetectJapaneseAndUseCharacterBasedSplitting()
    {
        // Arrange - "Konnichiwa" in Hiragana
        const string text = "こんにちは";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(5);
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void Split_JapaneseKatakana_ShouldDetectJapaneseAndUseCharacterBasedSplitting()
    {
        // Arrange - "Computer" in Katakana
        const string text = "コンピュータ";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(6);
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void Split_JapaneseMixedWithKanji_ShouldDetectJapanese()
    {
        // Arrange - Japanese text with Kanji and Hiragana
        const string text = "日本語はすばらしい";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void Split_MixedJapaneseAndEnglish_ShouldHandleBothCorrectly()
    {
        // Arrange
        const string text = "Hello こんにちは";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().Contain("Hello");
        result.DetectedLanguage.Should().Be("ja");
        result.IsCharacterBased.Should().BeTrue();
    }

    #endregion

    #region Korean Text Integration

    [Fact]
    public void Split_KoreanText_ShouldDetectKoreanAndUseCharacterBasedSplitting()
    {
        // Arrange - "Hello" in Korean
        const string text = "안녕하세요";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(5);
        result.DetectedLanguage.Should().Be("ko");
        result.IsCharacterBased.Should().BeTrue();
    }

    #endregion

    #region Edge Cases

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public void Split_EmptyOrNullText_ShouldReturnEmptyResult(string? text)
#pragma warning restore CS8632
    {
        // Act
        var result = _sut.Split(text!);

        // Assert
        result.Should().NotBeNull();
        result.Words.Should().BeEmpty();
        result.WordsAvailable.Should().BeFalse();
    }

    [Fact]
    public void Split_NumbersOnly_ShouldReturnEmptyWords()
    {
        // Arrange
        const string text = "12345";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().BeEmpty();
        result.DetectedLanguage.Should().Be("en");
    }

    #endregion

    #region Result Properties Verification

    [Fact]
    public void Split_ValidText_ShouldSetWordsAvailableTrue()
    {
        // Arrange
        const string text = "Hello world";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.WordsAvailable.Should().BeTrue();
    }

    [Fact]
    public void Split_ChineseText_ResultShouldContainLanguageMetadata()
    {
        // Arrange
        const string text = "中文文本";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.DetectedLanguage.Should().NotBeNullOrEmpty();
        result.IsCharacterBased.Should().BeTrue();
    }

    [Fact]
    public void Split_EnglishText_ResultShouldContainLanguageMetadata()
    {
        // Arrange
        const string text = "English text";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.DetectedLanguage.Should().NotBeNullOrEmpty();
        result.IsCharacterBased.Should().BeFalse();
    }

    #endregion

    #region Consistency with Existing Tests

    [Fact]
    public void Split_HumptyDumptyText_ShouldReturn10Words()
    {
        // Arrange - This test maintains consistency with existing TextSplitTests
        const string text = "Humpty-Dumpty sat on a wall. Humpty-Dumpty had a great fall.";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(10);
    }

    [Fact]
    public void Split_TextWithUnicode_ShouldReturn2Words()
    {
        // Arrange - This test maintains consistency with existing TextSplitTests
        const string text = "Hello André";

        // Act
        var result = _sut.Split(text);

        // Assert
        result.Words.Should().HaveCount(2);
        result.Words.Should().ContainInOrder("Hello", "André");
    }

    [Fact]
    public void Split_LongComplexSentence_ShouldMatchExistingBehavior()
    {
        // Arrange - This test maintains consistency with existing TextSplitTests
        const string text = "Longworth, however, in desperate need of money, killed the preacher with Hamlet's sword and stole the manuscript";

        // Act
        var result = _sut.Split(text);

        // Assert
        var expected = new[] { "Longworth", "however", "in", "desperate", "need", "of", "money", "killed", "the", "preacher", "with", "Hamlet's", "sword", "and", "stole", "the", "manuscript" };
        result.Words.Should().BeEquivalentTo(expected);
    }

    #endregion
}
