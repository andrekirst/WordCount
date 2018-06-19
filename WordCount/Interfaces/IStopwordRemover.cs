using System.Collections.Generic;
using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface IStopwordRemover
    {
        StopwordRemoverResult RemoveStopwords(List<string> values);
    }
}
