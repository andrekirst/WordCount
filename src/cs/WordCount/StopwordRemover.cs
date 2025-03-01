using System.Collections.Generic;

namespace WordCount;

public interface IStopwordRemover
{
    StopwordRemoverResult RemoveStopwords(List<string> words);
}

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

public class StopwordRemoverResult
{
    public List<string> Words { get; set; } = [];
}
