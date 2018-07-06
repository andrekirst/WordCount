using System.Collections.Generic;

namespace WordCount.Models.Requests
{
    public class IndexOutputRequest
    {
        public List<string> DistinctWords { get; set; }
    }
}
