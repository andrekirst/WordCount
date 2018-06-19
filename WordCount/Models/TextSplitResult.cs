using System.Collections.Generic;
using System.Linq;

namespace WordCount.Models
{
    public class TextSplitResult
    {
        public TextSplitResult(List<string> values)
        {
            Values = values;
        }

        public List<string> Values { get; }

        public bool ValuesAvailable => Values != null && Values.Any();
    }
}
