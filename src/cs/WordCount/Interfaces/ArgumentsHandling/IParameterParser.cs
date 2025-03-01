using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling;

public interface IParameterParser<TParameter>
    where TParameter : BaseParameter
{
    TParameter ParseParameter(string[] args);
}