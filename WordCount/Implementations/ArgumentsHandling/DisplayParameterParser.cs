using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class DisplayParameterParser : BaseParameterParser<DisplayParameter>, IDisplayParameterParser
    {
        private readonly IEnvironment _environment;

        public DisplayParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public DisplayParameter ParseDisplayParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] commandLineArgs = _environment.GetCommandLineArgs() ?? Array.Empty<string>();
                return new DisplayParameter
                {
                    IsPresent = commandLineArgs.Contains(value: "-display")
                };
            });
        }
    }
}