using System.Collections.Generic;

namespace WordCount.Models
{
    public class ArgumentsReaderResult
    {
        public string SourceTextFile { get; set; }

        public bool IsSourceTextFileParameterPresent { get; set; }

        public bool IsIndexParameterPresent { get; set; }

        public bool IsDictionaryParameterPresent { get; set; }
        public string DictionaryTextFile { get; set; }
    }
}
