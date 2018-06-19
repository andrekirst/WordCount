using System.Collections.Generic;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class StopwordRemover : IStopwordRemover
    {
        private readonly IStopwordLoader _stopwordLoader;

        public StopwordRemover(IStopwordLoader stopwordLoader)
        {
            _stopwordLoader = stopwordLoader;
        }

        public StopwordRemoverResult RemoveStopwords(List<string> values)
        {
            List<string> stopwords = _stopwordLoader.GetStopwords();

            values.RemoveAll(match: stopwords.Contains);
            
            return new StopwordRemoverResult()
            {
                Values = values
            };
        }
    }
}
