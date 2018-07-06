using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models.Results;
using WordCount.Tests.XUnitHelpers;

namespace WordCount.Tests
{
    public class WordCountAnalyzerOutputTests
    {
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly WordCountAnalyzerOutput _systemUnderTest;

        public WordCountAnalyzerOutputTests()
        {
            _mockDisplayOutput = new Mock<IDisplayOutput>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterType<WordCountAnalyzerOutput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<WordCountAnalyzerOutput>();
        }

        [NamedFact]
        public void WordCountAnalyzerOutputTests_DisplayResult_Result_NumberOfWords_2_Expect_Number_of_Words_2_Number_of_unique_Words_1()
        {
            _systemUnderTest.DisplayResult(wordCountAnalyzerResult: new WordCountAnalyzerResult
            {
                NumberOfWords = 2,
                NumberOfUniqueWords = 1,
                AverageWordLength = 5.63,
                NumberOfChapters = 2
            });
            const string expected = "Number of words: 2, unique: 1; average word length: 5.63 characters; chapters: 2";

            _mockDisplayOutput
                .Verify(
                    expression: v => v.WriteLine(expected),
                    times: Times.Once);
        }
    }
}
