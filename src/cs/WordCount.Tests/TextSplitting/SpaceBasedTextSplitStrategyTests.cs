using FluentAssertions;
using WordCount.Implementations.TextSplitting;
using WordCount.Interfaces;
using Xunit;

namespace WordCount.Tests.TextSplitting;

/// <summary>
/// Unit tests for the SpaceBasedTextSplitStrategy class that handles
/// text splitting for space-separated languages like English, German, French, etc.
/// </summary>
public class SpaceBasedTextSplitStrategyTests
{
    private readonly ITextSplitStrategy _sut;

    public SpaceBasedTextSplitStrategyTests()
    {
        _sut = new SpaceBasedTextSplitStrategy();
    }

    #region Basic English Text Splitting

    [Fact]
    public void SplitText_SimpleEnglishSentence_ShouldReturnCorrectWords()
    {
        // Arrange
        const string text = "Hello world this is a test";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(6);
        result.Should().ContainInOrder("Hello", "world", "this", "is", "a", "test");
    }

    [Fact]
    public void SplitText_EnglishWithPunctuation_ShouldRemovePunctuation()
    {
        // Arrange
        const string text = "Hello, world! How are you?";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(5);
        result.Should().ContainInOrder("Hello", "world", "How", "are", "you");
    }

    [Fact]
    public void SplitText_EnglishWithPeriod_ShouldRemovePeriod()
    {
        // Arrange
        const string text = "This is a sentence.";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().NotContain("sentence.");
        result.Should().Contain("sentence");
    }

    #endregion

    #region German Text Splitting

    [Fact]
    public void SplitText_GermanTextWithUmlauts_ShouldPreserveUmlauts()
    {
        // Arrange
        const string text = "Draußen ist es schön";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().ContainInOrder("Draußen", "ist", "es", "schön");
    }

    [Fact]
    public void SplitText_GermanTextWithEszett_ShouldPreserveEszett()
    {
        // Arrange
        const string text = "Die Straße ist lang";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().Contain("Straße");
    }

    [Fact]
    public void SplitText_GermanCompoundWords_ShouldPreserveCompoundWords()
    {
        // Arrange - German compound words should remain intact
        const string text = "Donaudampfschifffahrtsgesellschaft";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(1);
        result.Should().Contain("Donaudampfschifffahrtsgesellschaft");
    }

    #endregion

    #region Special Character Handling

    [Fact]
    public void SplitText_HyphenatedWord_ShouldKeepAsOneWord()
    {
        // Arrange
        const string text = "This is a pre-condition";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("pre-condition");
    }

    [Fact]
    public void SplitText_TrailingHyphen_ShouldRemoveTrailingHyphen()
    {
        // Arrange - Like a line break in the middle of a word
        const string text = "pre- condition";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("pre");
        result.Should().Contain("condition");
        result.Should().NotContain("pre-");
    }

    [Fact]
    public void SplitText_WordWithApostrophe_ShouldKeepAsOneWord()
    {
        // Arrange
        const string text = "Hamlet's sword";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("Hamlet's");
        result.Should().Contain("sword");
    }

    [Fact]
    public void SplitText_Contractions_ShouldKeepAsOneWord()
    {
        // Arrange
        const string text = "I'm don't won't";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().Contain("I'm");
        result.Should().Contain("don't");
        result.Should().Contain("won't");
    }

    #endregion

    #region Number Handling

    [Fact]
    public void SplitText_TextWithNumbers_ShouldExcludeNumbers()
    {
        // Arrange
        const string text = "There are 123 items in the 456 list";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().NotContain("123");
        result.Should().NotContain("456");
        result.Should().Contain("There");
        result.Should().Contain("are");
        result.Should().Contain("items");
    }

    [Fact]
    public void SplitText_NumbersOnly_ShouldReturnEmptyList()
    {
        // Arrange
        const string text = "12345 67890";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().BeEmpty();
    }

    #endregion

    #region Whitespace Handling

    [Fact]
    public void SplitText_MultipleSpaces_ShouldHandleCorrectly()
    {
        // Arrange
        const string text = "Hello    world";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainInOrder("Hello", "world");
    }

    [Fact]
    public void SplitText_TabsAndNewlines_ShouldHandleCorrectly()
    {
        // Arrange
        const string text = "Hello\tworld\nnew line";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(4);
        result.Should().ContainInOrder("Hello", "world", "new", "line");
    }

    [Fact]
    public void SplitText_LeadingAndTrailingWhitespace_ShouldHandleCorrectly()
    {
        // Arrange
        const string text = "   Hello world   ";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainInOrder("Hello", "world");
    }

    #endregion

    #region Edge Cases

    [Theory]
    [InlineData(null)]
    [InlineData("")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public void SplitText_NullOrEmptyText_ShouldReturnEmptyList(string? text)
#pragma warning restore CS8632
    {
        // Act
        var result = _sut.SplitText(text!);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SplitText_WhitespaceOnly_ShouldReturnEmptyList()
    {
        // Arrange
        const string text = "   \t\n   ";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SplitText_SingleWord_ShouldReturnSingleWord()
    {
        // Arrange
        const string text = "Hello";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(1);
        result.Should().Contain("Hello");
    }

    [Fact]
    public void SplitText_SingleCharacter_ShouldReturnSingleWord()
    {
        // Arrange
        const string text = "a";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(1);
        result.Should().Contain("a");
    }

    [Fact]
    public void SplitText_PunctuationOnly_ShouldReturnEmptyList()
    {
        // Arrange
        const string text = "!@#$%^&*()";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().BeEmpty();
    }

    #endregion

    #region Unicode Text (Non-CJK)

    [Fact]
    public void SplitText_FrenchAccents_ShouldPreserveAccents()
    {
        // Arrange
        const string text = "café résumé naïve";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain("café");
        result.Should().Contain("résumé");
        result.Should().Contain("naïve");
    }

    [Fact]
    public void SplitText_SpanishAccents_ShouldPreserveAccents()
    {
        // Arrange
        const string text = "niño señor español";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain("niño");
        result.Should().Contain("señor");
        result.Should().Contain("español");
    }

    [Fact]
    public void SplitText_RussianCyrillic_ShouldHandleCyrillic()
    {
        // Arrange - Russian text (Cyrillic script)
        const string text = "Привет мир";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain("Привет");
        result.Should().Contain("мир");
    }

    #endregion

    #region Complex Sentences

    [Fact]
    public void SplitText_LongComplexSentence_ShouldHandleCorrectly()
    {
        // Arrange
        const string text = "Longworth, however, in desperate need of money, killed the preacher with Hamlet's sword and stole the manuscript";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        var expected = new[] { "Longworth", "however", "in", "desperate", "need", "of", "money", "killed", "the", "preacher", "with", "Hamlet's", "sword", "and", "stole", "the", "manuscript" };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void SplitText_HumptyDumpty_ShouldHandle10Words()
    {
        // Arrange
        const string text = "Humpty-Dumpty sat on a wall. Humpty-Dumpty had a great fall.";

        // Act
        var result = _sut.SplitText(text);

        // Assert
        result.Should().HaveCount(10);
        result.Should().Contain("Humpty-Dumpty");
    }

    #endregion
}
