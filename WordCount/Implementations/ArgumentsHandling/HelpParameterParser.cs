using System;
using System.Linq;
using WordCount.Abstractions.Environment;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class HelpParameterParser : IHelpParameterParser
    {
        private readonly IEnvironment _environment;

        public HelpParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public HelpParameter ParseHelpParameter()
        {
            string[] args = _environment.GetCommandLineArgs() ?? Array.Empty<string>();
            if (!args.Any())
            {
                return new HelpParameter()
                {
                    IsPresent = false
                };
            }

            bool isPresent = args.Any(predicate: s => s.StartsWith(value: "-help") ||
                                                      s.StartsWith(value: "-h"));

            return new HelpParameter
            {
                IsPresent = isPresent
            };
        }
    }
}
