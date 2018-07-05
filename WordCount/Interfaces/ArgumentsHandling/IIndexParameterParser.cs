using WordCount.Models.Parameters;

namespace WordCount.Interfaces.ArgumentsHandling
{
    public interface IIndexParameterParser
    {
        IndexParameter ParseIndexParameter();
    }
}
