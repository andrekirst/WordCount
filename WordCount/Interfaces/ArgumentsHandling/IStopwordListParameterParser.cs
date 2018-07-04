using WordCount.Models;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IStopwordListParameterParser
    {
        StopwordListParameter ParseStopwordListParameter();
    }
}
