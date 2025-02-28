using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class IndexParameterParser(IEnvironment environment) : BaseParameterParser<IndexParameter>, IIndexParameterParser
{
    public IndexParameter ParseIndexParameter()
    {
        return CachedValue(() =>
        {
            var commandLineArgs = environment.GetCommandLineArgs() ?? Array.Empty<string>();
            return new IndexParameter
            {
                IsPresent = commandLineArgs.Contains("-index")
            };
        });
    }
}
