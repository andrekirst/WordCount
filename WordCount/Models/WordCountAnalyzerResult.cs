using System.Collections.Generic;

namespace WordCount.Models
{
    public class WordCountAnalyzerResult
    {
        public int NumberOfWords { get; set; }

        public int NumberOfUniqueWords { get; set; }

        public double AverageWordLength { get; set; }

        public List<string> DistinctWords { get; set; }
    }
}
