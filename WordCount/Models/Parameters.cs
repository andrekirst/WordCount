using System;

namespace WordCount.Models
{
    public class Parameters
    {
        public Parameters()
        {
        }

        public IndexParameter IndexParamater { get; internal set; }

        public DictionaryParameter DictionaryParameter { get; internal set; }

        public SourceFileParameter SourceFileParameter { get; internal set; }
}
}
