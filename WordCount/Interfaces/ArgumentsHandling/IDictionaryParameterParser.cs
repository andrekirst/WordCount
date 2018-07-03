using WordCount.Models;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IDictionaryParameterParser
    {
        DictionaryParameter ParseDictionaryParameter();
    }
}
