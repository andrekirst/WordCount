using System.Collections.Generic;

namespace WordCount.Models
{
    public class IndexOutputRequest
    {
        public List<string> DistinctWords { get; set; }

        public string DictionaryTextFile { get; set; }

        public bool IsDictionaryParameterPresent { get; set; }
    }
}
