using System.Collections.Generic;

namespace WordCount.Interfaces
{
    public interface IDictionaryFileLoader
    {
        List<string> ReadWords(string path);
    }
}
