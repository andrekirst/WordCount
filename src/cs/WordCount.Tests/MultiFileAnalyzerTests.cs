using System;
using System.IO;
using System.IO.Abstractions;
using Moq;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests;

public class MultiFileAnalyzerTests
{
    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_Folder_Not_Exists_Expect_Empty_Result(
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        MultiFileAnalyzer sut)
    {
        fileSystem
            .Setup(m => m.Directory.Exists(It.IsAny<string>()))
            .Returns(false);

        var actual = sut.AnalyzeFolder("/nonexistent/folder", "*.txt");

        actual.Should().NotBeNull();
        actual.TotalFilesAnalyzed.Should().Be(0);
        actual.FileResults.Should().BeEmpty();

        displayOutput.Verify(v => v.WriteErrorResourceLine("FOLDER_NOT_FOUND", "/nonexistent/folder"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_Folder_Exists_But_No_Files_Expect_Empty_Result(
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        MultiFileAnalyzer sut)
    {
        fileSystem
            .Setup(m => m.Directory.Exists(It.IsAny<string>()))
            .Returns(true);

        fileSystem
            .Setup(m => m.Directory.GetFiles(It.IsAny<string>(), It.IsAny<string>(), SearchOption.AllDirectories))
            .Returns(Array.Empty<string>());

        var actual = sut.AnalyzeFolder("/empty/folder", "*.txt");

        actual.Should().NotBeNull();
        actual.TotalFilesAnalyzed.Should().Be(0);
        actual.FileResults.Should().BeEmpty();

        displayOutput.Verify(v => v.WriteErrorResourceLine("NO_FILES_FOUND", "*.txt", "/empty/folder"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_Single_File_Found_Expect_Single_FileResult(
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IWordCountAnalyzer> wordCountAnalyzer,
        MultiFileAnalyzer sut)
    {
        var folderPath = "/test/folder";
        var filePath = "/test/folder/file1.txt";
        var fileContent = "Hello world test";

        fileSystem
            .Setup(m => m.Directory.Exists(folderPath))
            .Returns(true);

        fileSystem
            .Setup(m => m.Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories))
            .Returns(new[] { filePath });

        var mockFileInfo = new Mock<IFileInfo>();
        mockFileInfo.Setup(m => m.Length).Returns(16);

        fileSystem
            .Setup(m => m.FileInfo.FromFileName(filePath))
            .Returns(mockFileInfo.Object);

        fileSystem
            .Setup(m => m.File.ReadAllText(filePath))
            .Returns(fileContent);

        fileSystem
            .Setup(m => m.Path.GetFileName(filePath))
            .Returns("file1.txt");

        var analyzerResult = new WordCountAnalyzerResult
        {
            NumberOfWords = 3,
            NumberOfUniqueWords = 3,
            AverageWordLength = 4.33,
            NumberOfChapters = 1,
            DistinctWords = new System.Collections.Generic.List<string> { "Hello", "world", "test" }
        };

        wordCountAnalyzer
            .Setup(m => m.Analyze(fileContent))
            .Returns(analyzerResult);

        var actual = sut.AnalyzeFolder(folderPath, "*.txt");

        actual.Should().NotBeNull();
        actual.TotalFilesAnalyzed.Should().Be(1);
        actual.FileResults.Should().HaveCount(1);
        actual.FileResults[0].FileName.Should().Be("file1.txt");
        actual.FileResults[0].AnalysisResult.NumberOfWords.Should().Be(3);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_Multiple_Files_Found_Expect_Aggregated_Statistics(
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IWordCountAnalyzer> wordCountAnalyzer,
        MultiFileAnalyzer sut)
    {
        var folderPath = "/test/folder";
        var filePath1 = "/test/folder/file1.txt";
        var filePath2 = "/test/folder/file2.txt";
        var fileContent1 = "Hello world";
        var fileContent2 = "Goodbye world test";

        fileSystem
            .Setup(m => m.Directory.Exists(folderPath))
            .Returns(true);

        fileSystem
            .Setup(m => m.Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories))
            .Returns(new[] { filePath1, filePath2 });

        var mockFileInfo1 = new Mock<IFileInfo>();
        mockFileInfo1.Setup(m => m.Length).Returns(11);

        var mockFileInfo2 = new Mock<IFileInfo>();
        mockFileInfo2.Setup(m => m.Length).Returns(18);

        fileSystem
            .Setup(m => m.FileInfo.FromFileName(filePath1))
            .Returns(mockFileInfo1.Object);

        fileSystem
            .Setup(m => m.FileInfo.FromFileName(filePath2))
            .Returns(mockFileInfo2.Object);

        fileSystem
            .Setup(m => m.File.ReadAllText(filePath1))
            .Returns(fileContent1);

        fileSystem
            .Setup(m => m.File.ReadAllText(filePath2))
            .Returns(fileContent2);

        fileSystem
            .Setup(m => m.Path.GetFileName(filePath1))
            .Returns("file1.txt");

        fileSystem
            .Setup(m => m.Path.GetFileName(filePath2))
            .Returns("file2.txt");

        var analyzerResult1 = new WordCountAnalyzerResult
        {
            NumberOfWords = 2,
            NumberOfUniqueWords = 2,
            AverageWordLength = 5.0,
            NumberOfChapters = 1,
            DistinctWords = new System.Collections.Generic.List<string> { "Hello", "world" }
        };

        var analyzerResult2 = new WordCountAnalyzerResult
        {
            NumberOfWords = 3,
            NumberOfUniqueWords = 3,
            AverageWordLength = 5.0,
            NumberOfChapters = 1,
            DistinctWords = new System.Collections.Generic.List<string> { "Goodbye", "world", "test" }
        };

        wordCountAnalyzer
            .Setup(m => m.Analyze(fileContent1))
            .Returns(analyzerResult1);

        wordCountAnalyzer
            .Setup(m => m.Analyze(fileContent2))
            .Returns(analyzerResult2);

        var actual = sut.AnalyzeFolder(folderPath, "*.txt");

        actual.Should().NotBeNull();
        actual.TotalFilesAnalyzed.Should().Be(2);
        actual.FileResults.Should().HaveCount(2);

        // Verify aggregate statistics
        actual.AggregateStatistics.TotalWords.Should().Be(5); // 2 + 3
        actual.AggregateStatistics.TotalChapters.Should().Be(2); // 1 + 1
        actual.AggregateStatistics.TotalFileSizeBytes.Should().Be(29); // 11 + 18
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_Aggregate_UniqueWords_Are_Combined_And_Sorted(
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IWordCountAnalyzer> wordCountAnalyzer,
        MultiFileAnalyzer sut)
    {
        var folderPath = "/test/folder";
        var filePath1 = "/test/folder/file1.txt";
        var filePath2 = "/test/folder/file2.txt";

        fileSystem
            .Setup(m => m.Directory.Exists(folderPath))
            .Returns(true);

        fileSystem
            .Setup(m => m.Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories))
            .Returns(new[] { filePath1, filePath2 });

        var mockFileInfo1 = new Mock<IFileInfo>();
        mockFileInfo1.Setup(m => m.Length).Returns(10);
        var mockFileInfo2 = new Mock<IFileInfo>();
        mockFileInfo2.Setup(m => m.Length).Returns(10);

        fileSystem.Setup(m => m.FileInfo.FromFileName(filePath1)).Returns(mockFileInfo1.Object);
        fileSystem.Setup(m => m.FileInfo.FromFileName(filePath2)).Returns(mockFileInfo2.Object);
        fileSystem.Setup(m => m.File.ReadAllText(It.IsAny<string>())).Returns("content");
        fileSystem.Setup(m => m.Path.GetFileName(filePath1)).Returns("file1.txt");
        fileSystem.Setup(m => m.Path.GetFileName(filePath2)).Returns("file2.txt");

        var analyzerResult1 = new WordCountAnalyzerResult
        {
            NumberOfWords = 2,
            DistinctWords = new System.Collections.Generic.List<string> { "apple", "world" }
        };

        var analyzerResult2 = new WordCountAnalyzerResult
        {
            NumberOfWords = 2,
            DistinctWords = new System.Collections.Generic.List<string> { "banana", "world" }
        };

        wordCountAnalyzer.SetupSequence(m => m.Analyze(It.IsAny<string>()))
            .Returns(analyzerResult1)
            .Returns(analyzerResult2);

        var actual = sut.AnalyzeFolder(folderPath, "*.txt");

        // "world" appears in both files, so total unique should be 3: apple, banana, world
        actual.AggregateStatistics.TotalUniqueWords.Should().Be(3);
        actual.AggregateStatistics.AllDistinctWords.Should().BeInAscendingOrder();
        actual.AggregateStatistics.AllDistinctWords.Should().Contain("apple");
        actual.AggregateStatistics.AllDistinctWords.Should().Contain("banana");
        actual.AggregateStatistics.AllDistinctWords.Should().Contain("world");
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_FolderPath_Is_Set_In_Result(
        [Frozen] Mock<IFileSystem> fileSystem,
        MultiFileAnalyzer sut)
    {
        var folderPath = "/my/documents";

        fileSystem
            .Setup(m => m.Directory.Exists(folderPath))
            .Returns(false);

        var actual = sut.AnalyzeFolder(folderPath, "*.txt");

        actual.FolderPath.Should().Be(folderPath);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_File_Read_Error_Handles_Gracefully(
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<IWordCountAnalyzer> wordCountAnalyzer,
        MultiFileAnalyzer sut)
    {
        var folderPath = "/test/folder";
        var filePath1 = "/test/folder/file1.txt";
        var filePath2 = "/test/folder/file2.txt";

        fileSystem
            .Setup(m => m.Directory.Exists(folderPath))
            .Returns(true);

        fileSystem
            .Setup(m => m.Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories))
            .Returns(new[] { filePath1, filePath2 });

        // First file throws an exception
        fileSystem
            .Setup(m => m.FileInfo.FromFileName(filePath1))
            .Throws(new IOException("Permission denied"));

        // Second file works fine
        var mockFileInfo2 = new Mock<IFileInfo>();
        mockFileInfo2.Setup(m => m.Length).Returns(10);

        fileSystem
            .Setup(m => m.FileInfo.FromFileName(filePath2))
            .Returns(mockFileInfo2.Object);

        fileSystem
            .Setup(m => m.File.ReadAllText(filePath2))
            .Returns("content");

        fileSystem
            .Setup(m => m.Path.GetFileName(filePath2))
            .Returns("file2.txt");

        var analyzerResult = new WordCountAnalyzerResult
        {
            NumberOfWords = 1,
            DistinctWords = new System.Collections.Generic.List<string> { "content" }
        };

        wordCountAnalyzer
            .Setup(m => m.Analyze("content"))
            .Returns(analyzerResult);

        var actual = sut.AnalyzeFolder(folderPath, "*.txt");

        // Only the second file should be analyzed
        actual.TotalFilesAnalyzed.Should().Be(1);

        // Error should be reported for the first file
        displayOutput.Verify(v => v.WriteErrorResourceLine("FILE_READ_ERROR", filePath1, "Permission denied"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerTests_Average_Word_Length_Is_Weighted_Correctly(
        [Frozen] Mock<IFileSystem> fileSystem,
        [Frozen] Mock<IWordCountAnalyzer> wordCountAnalyzer,
        MultiFileAnalyzer sut)
    {
        var folderPath = "/test/folder";
        var filePath1 = "/test/folder/file1.txt";
        var filePath2 = "/test/folder/file2.txt";

        fileSystem
            .Setup(m => m.Directory.Exists(folderPath))
            .Returns(true);

        fileSystem
            .Setup(m => m.Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories))
            .Returns(new[] { filePath1, filePath2 });

        var mockFileInfo1 = new Mock<IFileInfo>();
        mockFileInfo1.Setup(m => m.Length).Returns(10);
        var mockFileInfo2 = new Mock<IFileInfo>();
        mockFileInfo2.Setup(m => m.Length).Returns(10);

        fileSystem.Setup(m => m.FileInfo.FromFileName(filePath1)).Returns(mockFileInfo1.Object);
        fileSystem.Setup(m => m.FileInfo.FromFileName(filePath2)).Returns(mockFileInfo2.Object);
        fileSystem.Setup(m => m.File.ReadAllText(It.IsAny<string>())).Returns("content");
        fileSystem.Setup(m => m.Path.GetFileName(filePath1)).Returns("file1.txt");
        fileSystem.Setup(m => m.Path.GetFileName(filePath2)).Returns("file2.txt");

        // File 1: 10 words with average length 3.0
        var analyzerResult1 = new WordCountAnalyzerResult
        {
            NumberOfWords = 10,
            AverageWordLength = 3.0,
            DistinctWords = new System.Collections.Generic.List<string>()
        };

        // File 2: 20 words with average length 6.0
        var analyzerResult2 = new WordCountAnalyzerResult
        {
            NumberOfWords = 20,
            AverageWordLength = 6.0,
            DistinctWords = new System.Collections.Generic.List<string>()
        };

        wordCountAnalyzer.SetupSequence(m => m.Analyze(It.IsAny<string>()))
            .Returns(analyzerResult1)
            .Returns(analyzerResult2);

        var actual = sut.AnalyzeFolder(folderPath, "*.txt");

        // Weighted average: (10*3.0 + 20*6.0) / (10+20) = (30 + 120) / 30 = 5.0
        actual.AggregateStatistics.AverageWordLength.Should().Be(5.0);
    }
}
