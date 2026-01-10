using System.Collections.Generic;
using System.Globalization;
using AutoFixture.Xunit2;
using Moq;
using WordCount.Implementations.Output;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests;

public class MultiFileAnalyzerOutputTests
{
    // Use the existing DecideLanguageResult from the application
    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Null_Result_Does_Nothing(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        MultiFileAnalyzerOutput sut)
    {
        sut.DisplayResult(null);

        displayOutput.Verify(v => v.WriteResourceLine(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Zero_Files_Does_Nothing(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        MultiFileAnalyzerOutput sut)
    {
        var result = new MultiFileAnalysisResult
        {
            TotalFilesAnalyzed = 0,
            FileResults = new List<FileAnalysisResult>()
        };

        sut.DisplayResult(result);

        displayOutput.Verify(v => v.WriteResourceLine("MULTI_FILE_DASHBOARD"), Times.Never);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_With_Files_Shows_Dashboard_Header(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = CreateSampleMultiFileResult();

        sut.DisplayResult(result);

        displayOutput.Verify(v => v.WriteResourceLine("MULTI_FILE_DASHBOARD"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Shows_Folder_Path(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = CreateSampleMultiFileResult();
        result.FolderPath = "/my/documents/folder";

        sut.DisplayResult(result);

        displayOutput.Verify(v => v.WriteResourceLine("FOLDER_ANALYZED", "/my/documents/folder"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Shows_Total_Files_Analyzed(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = CreateSampleMultiFileResult();
        result.TotalFilesAnalyzed = 5;

        sut.DisplayResult(result);

        displayOutput.Verify(v => v.WriteResourceLine("TOTAL_FILES_ANALYZED", 5), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Shows_Aggregate_Statistics_Header(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = CreateSampleMultiFileResult();

        sut.DisplayResult(result);

        displayOutput.Verify(v => v.WriteResourceLine("AGGREGATE_STATISTICS"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Shows_Individual_Results_Header(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = CreateSampleMultiFileResult();

        sut.DisplayResult(result);

        displayOutput.Verify(v => v.WriteResourceLine("INDIVIDUAL_FILE_RESULTS"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Shows_File_Names(
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = new MultiFileAnalysisResult
        {
            TotalFilesAnalyzed = 2,
            FolderPath = "/test",
            AggregateStatistics = new AggregateStatistics(),
            FileResults = new List<FileAnalysisResult>
            {
                new FileAnalysisResult
                {
                    FileName = "document1.txt",
                    FilePath = "/test/document1.txt",
                    AnalysisResult = new WordCountAnalyzerResult()
                },
                new FileAnalysisResult
                {
                    FileName = "document2.txt",
                    FilePath = "/test/document2.txt",
                    AnalysisResult = new WordCountAnalyzerResult()
                }
            }
        };

        sut.DisplayResult(result);

        displayOutput.Verify(v => v.WriteResourceLine("FILE_NAME", "document1.txt"), Times.Once);
        displayOutput.Verify(v => v.WriteResourceLine("FILE_NAME", "document2.txt"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Calls_LanguageResource_For_Labels(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = CreateSampleMultiFileResult();

        sut.DisplayResult(result);

        // Verify that DetectLongestResourceString is called for formatting
        languageResource.Verify(v => v.DetectLongestResourceString(It.IsAny<string[]>()), Times.AtLeastOnce);
    }

    [Theory, AutoMoqData]
    public void MultiFileAnalyzerOutputTests_DisplayResult_Gets_Resource_Strings_For_Statistics(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<ILanguageDecision> languageDecision,
        MultiFileAnalyzerOutput sut)
    {
        SetupLanguageMocks(languageResource, languageDecision);

        var result = CreateSampleMultiFileResult();

        sut.DisplayResult(result);

        // Verify that GetResourceStringById is called for various statistics
        languageResource.Verify(v => v.GetResourceStringById("TOTAL_WORDS"), Times.AtLeastOnce);
        languageResource.Verify(v => v.GetResourceStringById("TOTAL_UNIQUE_WORDS"), Times.AtLeastOnce);
        languageResource.Verify(v => v.GetResourceStringById("CHARACTERS"), Times.AtLeastOnce);
    }

    private static void SetupLanguageMocks(Mock<ILanguageResource> languageResource, Mock<ILanguageDecision> languageDecision)
    {
        languageDecision
            .Setup(m => m.DecideLanguage())
            .Returns(new DecideLanguageResult { Culture = CultureInfo.InvariantCulture });

        languageResource
            .Setup(m => m.DetectLongestResourceString(It.IsAny<string[]>()))
            .Returns(30);

        languageResource
            .Setup(m => m.GetResourceStringById(It.IsAny<string>()))
            .Returns((string id) => id);
    }

    private static MultiFileAnalysisResult CreateSampleMultiFileResult()
    {
        return new MultiFileAnalysisResult
        {
            TotalFilesAnalyzed = 1,
            FolderPath = "/test/folder",
            AggregateStatistics = new AggregateStatistics
            {
                TotalWords = 100,
                TotalUniqueWords = 50,
                AverageWordLength = 5.5,
                TotalChapters = 3,
                TotalFileSizeBytes = 1024,
                AllDistinctWords = new List<string> { "test", "words" }
            },
            FileResults = new List<FileAnalysisResult>
            {
                new FileAnalysisResult
                {
                    FileName = "test.txt",
                    FilePath = "/test/folder/test.txt",
                    FileSizeBytes = 1024,
                    AnalysisResult = new WordCountAnalyzerResult
                    {
                        NumberOfWords = 100,
                        NumberOfUniqueWords = 50,
                        AverageWordLength = 5.5,
                        NumberOfChapters = 3
                    }
                }
            }
        };
    }
}
