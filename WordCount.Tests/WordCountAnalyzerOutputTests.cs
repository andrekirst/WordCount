using AutoFixture.Xunit2;
using Moq;
using WordCount.Implementations.Output;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;
using Xunit;

namespace WordCount.Tests;

public class WordCountAnalyzerOutputTests
{
    [Theory, AutoMoqData]
    public void WordCountAnalyzerOutputTests_DisplayResult_Result_NumberOfWords_2_Expect_Number_of_Words_2_Number(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<IStatisticsOutput> statisticsOutput,
        WordCountAnalyzerOutput sut)
    {
        languageResource
            .Setup(m => m.DetectLongestResourceString(It.IsAny<string[]>()))
            .Returns(20);

        sut.DisplayResult(new WordCountAnalyzerResult
        {
            NumberOfWords = 2,
            NumberOfUniqueWords = 1,
            AverageWordLength = 5.63,
            NumberOfChapters = 3
        });

        displayOutput.Verify(v => v.WriteResourceLine("STATISTICS"), Times.Once);
        statisticsOutput.Verify(v => v.WriteNumberOfWords(2, 20), Times.Once);
        statisticsOutput.Verify(v => v.WriteNumberOfUniqeWords(1, 20), Times.Once);
        statisticsOutput.Verify(v => v.WriteAverageWordLength(5.63, 20), Times.Once);
        statisticsOutput.Verify(v => v.WriteNumberOfChapters(3, 20), Times.Once);
    }
}