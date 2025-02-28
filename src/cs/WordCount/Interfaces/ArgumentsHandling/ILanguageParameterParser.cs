using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface ILanguageParameterParser
    {
        LanguageParameter ParseLanguageParameter();
    }
}