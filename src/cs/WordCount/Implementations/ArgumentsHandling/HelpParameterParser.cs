using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class HelpParameterParser(IEnvironment environment) : BaseParameterParser<HelpParameter>, IHelpParameterParser
{
    public HelpParameter ParseHelpParameter()
    {
        return CachedValue(() =>
        {
            var args = environment.GetCommandLineArgs() ?? [];
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
