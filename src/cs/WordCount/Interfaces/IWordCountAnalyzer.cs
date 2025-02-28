using WordCount.Models.Results;

namespace WordCount.Interfaces
{
    public interface IWordCountAnalyzer
    {
        WordCountAnalyzerResult Analyze(string text);
    }
}
