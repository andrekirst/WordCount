using System.Collections.Generic;
using System.Linq;

namespace WordCount.Models.Results
{
    public class TextSplitResult
    {
        public TextSplitResult()
        {
        }

        public TextSplitResult(List<string> words)
        {
            Words = words;
        }

        public List<string> Words { get; } = new List<string>();

        public bool WordsAvailable => Words != null && Words.Any();
    }
}
