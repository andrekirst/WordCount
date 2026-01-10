using FluentAssertions;
using WordCount.LiveEditor.Services;
using Xunit;

namespace WordCount.LiveEditor.Tests;

/// <summary>
/// Unit tests for the TextStatistics model class
/// Tests cover default values and property behavior
/// </summary>
public class TextStatisticsModelTests
{
    [Fact]
    public void TextStatistics_NewInstance_ShouldHaveDefaultValues()
    {
        // Arrange & Act
        var stats = new TextStatistics();

        // Assert
        stats.WordCount.Should().Be(0);
        stats.CharacterCount.Should().Be(0);
        stats.CharacterCountNoSpaces.Should().Be(0);
        stats.SentenceCount.Should().Be(0);
        stats.ParagraphCount.Should().Be(0);
        stats.UniqueWordCount.Should().Be(0);
        stats.AverageWordLength.Should().Be(0.0);
        stats.LineCount.Should().Be(0);
    }

    [Fact]
    public void TextStatistics_WordCount_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.WordCount = 100;

        // Assert
        stats.WordCount.Should().Be(100);
    }

    [Fact]
    public void TextStatistics_CharacterCount_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.CharacterCount = 500;

        // Assert
        stats.CharacterCount.Should().Be(500);
    }

    [Fact]
    public void TextStatistics_CharacterCountNoSpaces_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.CharacterCountNoSpaces = 450;

        // Assert
        stats.CharacterCountNoSpaces.Should().Be(450);
    }

    [Fact]
    public void TextStatistics_SentenceCount_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.SentenceCount = 10;

        // Assert
        stats.SentenceCount.Should().Be(10);
    }

    [Fact]
    public void TextStatistics_ParagraphCount_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.ParagraphCount = 5;

        // Assert
        stats.ParagraphCount.Should().Be(5);
    }

    [Fact]
    public void TextStatistics_UniqueWordCount_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.UniqueWordCount = 75;

        // Assert
        stats.UniqueWordCount.Should().Be(75);
    }

    [Fact]
    public void TextStatistics_AverageWordLength_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.AverageWordLength = 5.75;

        // Assert
        stats.AverageWordLength.Should().Be(5.75);
    }

    [Fact]
    public void TextStatistics_LineCount_ShouldBeSettable()
    {
        // Arrange
        var stats = new TextStatistics();

        // Act
        stats.LineCount = 25;

        // Assert
        stats.LineCount.Should().Be(25);
    }

    [Fact]
    public void TextStatistics_AllProperties_ShouldBeIndependent()
    {
        // Arrange
        var stats = new TextStatistics
        {
            WordCount = 100,
            CharacterCount = 500,
            CharacterCountNoSpaces = 450,
            SentenceCount = 10,
            ParagraphCount = 5,
            UniqueWordCount = 75,
            AverageWordLength = 5.5,
            LineCount = 20
        };

        // Act - modify one property
        stats.WordCount = 200;

        // Assert - other properties should be unchanged
        stats.CharacterCount.Should().Be(500);
        stats.CharacterCountNoSpaces.Should().Be(450);
        stats.SentenceCount.Should().Be(10);
        stats.ParagraphCount.Should().Be(5);
        stats.UniqueWordCount.Should().Be(75);
        stats.AverageWordLength.Should().Be(5.5);
        stats.LineCount.Should().Be(20);
    }
}
