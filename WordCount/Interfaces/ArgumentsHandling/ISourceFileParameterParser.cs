using WordCount.Models;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface ISourceFileParameterParser
    {
        SourceFileParameter ParseSourceFileParameter(string[] args);
    }
}
