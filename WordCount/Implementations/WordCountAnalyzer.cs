using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class WordCountAnalyzer : IWordCountAnalyzer
    {
        public WordCountAnalyzerResult Analyze(string text)
        {
            int numberOfWords = text.Split(' ').Count();
            return new WordCountAnalyzerResult()
            {
                NumberOfWords = numberOfWords
            };
        }
    }
}
