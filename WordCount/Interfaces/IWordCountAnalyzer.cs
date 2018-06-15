using System.Collections.Generic;
using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface IWordCountAnalyzer
    {
        WordCountAnalyzerResult Analyze(
            string text,
            List<string> stopwords = null);
    }
}
