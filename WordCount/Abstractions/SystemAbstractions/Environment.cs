using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.SystemAbstractions
{
    [ExcludeFromCodeCoverage]
    public class Environment : IEnvironment
    {
        public string[] GetCommandLineArgs() =>
            System.Environment.GetCommandLineArgs()
                .Skip(count: 1)
                .ToArray();
    }
}