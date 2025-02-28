using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface ISourceFileParameterParser
    {
        SourceFileParameter ParseSourceFileParameter();
    }
}
