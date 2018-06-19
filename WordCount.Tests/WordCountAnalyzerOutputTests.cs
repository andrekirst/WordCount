using WordCount.Implementations;
using Xunit;

namespace WordCount.Tests
{
    public class WordCountAnalyzerOutputTests
    {
        [Fact]
        public void WordCountAnalyzerOutputTests_DisplayResultAsString_Result_NumberOfWords_2_Expect_Number_of_Words_2_Number_of_unique_Words_1()
        {
            WordCountAnalyzerOutput systemUnderTest = new WordCountAnalyzerOutput();
            string actual = systemUnderTest.DisplayResultAsString(wordCountAnalyzerResult: new Models.WordCountAnalyzerResult()
            {
                NumberOfWords = 2,
                NumberOfUniqueWords = 1
            });
            string expected = "Number of words: 2, unique: 1";

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
