﻿using System.Globalization;
using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;
using WordCount.Tests.XUnitHelpers;

namespace WordCount.Tests
{
    public class WordCountAnalyzerOutputTests
    {
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<ILanguageDecision> _mockLanguageDecision;
        private readonly WordCountAnalyzerOutput _systemUnderTest;

        public WordCountAnalyzerOutputTests()
        {
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockLanguageDecision = new Mock<ILanguageDecision>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockLanguageDecision.Object)
                .As<ILanguageDecision>();

            containerBuilder
                .RegisterType<WordCountAnalyzerOutput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<WordCountAnalyzerOutput>();
        }

        [NamedFact]
        public void WordCountAnalyzerOutputTests_DisplayResult_Result_NumberOfWords_2_Expect_Number_of_Words_2_Number()
        {
            _mockLanguageDecision
                .Setup(m => m.DecideLanguage())
                .Returns(new DecideLanguageResult { Culture = new CultureInfo("de-DE") });

            _systemUnderTest.DisplayResult(wordCountAnalyzerResult: new WordCountAnalyzerResult
            {
                NumberOfWords = 2,
                NumberOfUniqueWords = 1,
                AverageWordLength = 5.63,
                NumberOfChapters = 2
            });
            _mockDisplayOutput
                .Verify(v => v.WriteResourceLine("NUMBER_OF_WORDS", 2), Times.Once);

            _mockDisplayOutput
                .Verify(v => v.WriteResourceLine("AVERAGE_WORD_LENGTH", "5,63"), Times.Once);
        }
    }
}
