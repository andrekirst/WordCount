using System.Collections.Generic;

namespace WordCount.Models
{
    public class StopwordRemoverResult
    {
        public List<string> Words { get; set; } = new List<string>();
    }
}
