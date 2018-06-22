using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class ConsoleParameterArgumentsReader : IArgumentsReader
    {
        private const string IndexParameterName = "-index";

        public ArgumentsReaderResult ReadArguments(string[] args)
        {
            List<string> argumentsList = args == null ? new List<string>() : args.ToList();
            bool isIndexParameterPresent = argumentsList.Contains(item: IndexParameterName);
            argumentsList.Remove(item: IndexParameterName);

            bool isSourceTextFileParameterPresent = argumentsList.Any();

            return new ArgumentsReaderResult(
                sourceTextFile: isSourceTextFileParameterPresent ? argumentsList.First() : null,
                isSourceTextFileParameterPresent: isSourceTextFileParameterPresent,
                isIndexParameterPresent: isIndexParameterPresent);
        }
    }
}
