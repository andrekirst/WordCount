using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IDisplayParameterParser
    {
        DisplayParameter ParseDisplayParameter();
    }
}