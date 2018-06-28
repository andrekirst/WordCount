using System.Collections.Generic;
using System.Linq;

namespace WordCount.Models
{
    public class TextSplitResult
    {
        public TextSplitResult(List<string> words)
        {
            Words = words;
        }

        public List<string> Words { get; }

        public bool WordsAvailable => Words != null && Words.Any();
    }
}
