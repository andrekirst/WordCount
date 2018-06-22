using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class ConsoleParameterArgumentsReader : IArgumentsReader
    {
        private const string _indexParameterName = "-index";

        public ArgumentsReaderResult ReadArguments(string[] args)
        {
            List<string> argumentsList = args == null ? new List<string>() : args.ToList();
            bool isIndexParameterPresent = argumentsList.Contains(item: _indexParameterName);
            argumentsList.Remove(item: _indexParameterName);

            bool isSourceTextFilePresent = argumentsList.Any();

            return new ArgumentsReaderResult(
                sourceTextFile: isSourceTextFilePresent ? argumentsList.First() : null,
                isSourceTextFileParameterPresent: isSourceTextFilePresent,
                isIndexParameterPresent: isIndexParameterPresent);
        }
    }
}
