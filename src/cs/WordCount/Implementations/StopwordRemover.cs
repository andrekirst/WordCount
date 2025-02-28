using System.Collections.Generic;
using WordCount.Interfaces;
using WordCount.Models.Results;

namespace WordCount.Implementations;

public class StopwordRemover(IStopwordLoader stopwordLoader) : IStopwordRemover
{
    public StopwordRemoverResult RemoveStopwords(List<string> words)
    {
        var stopwords = stopwordLoader.GetStopwords();
        var newWordsList = new List<string>(words);
        newWordsList.RemoveAll(stopwords.Contains);
        
        return new StopwordRemoverResult
        {
            Words = newWordsList
        };
    }
}
