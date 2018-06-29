using WordCount.Models;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IArgumentsReader
    {
        ArgumentsReaderResult ReadArguments(string[] args);
    }
}
