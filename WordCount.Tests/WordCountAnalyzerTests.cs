using System;
using System.Linq;
using WordCount.Implementations;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class WordCountAnalyzerTests
    {
        public WordCountAnalyzerTests()
        {
        }

        [Fact]
        public void WordCountAnalyzerTests_Analyze_Text_Bla_bla_Expect_2_Words()
        {
            WordCountAnalyzer systemUnterTest = new WordCountAnalyzer();
            WordCountAnalyzerResult actual = systemUnterTest.Analyze(text: "Bla bla");

            Assert.Equal(expected: 2, actual: actual.NumberOfWords);
        }
    }
}
