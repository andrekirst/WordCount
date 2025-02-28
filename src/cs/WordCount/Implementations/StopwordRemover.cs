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
            var stopwords = StopwordLoader.GetStopwords();
            var newWordsList = new List<string>(words);
            newWordsList.RemoveAll(stopwords.Contains);
            
            return new StopwordRemoverResult
            {
                Words = newWordsList
            };
        }
    }
}
