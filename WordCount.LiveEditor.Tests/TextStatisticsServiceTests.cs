using FluentAssertions;
using WordCount.LiveEditor.Services;
using Xunit;

namespace WordCount.LiveEditor.Tests;

/// <summary>
/// Unit tests for the TextStatisticsService class
/// Tests cover word counting, character counting, sentence counting,
/// paragraph counting, and other text statistics functionality
/// </summary>
public class TextStatisticsServiceTests
{
    private readonly TextStatisticsService _sut;

    public TextStatisticsServiceTests()
    {
        _sut = new TextStatisticsService();
    }

    #region Word Count Tests

    [Fact]
    public void Analyze_WithEmptyString_ShouldReturnZeroWordCount()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithNullString_ShouldReturnZeroWordCount()
    {
        // Arrange
        string? text = null;

        // Act
        var result = _sut.Analyze(text!);

        // Assert
        result.WordCount.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithSingleWord_ShouldReturnWordCountOf1()
    {
        // Arrange
        var text = "Hello";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithTwoWords_ShouldReturnWordCountOf2()
    {
        // Arrange
        var text = "Hello world";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(2);
    }

    [Theory]
    [InlineData("One two three", 3)]
    [InlineData("The quick brown fox jumps", 5)]
    [InlineData("This is a test sentence with multiple words", 8)]
    public void Analyze_WithVariousTexts_ShouldReturnCorrectWordCount(string text, int expectedWordCount)
    {
        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(expectedWordCount);
    }

    [Fact]
    public void Analyze_WithMultipleSpaces_ShouldCountWordsCorrectly()
    {
        // Arrange
        var text = "Hello    world";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(2);
    }

    [Fact]
    public void Analyze_WithHyphenatedWord_ShouldCountAsOneWord()
    {
        // Arrange
        var text = "well-known";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithApostrophe_ShouldCountAsOneWord()
    {
        // Arrange
        var text = "don't";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(1);
    }

    #endregion

    #region Character Count Tests

    [Fact]
    public void Analyze_WithEmptyString_ShouldReturnZeroCharacterCount()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.CharacterCount.Should().Be(0);
        result.CharacterCountNoSpaces.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithSingleWord_ShouldReturnCorrectCharacterCount()
    {
        // Arrange
        var text = "Hello";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.CharacterCount.Should().Be(5);
        result.CharacterCountNoSpaces.Should().Be(5);
    }

    [Fact]
    public void Analyze_WithTwoWords_ShouldCountSpaceInCharacterCount()
    {
        // Arrange
        var text = "Hello world";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.CharacterCount.Should().Be(11);
        result.CharacterCountNoSpaces.Should().Be(10);
    }

    [Fact]
    public void Analyze_WithMultipleSpaces_ShouldExcludeSpacesFromNoSpacesCount()
    {
        // Arrange
        var text = "Hello    world";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.CharacterCount.Should().Be(14);
        result.CharacterCountNoSpaces.Should().Be(10);
    }

    [Fact]
    public void Analyze_WithNewlines_ShouldExcludeNewlinesFromNoSpacesCount()
    {
        // Arrange
        var text = "Hello\nworld";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.CharacterCount.Should().Be(11);
        result.CharacterCountNoSpaces.Should().Be(10);
    }

    [Fact]
    public void Analyze_WithTabs_ShouldExcludeTabsFromNoSpacesCount()
    {
        // Arrange
        var text = "Hello\tworld";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.CharacterCount.Should().Be(11);
        result.CharacterCountNoSpaces.Should().Be(10);
    }

    #endregion

    #region Sentence Count Tests

    [Fact]
    public void Analyze_WithEmptyString_ShouldReturnZeroSentenceCount()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithSingleSentence_ShouldReturnSentenceCountOf1()
    {
        // Arrange
        var text = "This is a sentence.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithTwoSentences_ShouldReturnSentenceCountOf2()
    {
        // Arrange
        var text = "This is sentence one. This is sentence two.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(2);
    }

    [Fact]
    public void Analyze_WithExclamationMark_ShouldCountAsSentence()
    {
        // Arrange
        var text = "Hello world!";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithQuestionMark_ShouldCountAsSentence()
    {
        // Arrange
        var text = "Is this a question?";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithMixedPunctuation_ShouldCountAllSentences()
    {
        // Arrange
        var text = "Statement one. Question two? Exclamation three!";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(3);
    }

    [Fact]
    public void Analyze_WithTextWithoutPunctuation_ShouldReturnSentenceCountOf1()
    {
        // Arrange
        var text = "This is text without ending punctuation";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithMultiplePunctuationMarks_ShouldCountAsOneSentence()
    {
        // Arrange
        var text = "What?!";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.SentenceCount.Should().Be(1);
    }

    #endregion

    #region Paragraph Count Tests

    [Fact]
    public void Analyze_WithEmptyString_ShouldReturnZeroParagraphCount()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithWhitespaceOnly_ShouldReturnZeroParagraphCount()
    {
        // Arrange
        var text = "   \t\n  ";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithSingleParagraph_ShouldReturnParagraphCountOf1()
    {
        // Arrange
        var text = "This is a single paragraph.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithTwoParagraphsSeparatedByDoubleNewline_ShouldReturnParagraphCountOf2()
    {
        // Arrange
        var text = "First paragraph.\n\nSecond paragraph.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(2);
    }

    [Fact]
    public void Analyze_WithThreeParagraphs_ShouldReturnParagraphCountOf3()
    {
        // Arrange
        var text = "First.\n\nSecond.\n\nThird.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(3);
    }

    [Fact]
    public void Analyze_WithSingleNewline_ShouldReturnParagraphCountOf1()
    {
        // Arrange
        var text = "Line one.\nLine two.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithWindowsStyleNewlines_ShouldReturnCorrectParagraphCount()
    {
        // Arrange
        var text = "First paragraph.\r\n\r\nSecond paragraph.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(2);
    }

    #endregion

    #region Unique Word Count Tests

    [Fact]
    public void Analyze_WithEmptyString_ShouldReturnZeroUniqueWordCount()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.UniqueWordCount.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithRepeatedWords_ShouldReturnCorrectUniqueWordCount()
    {
        // Arrange
        var text = "hello hello world world hello";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(5);
        result.UniqueWordCount.Should().Be(2); // hello, world
    }

    [Fact]
    public void Analyze_WithAllUniqueWords_ShouldReturnSameAsWordCount()
    {
        // Arrange
        var text = "one two three four five";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(5);
        result.UniqueWordCount.Should().Be(5);
    }

    [Fact]
    public void Analyze_WithCaseDifferences_ShouldTreatAsSameWord()
    {
        // Arrange
        var text = "Hello HELLO hello";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(3);
        result.UniqueWordCount.Should().Be(1); // All are "hello"
    }

    #endregion

    #region Average Word Length Tests

    [Fact]
    public void Analyze_WithEmptyString_ShouldReturnZeroAverageWordLength()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.AverageWordLength.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithSingleWord_ShouldReturnWordLengthAsAverage()
    {
        // Arrange
        var text = "Hello"; // 5 characters

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.AverageWordLength.Should().Be(5.0);
    }

    [Fact]
    public void Analyze_WithMultipleWords_ShouldReturnCorrectAverage()
    {
        // Arrange
        var text = "one two"; // 3 + 3 = 6 / 2 = 3

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.AverageWordLength.Should().Be(3.0);
    }

    [Fact]
    public void Analyze_WithVaryingWordLengths_ShouldReturnCorrectAverage()
    {
        // Arrange
        var text = "a bb ccc"; // 1 + 2 + 3 = 6 / 3 = 2

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.AverageWordLength.Should().Be(2.0);
    }

    [Fact]
    public void Analyze_WithDecimalAverage_ShouldRoundToTwoDecimals()
    {
        // Arrange
        var text = "one two three"; // 3 + 3 + 5 = 11 / 3 = 3.67

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.AverageWordLength.Should().Be(3.67);
    }

    #endregion

    #region Line Count Tests

    [Fact]
    public void Analyze_WithEmptyString_ShouldReturnZeroLineCount()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.LineCount.Should().Be(0);
    }

    [Fact]
    public void Analyze_WithSingleLine_ShouldReturnLineCountOf1()
    {
        // Arrange
        var text = "This is a single line.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.LineCount.Should().Be(1);
    }

    [Fact]
    public void Analyze_WithTwoLines_ShouldReturnLineCountOf2()
    {
        // Arrange
        var text = "Line one.\nLine two.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.LineCount.Should().Be(2);
    }

    [Fact]
    public void Analyze_WithWindowsNewlines_ShouldCountLinesCorrectly()
    {
        // Arrange
        var text = "Line one.\r\nLine two.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.LineCount.Should().Be(2);
    }

    [Fact]
    public void Analyze_WithMultipleNewlines_ShouldCountAllLines()
    {
        // Arrange
        var text = "One\n\nTwo\n\n\nThree";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.LineCount.Should().Be(6); // Includes empty lines
    }

    #endregion

    #region Integration Tests - Full Text Analysis

    [Fact]
    public void Analyze_WithCompleteText_ShouldReturnAllStatisticsCorrectly()
    {
        // Arrange
        // Words: The, quick, brown, fox, jumps, over, the, lazy, dog, This, sentence, has, unique, words = 14 words
        var text = "The quick brown fox jumps over the lazy dog. This sentence has unique words!";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.WordCount.Should().Be(14);
        result.SentenceCount.Should().Be(2);
        result.ParagraphCount.Should().Be(1);
        result.CharacterCount.Should().Be(76);
        result.LineCount.Should().Be(1);
        result.UniqueWordCount.Should().BeGreaterThan(0);
        result.AverageWordLength.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Analyze_WithMultiParagraphDocument_ShouldReturnAllStatisticsCorrectly()
    {
        // Arrange
        var text = @"First paragraph with some text.

Second paragraph with more text.

Third paragraph concludes the document.";

        // Act
        var result = _sut.Analyze(text);

        // Assert
        result.ParagraphCount.Should().Be(3);
        result.SentenceCount.Should().Be(3);
        result.LineCount.Should().BeGreaterThan(3);
    }

    #endregion
}
