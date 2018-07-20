using WordCount.Models.Results;

namespace WordCount.Interfaces
{
    public interface IWordCountAnalyzerOutput
    {
        void DisplayResult(WordCountAnalyzerResult result);
    }
}
