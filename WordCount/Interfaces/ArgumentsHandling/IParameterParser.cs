using WordCount.Models;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IParameterParser
    {
        Parameters ParseArguments(string[] args);
    }
}
