using System;
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
            if (string.IsNullOrWhiteSpace(value: text))
            {
                return new WordCountAnalyzerResult()
                {
                    NumberOfWords = 0
                };
            }

            List<string> splitByWhitespace = text.Split(
                separator: new[] { " ", Environment.NewLine },
                options: StringSplitOptions.None).ToList();

            if (stopwords != null)
            {
                splitByWhitespace.RemoveAll(match: stopwords.Contains); 
            }

            int numberOfWords = splitByWhitespace.Count;
            return new WordCountAnalyzerResult()
            {
                NumberOfWords = numberOfWords
            };
        }
    }
}
