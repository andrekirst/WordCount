using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IHelpParameterParser
    {
        HelpParameter ParseHelpParameter();
    }
}
