using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface ITextUrlParameterParser
    {
        TextUrlParameter ParseTextUrlParameter();
    }
}
