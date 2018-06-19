using System.Collections.Generic;
using WordCount.Implementations;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class WordCountAnalyzerTests
    {
        private readonly WordCountAnalyzer _systemUnderTest;

        public WordCountAnalyzerTests()
        {
            _systemUnderTest = new WordCountAnalyzer();
        }

        [Fact]
        public void WordCountAnalyzerTests_Analyze_Text_Bla_bla_Expect_2_Words()
        {
            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: "Bla bla");

            Assert.Equal(expected: 2, actual: actual.NumberOfWords);
        }

        [Fact]
        public void WordCountAnalyzerTests_Analyze_Stopword_a_This_is_a_Text_Expect_3_Words()
        {
            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(
                text: "This is a Text",
                stopwords: new List<string>() { "a" });

            Assert.Equal(expected: 3, actual: actual.NumberOfWords);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WordCountAnalyzerTests_Analyze_empty_strings_Expect_0_Words(
            string inputtext)
        {
            WordCountAnalyzerResult actual = _systemUnderTest.Analyze(text: inputtext);

            Assert.Equal(expected: 0, actual: actual.NumberOfWords);
        }
    }
}
