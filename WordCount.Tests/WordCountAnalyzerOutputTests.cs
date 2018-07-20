using Autofac;
using Moq;
using WordCount.Implementations.Output;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;
using WordCount.Tests.XUnitHelpers;

namespace WordCount.Tests
{
    public class WordCountAnalyzerOutputTests
    {
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<IStatisticsOutput> _mockStatisticsOutput;
        private readonly Mock<ILanguageResource> _mockLanguageResource;
        private readonly WordCountAnalyzerOutput _systemUnderTest;

        public WordCountAnalyzerOutputTests()
        {
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockLanguageResource = new Mock<ILanguageResource>();
            _mockStatisticsOutput = new Mock<IStatisticsOutput>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockLanguageResource.Object)
                .As<ILanguageResource>();

            containerBuilder
                .RegisterInstance(instance: _mockStatisticsOutput.Object)
                .As<IStatisticsOutput>();

            containerBuilder
                .RegisterType<WordCountAnalyzerOutput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<WordCountAnalyzerOutput>();
        }

        [NamedFact]
        public void WordCountAnalyzerOutputTests_DisplayResult_Result_NumberOfWords_2_Expect_Number_of_Words_2_Number()
        {
            _mockLanguageResource
                .Setup(m => m.DetectLongestResourceString(It.IsAny<string[]>()))
                .Returns(20);

            _systemUnderTest.DisplayResult(result: new WordCountAnalyzerResult
            {
                NumberOfWords = 2,
                NumberOfUniqueWords = 1,
                AverageWordLength = 5.63,
                NumberOfChapters = 3
            });

            _mockDisplayOutput
                .Verify(v => v.WriteResourceLine("STATISTICS"), Times.Once);

            _mockStatisticsOutput
                .Verify(v => v.WriteNumberOfWords(2, 20), Times.Once);
            _mockStatisticsOutput
                .Verify(v => v.WriteNumberOfUniqeWords(1, 20), Times.Once);
            _mockStatisticsOutput
                .Verify(v => v.WriteAverageWordLength(5.63, 20), Times.Once);
            _mockStatisticsOutput
                .Verify(v => v.WriteNumberOfChapters(3, 20), Times.Once);
        }
    }
}
