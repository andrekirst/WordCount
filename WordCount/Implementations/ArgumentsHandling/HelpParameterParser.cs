using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class HelpParameterParser : BaseParameterParser<HelpParameter>, IHelpParameterParser
    {
        private IEnvironment Environment { get; }

        public HelpParameterParser(IEnvironment environment)
        {
            Environment = environment;
        }

        public HelpParameter ParseHelpParameter()
        {
            return CachedValue(() =>
            {
                var args = Environment.GetCommandLineArgs() ?? Array.Empty<string>();
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
}
