using WordCount.Models.Results;

namespace WordCount.Interfaces.Language
{
    public interface ILanguageDecision
    {
        DecideLanguageResult DecideLanguage();
    }
}
