using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class IndexOutput : IIndexOutput
    {
        private readonly IDisplayOutput _displayOutput;

        public IndexOutput(IDisplayOutput displayOutput)
        {
            _displayOutput = displayOutput;
        }

        public void OutputIndex(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            _displayOutput.WriteLine(text: "Index:");

            foreach (string distinctWord in wordCountAnalyzerResult
                .DistinctWords
                .OrderBy(keySelector: s => s))
            {
                _displayOutput.WriteLine(text: distinctWord);
            }
        }
    }
}
