using Moq;
using System;
using System.Collections.Generic;
using Autofac;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class WordCountAnalyzerTests
    {
        private Mock<ITextSplit> _mockTextSplit;

        private readonly WordCountAnalyzer _systemUnderTest;

        public WordCountAnalyzerTests()
        {
            _mockTextSplit = new Mock<ITextSplit>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(instance: _mockTextSplit.Object)
                .As<ITextSplit>()
                .SingleInstance();

            containerBuilder
                .RegisterType<WordCountAnalyzer>()
                .SingleInstance();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<WordCountAnalyzer>();
        }

        [Fact]
        public void WordCountAnalyzerTests_Analyze_Text_Bla_bla_Expect_2_Words()
        {
            string text = "Bla bla";

            _mockTextSplit
                .Setup(m => m.Split(text))
                .Returns(new TextSplitResult(new List<string>() { "Bla", "bla" }));

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: text);

            Assert.Equal(expected: 2, actual: actual.NumberOfWords);
        }

        [Fact]
        public void WordCountAnalyzerTests_Analyze_Stopword_a_This_is_a_Text_Expect_3_Words()
        {
            const string text = "This is a Text";

            _mockTextSplit
                .Setup(m => m.Split(text))
                .Returns(new TextSplitResult(new List<string>() { "This", "is", "a", "Text" }));

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(
                text: text,
                stopwords: new List<string>() { "a" });

            Assert.Equal(expected: 3, actual: actual.NumberOfWords);
        }

        [Fact]
        public void WordCountAnalyzerTests_Analyze_Stopword_a_Mary_has_a_little_lamb_with_Newlines_Expect_4_Words()
        {
            string text = $"Mary had{Environment.NewLine}a little{Environment.NewLine}lamb";

            _mockTextSplit
                .Setup(m => m.Split(text))
                .Returns(new TextSplitResult(new List<string>() { "Mary", "had", "a", "little", "lamb" }));

            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(
                text: text,
                stopwords: new List<string>() { "the", "a", "on", "off" });

            Assert.Equal(expected: 4, actual: actual.NumberOfWords);
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
    }
}
