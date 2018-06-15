using System.Collections.Generic;
using WordCount.Implementations;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class WordCountAnalyzerTests
    {
        [Fact]
        public void WordCountAnalyzerTests_Analyze_Text_Bla_bla_Expect_2_Words()
        {
            WordCountAnalyzer systemUnterTest = new WordCountAnalyzer();
            WordCountAnalyzerResult actual = systemUnterTest.Analyze("Bla bla");

            Assert.Equal(2, actual: actual.NumberOfWords);
        }

        [Fact]
        public void WordCountAnalyzerTests_Analyze_Stopword_a_This_is_a_Text_Expect_3_Words()
        {
            WordCountAnalyzer systemUnterTest = new WordCountAnalyzer();
            WordCountAnalyzerResult actual = systemUnterTest.Analyze(
                "This is a Text",
                new List<string>() { "a" });

            Assert.Equal(3, actual: actual.NumberOfWords);
        }
    }
}
