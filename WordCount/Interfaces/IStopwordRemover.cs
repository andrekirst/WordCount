using System.Collections.Generic;
using WordCount.Models.Results;

namespace WordCount.Interfaces
{
    public interface IStopwordRemover
    {
        StopwordRemoverResult RemoveStopwords(List<string> words);
    }
}
