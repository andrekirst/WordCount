using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class IndexParameterParser : BaseParameterParser<IndexParameter>, IIndexParameterParser
    {
        private readonly IEnvironment _environment;

        public IndexParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public IndexParameter ParseIndexParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] commandLineArgs = _environment.GetCommandLineArgs() ?? Array.Empty<string>();
                return new IndexParameter
                {
                    IsPresent = commandLineArgs.Contains(value: "-index")
                };
            });
        }
    }
}
