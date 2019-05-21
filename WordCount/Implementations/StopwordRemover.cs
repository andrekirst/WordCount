using System.Collections.Generic;
using WordCount.Interfaces;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    public class StopwordRemover : IStopwordRemover
    {
        private IStopwordLoader StopwordLoader { get; }

        public StopwordRemover(IStopwordLoader stopwordLoader)
        {
            StopwordLoader = stopwordLoader;
        }

        public StopwordRemoverResult RemoveStopwords(List<string> words)
        {
            List<string> stopwords = StopwordLoader.GetStopwords();
            List<string> newWordsList = new List<string>(collection: words);
            newWordsList.RemoveAll(match: stopwords.Contains);
            
            return new StopwordRemoverResult
            {
                Words = newWordsList
            };
        }
    }
}
