using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.SystemAbstractions
{
    [ExcludeFromCodeCoverage]
    public class Environment : IEnvironment
    {
        public string[] GetCommandLineArgs()
        {
            string[] commandLineArgs = System.Environment.GetCommandLineArgs();
            commandLineArgs = commandLineArgs
                .Skip(count: 1)
                .ToArray();
            return commandLineArgs;
        }
    }
}