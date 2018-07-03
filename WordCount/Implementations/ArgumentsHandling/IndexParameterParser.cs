using System.Linq;
using WordCount.Abstractions.Console;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class IndexParameterParser : IIndexParameterParser
    {
        private readonly IEnvironment _environment;

        public IndexParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public IndexParameter ParseIndexParameter()
        {
            string[] commandLineArgs = _environment.GetCommandLineArgs();
            return new IndexParameter()
            {
                IsPresent = commandLineArgs.Contains(value: "-index")
            };
        }
    }
}
