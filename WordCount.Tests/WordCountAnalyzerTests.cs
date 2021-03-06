﻿using System;
using Moq;
using System.Collections.Generic;
using Autofac;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models.Results;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class WordCountAnalyzerTests
    {
        private readonly Mock<ITextSplit> _mockTextSplit;
        private readonly Mock<IStopwordRemover> _mockStopwordRemover;

        private readonly WordCountAnalyzer _systemUnderTest;

        public WordCountAnalyzerTests()
        {
            _mockTextSplit = new Mock<ITextSplit>();
            _mockStopwordRemover = new Mock<IStopwordRemover>();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(instance: _mockTextSplit.Object)
                .As<ITextSplit>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(_mockStopwordRemover.Object)
                .As<IStopwordRemover>()
                .SingleInstance();

            containerBuilder
                .RegisterType<WordCountAnalyzer>()
                .SingleInstance();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<WordCountAnalyzer>();
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Analyze_Text_Bla_bla_Expect_2_Words()
        {
            string text = "Bla bla";
            List<string> mockTextSplitResulValues = new List<string> { "Bla", "bla" };

            _mockStopwordRemover
                .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
                .Returns(value: new StopwordRemoverResult { Words = mockTextSplitResulValues });

            _mockTextSplit
                .Setup(m => m.Split(text))
                .Returns(new TextSplitResult(mockTextSplitResulValues));

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: text);

            Assert.Equal(expected: 2, actual: actual.NumberOfWords);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Analyze_Stopword_a_This_is_a_Text_Expect_3_Words()
        {
            const string text = "This is a Text";

            List<string> values = new List<string> { "This", "is", "a", "Text" };

            _mockTextSplit
                .Setup(m => m.Split(text))
                .Returns(new TextSplitResult(values));

            _mockStopwordRemover
                .Setup(m => m.RemoveStopwords(values))
                .Returns(new StopwordRemoverResult
                {
                    Words = new List<string> { "This", "is", "Text" }
                });

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: text);

            Assert.Equal(expected: 3, actual: actual.NumberOfWords);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WordCountAnalyzerTests_Analyze_empty_strings_Expect_0_Words(
            string inputtext)
        {
            _mockTextSplit
                .Setup(m => m.Split(inputtext))
                .Returns(new TextSplitResult(new List<string>()));

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: inputtext);

            Assert.Equal(expected: 0, actual: actual.NumberOfWords);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Long_text_Bla_bla_bla_Expect_Number_of_Words_3()
        {
            const string text = "Bla bla bla";
            List<string> mockValues = new List<string> { "Bla", "bla", "bla" };
            List<string> mockValuesStopwordsRemoved = new List<string> { "Bla", "bla", "bla" };

            _mockTextSplit
                .Setup(m => m.Split(text))
                .Returns(value: new TextSplitResult(mockValues));

            _mockStopwordRemover
                .Setup(m => m.RemoveStopwords(mockValues))
                .Returns(new StopwordRemoverResult { Words = mockValuesStopwordsRemoved });

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: text);

            Assert.Equal(expected: 3, actual: actual.NumberOfWords);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Long_text_Bla_bla_bla_Expect_Number_of_unique_Words_2()
        {
            const string text = "Bla bla bla";

            List<string> mockValues = new List<string> { "Bla", "bla", "bla" };

            _mockTextSplit
                .Setup(expression: m => m.Split(text))
                .Returns(value: new TextSplitResult(mockValues));

            _mockStopwordRemover
                .Setup(expression: m => m.RemoveStopwords(mockValues))
                .Returns(value: new StopwordRemoverResult { Words = mockValues });

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: text);

            Assert.Equal(expected: 2, actual: actual.NumberOfUniqueWords);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Text_Bla_bla_bla_Expect_Average_word_Length_3()
        {
            string text = "Bla bla bla";

            List<string> mockValues = new List<string> { "Bla", "bla", "bla" };

            _mockTextSplit
                .Setup(m => m.Split(text))
                .Returns(value: new TextSplitResult(mockValues));

            _mockStopwordRemover
                .Setup(expression: m => m.RemoveStopwords(mockValues))
                .Returns(value: new StopwordRemoverResult { Words = mockValues });

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: text);

            Assert.Equal(expected: 3.0, actual: actual.AverageWordLength);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Text_Bla_bla_bla_Expect_distinct_Words_Bla_bla()
        {
            const string text = "Bla bla bla";

            List<string> mockValues = new List<string> { "Bla", "bla", "bla" };

            _mockTextSplit
                .Setup(expression: m => m.Split(text))
                .Returns(value: new TextSplitResult(mockValues));

            _mockStopwordRemover
                .Setup(expression: m => m.RemoveStopwords(mockValues))
                .Returns(value: new StopwordRemoverResult { Words = mockValues });

            List<string> expected = new List<string> { "Bla", "bla" };

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: text);

            Assert.Equal(expected: expected, actual: actual.DistinctWords);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Text_a_Stopword_a_Expect_All_Values_in_Result_0()
        {
            _mockStopwordRemover
                .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
                .Returns(value: new StopwordRemoverResult());

            _mockTextSplit
                .Setup(m => m.Split("a."))
                .Returns(value: new TextSplitResult(new List<string> { "a" }));

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze("a.");

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: 0.0, actual: actual.AverageWordLength);
            Assert.Equal(expected: 0, actual: actual.NumberOfUniqueWords);
            Assert.Equal(expected: 0, actual: actual.NumberOfWords);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Text_with_2_Linebreaks_Expect_2_chapters()
        {
            _mockTextSplit
                .Setup(m => m.Split(It.IsAny<string>()))
                .Returns(new TextSplitResult(new List<string>() {"asd"}));

            _mockStopwordRemover
                .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
                .Returns(new StopwordRemoverResult() {Words = new List<string>() {"asd"}});

            string inputText =
                $"Das ist das erste Kapitel.{Environment.NewLine}{Environment.NewLine}Das ist das zweite Kapitel.";

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: inputText);

            Assert.Equal(
                expected: 2,
                actual: actual.NumberOfChapters);
        }

        [NamedFact]
        public void WordCountAnalyzerTests_Text_with_1_Linebreak_Expect_1_chapters()
        {
            _mockTextSplit
                .Setup(m => m.Split(It.IsAny<string>()))
                .Returns(new TextSplitResult(new List<string>() { "asd" }));

            _mockStopwordRemover
                .Setup(m => m.RemoveStopwords(It.IsAny<List<string>>()))
                .Returns(new StopwordRemoverResult() { Words = new List<string>() { "asd" } });

            string inputText =
                $"Das ist das erste Kapitel.{Environment.NewLine}Das ist das zweite Kapitel.";

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: inputText);

            Assert.Equal(
                expected: 1,
                actual: actual.NumberOfChapters);
        }
    }
}
