using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface IWordCountAnalyzerOutput
    {
        void DisplayResult(WordCountAnalyzerResult wordCountAnalyzerResult);
    }
}
