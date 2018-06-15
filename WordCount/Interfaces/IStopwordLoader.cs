using System.Collections.Generic;

namespace WordCount.Interfaces
{
    public interface IStopwordLoader
    {
        List<string> GetStopwords();
    }
}
