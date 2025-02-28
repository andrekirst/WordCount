using System.Collections.Generic;

namespace WordCount.Models.Results
{
    public class WordCountAnalyzerResult
    {
        public int NumberOfWords { get; set; }

        public int NumberOfUniqueWords { get; set; }

        public double AverageWordLength { get; set; }

        public List<string> DistinctWords { get; set; }

        public int NumberOfChapters { get; set; }
    }
}
