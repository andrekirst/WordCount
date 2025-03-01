using System.Linq;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class HelpParameterParser : BaseParameterParser<HelpParameter>, IParameterParser<HelpParameter>
{
    public HelpParameter ParseParameter(string[] args)
    {
        return CachedValue(() =>
        {
            if (!args.Any())
            {
                return new HelpParameter()
                {
                    IsPresent = false
                };
            }

            var isPresent = args.Any(s => s.StartsWith("-help") ||
                                          s.StartsWith("-h"));

            return new HelpParameter
            {
                IsPresent = isPresent
            };
        });
    }
}
