using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class IndexParameterParser : BaseParameterParser<IndexParameter>, IIndexParameterParser
    {
        private IEnvironment Environment { get; }

        public IndexParameterParser(IEnvironment environment)
        {
            Environment = environment;
        }

        public IndexParameter ParseIndexParameter()
        {
            return CachedValue(() =>
            {
                var commandLineArgs = Environment.GetCommandLineArgs() ?? Array.Empty<string>();
                return new IndexParameter
                {
                    IsPresent = commandLineArgs.Contains("-index")
                };
            });
        }
    }
}
