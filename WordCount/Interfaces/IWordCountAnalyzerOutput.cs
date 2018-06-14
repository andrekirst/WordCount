using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface IWordCountAnalyzerOutput
    {
        string DisplayResultAsString(WordCountAnalyzerResult wordCountAnalyzerResult);
    }
}
