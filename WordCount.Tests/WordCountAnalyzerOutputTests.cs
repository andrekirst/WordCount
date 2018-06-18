using WordCount.Implementations;
using Xunit;

namespace WordCount.Tests
{
    public class WordCountAnalyzerOutputTests
    {
        [Fact]
        public void WordCountAnalyzerOutputTests_DisplayResultAsString_Result_NumberOfWords_2_Expect_Number_of_Words_2()
        {
            WordCountAnalyzerOutput systemUnderTest = new WordCountAnalyzerOutput();
            string actual = systemUnderTest.DisplayResultAsString(wordCountAnalyzerResult: new Models.WordCountAnalyzerResult()
            {
                NumberOfWords = 2
            });
            string expected = "Number of words: 2";

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
