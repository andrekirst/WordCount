using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface IArgumentsReader
    {
        ArgumentsReaderResult ReadArguments(string[] args);
    }
}
