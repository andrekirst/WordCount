using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class WordCountAnalyzer : IWordCountAnalyzer
    {
        public WordCountAnalyzerResult Analyze(
            string text,
            List<string> stopwords = null)
        {
            List<string> splitByWhitespace = text.Split(' ').ToList();
            if (stopwords != null)
            {
                splitByWhitespace.RemoveAll((s) =>
                {
                    return stopwords.Contains(item: s);
                }); 
            }
            int numberOfWords = splitByWhitespace.Count;
            return new WordCountAnalyzerResult()
            {
                NumberOfWords = numberOfWords
            };
        }
    }
}
