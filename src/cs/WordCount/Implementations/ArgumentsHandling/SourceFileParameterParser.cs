using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class SourceFileParameterParser(IEnvironment environment) : BaseParameterParser<SourceFileParameter>, ISourceFileParameterParser
{
    private IEnvironment Environment { get; } = environment;

    public SourceFileParameter ParseSourceFileParameter()
    {
        return CachedValue(() =>
        {
            var commandLineArgs = Environment.GetCommandLineArgs() ?? [];

            var fileName = commandLineArgs
                                  .FirstOrDefault(s => !s.StartsWith("-")) ?? string.Empty;

            var isPresent = fileName.IsFilled();

            return new SourceFileParameter
            {
                IsPresent = isPresent,
                FileName = fileName
            };
        });
    }
}
