using FluentAssertions;
using Moq;
using WordCount.LiveEditor.Services;
using Xunit;

namespace WordCount.LiveEditor.Tests;

/// <summary>
/// Unit tests for the MultiFileAnalysisService class
/// Tests cover multi-file analysis, aggregate statistics calculation,
/// and individual file result generation
/// </summary>
public class MultiFileAnalysisServiceTests
{
    private readonly Mock<ITextStatisticsService> _textStatisticsServiceMock;
    private readonly MultiFileAnalysisService _sut;

    public MultiFileAnalysisServiceTests()
    {
        _textStatisticsServiceMock = new Mock<ITextStatisticsService>();
        _sut = new MultiFileAnalysisService(_textStatisticsServiceMock.Object);
    }

    #region Empty/Null Input Tests

    [Fact]
    public void AnalyzeMultipleTexts_WithEmptyDictionary_ShouldReturnEmptyResults()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>();

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.Should().NotBeNull();
        result.FileResults.Should().BeEmpty();
        result.AggregateStatistics.TotalFiles.Should().Be(0);
    }

    [Fact]
    public void AnalyzeMultipleTexts_WithEmptyDictionary_ShouldReturnZeroAggregateStats()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>();

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.AggregateStatistics.TotalWords.Should().Be(0);
        result.AggregateStatistics.TotalCharacters.Should().Be(0);
        result.AggregateStatistics.TotalParagraphs.Should().Be(0);
        result.AggregateStatistics.AverageWordLength.Should().Be(0);
    }

    #endregion

    #region Single File Tests

    [Fact]
    public void AnalyzeMultipleTexts_WithSingleFile_ShouldReturnSingleFileResult()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "document.txt", "Hello world" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Hello world"))
            .Returns(new TextStatistics
            {
                WordCount = 2,
                UniqueWordCount = 2,
                AverageWordLength = 5.0,
                CharacterCount = 11,
                ParagraphCount = 1
            });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.FileResults.Should().HaveCount(1);
        result.FileResults[0].FileName.Should().Be("document.txt");
        result.FileResults[0].WordCount.Should().Be(2);
    }

    [Fact]
    public void AnalyzeMultipleTexts_WithSingleFile_ShouldSetAggregateStatsFromSingleFile()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "document.txt", "Hello world test" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Hello world test"))
            .Returns(new TextStatistics
            {
                WordCount = 3,
                UniqueWordCount = 3,
                AverageWordLength = 4.67,
                CharacterCount = 16,
                ParagraphCount = 1
            });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.AggregateStatistics.TotalFiles.Should().Be(1);
        result.AggregateStatistics.TotalWords.Should().Be(3);
        result.AggregateStatistics.TotalCharacters.Should().Be(16);
        result.AggregateStatistics.TotalParagraphs.Should().Be(1);
    }

    #endregion

    #region Multiple Files Tests

    [Fact]
    public void AnalyzeMultipleTexts_WithMultipleFiles_ShouldReturnMultipleFileResults()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "file1.txt", "Hello world" },
            { "file2.txt", "Goodbye world" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Hello world"))
            .Returns(new TextStatistics { WordCount = 2, CharacterCount = 11, ParagraphCount = 1, AverageWordLength = 5.0, UniqueWordCount = 2 });

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Goodbye world"))
            .Returns(new TextStatistics { WordCount = 2, CharacterCount = 13, ParagraphCount = 1, AverageWordLength = 6.0, UniqueWordCount = 2 });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.FileResults.Should().HaveCount(2);
        result.FileResults.Should().Contain(f => f.FileName == "file1.txt");
        result.FileResults.Should().Contain(f => f.FileName == "file2.txt");
    }

    [Fact]
    public void AnalyzeMultipleTexts_WithMultipleFiles_ShouldAggregateWordCount()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "file1.txt", "Hello world" },
            { "file2.txt", "Goodbye world test" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Hello world"))
            .Returns(new TextStatistics { WordCount = 2, CharacterCount = 11, ParagraphCount = 1, AverageWordLength = 5.0 });

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Goodbye world test"))
            .Returns(new TextStatistics { WordCount = 3, CharacterCount = 18, ParagraphCount = 1, AverageWordLength = 5.33 });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.AggregateStatistics.TotalWords.Should().Be(5); // 2 + 3
        result.AggregateStatistics.TotalFiles.Should().Be(2);
    }

    [Fact]
    public void AnalyzeMultipleTexts_WithMultipleFiles_ShouldAggregateCharacterCount()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "file1.txt", "ABC" },
            { "file2.txt", "DEFGH" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("ABC"))
            .Returns(new TextStatistics { WordCount = 1, CharacterCount = 3, ParagraphCount = 1, AverageWordLength = 3.0 });

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("DEFGH"))
            .Returns(new TextStatistics { WordCount = 1, CharacterCount = 5, ParagraphCount = 1, AverageWordLength = 5.0 });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.AggregateStatistics.TotalCharacters.Should().Be(8); // 3 + 5
    }

    [Fact]
    public void AnalyzeMultipleTexts_WithMultipleFiles_ShouldAggregateParagraphCount()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "file1.txt", "Para 1\n\nPara 2" },
            { "file2.txt", "Single para" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Para 1\n\nPara 2"))
            .Returns(new TextStatistics { WordCount = 4, CharacterCount = 14, ParagraphCount = 2, AverageWordLength = 3.0 });

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Single para"))
            .Returns(new TextStatistics { WordCount = 2, CharacterCount = 11, ParagraphCount = 1, AverageWordLength = 4.5 });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.AggregateStatistics.TotalParagraphs.Should().Be(3); // 2 + 1
    }

    #endregion

    #region Weighted Average Word Length Tests

    [Fact]
    public void AnalyzeMultipleTexts_WithMultipleFiles_ShouldCalculateWeightedAverageWordLength()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "file1.txt", "content1" },
            { "file2.txt", "content2" }
        };

        // File 1: 10 words with avg length 3.0
        _textStatisticsServiceMock
            .Setup(m => m.Analyze("content1"))
            .Returns(new TextStatistics { WordCount = 10, AverageWordLength = 3.0, CharacterCount = 0, ParagraphCount = 0 });

        // File 2: 20 words with avg length 6.0
        _textStatisticsServiceMock
            .Setup(m => m.Analyze("content2"))
            .Returns(new TextStatistics { WordCount = 20, AverageWordLength = 6.0, CharacterCount = 0, ParagraphCount = 0 });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        // Weighted average: (10*3.0 + 20*6.0) / (10+20) = (30 + 120) / 30 = 5.0
        result.AggregateStatistics.AverageWordLength.Should().Be(5.0);
    }

    [Fact]
    public void AnalyzeMultipleTexts_WithZeroWordFiles_ShouldHandleAverageWordLengthCorrectly()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "empty.txt", "" },
            { "file.txt", "content" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze(""))
            .Returns(new TextStatistics { WordCount = 0, AverageWordLength = 0, CharacterCount = 0, ParagraphCount = 0 });

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("content"))
            .Returns(new TextStatistics { WordCount = 1, AverageWordLength = 7.0, CharacterCount = 7, ParagraphCount = 1 });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.AggregateStatistics.AverageWordLength.Should().Be(7.0); // Only the non-zero file contributes
    }

    #endregion

    #region File-Level Statistics Tests

    [Fact]
    public void AnalyzeMultipleTexts_ShouldPreserveIndividualFileStatistics()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "document.txt", "Test content" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("Test content"))
            .Returns(new TextStatistics
            {
                WordCount = 2,
                UniqueWordCount = 2,
                AverageWordLength = 6.0,
                CharacterCount = 12,
                ParagraphCount = 1
            });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        var fileResult = result.FileResults.Single();
        fileResult.WordCount.Should().Be(2);
        fileResult.UniqueWordCount.Should().Be(2);
        fileResult.AverageWordLength.Should().Be(6.0);
        fileResult.CharacterCount.Should().Be(12);
        fileResult.ParagraphCount.Should().Be(1);
    }

    #endregion

    #region Total Unique Words Tests

    [Fact]
    public void AnalyzeMultipleTexts_ShouldSumUniqueWordsFromAllFiles()
    {
        // Arrange
        var fileContents = new Dictionary<string, string>
        {
            { "file1.txt", "hello world" },
            { "file2.txt", "foo bar" }
        };

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("hello world"))
            .Returns(new TextStatistics { WordCount = 2, UniqueWordCount = 2, CharacterCount = 0, ParagraphCount = 0, AverageWordLength = 0 });

        _textStatisticsServiceMock
            .Setup(m => m.Analyze("foo bar"))
            .Returns(new TextStatistics { WordCount = 2, UniqueWordCount = 2, CharacterCount = 0, ParagraphCount = 0, AverageWordLength = 0 });

        // Act
        var result = _sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        // Note: Current implementation sums unique words, not the actual distinct count across files
        result.AggregateStatistics.TotalUniqueWords.Should().Be(4); // 2 + 2
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void AnalyzeMultipleTexts_WithRealTextStatisticsService_ShouldWorkCorrectly()
    {
        // Arrange
        var realService = new TextStatisticsService();
        var sut = new MultiFileAnalysisService(realService);

        var fileContents = new Dictionary<string, string>
        {
            { "doc1.txt", "Hello world" },
            { "doc2.txt", "This is a test sentence." },
            { "doc3.txt", "Another document with some words." }
        };

        // Act
        var result = sut.AnalyzeMultipleTexts(fileContents);

        // Assert
        result.AggregateStatistics.TotalFiles.Should().Be(3);
        result.AggregateStatistics.TotalWords.Should().BeGreaterThan(0);
        result.AggregateStatistics.TotalCharacters.Should().BeGreaterThan(0);
        result.FileResults.Should().HaveCount(3);
    }

    #endregion
}
