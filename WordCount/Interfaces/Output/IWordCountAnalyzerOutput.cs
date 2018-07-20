using WordCount.Models.Results;

namespace WordCount.Interfaces.Output
{
    public interface IWordCountAnalyzerOutput
    {
        void DisplayResult(WordCountAnalyzerResult result);
    }
}
