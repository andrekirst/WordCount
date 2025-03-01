using WordCount.Models.Parameters;

namespace WordCount;

public interface IParameterParser<TParameter>
    where TParameter : BaseParameter
{
    TParameter ParseParameter(string[] args);
}