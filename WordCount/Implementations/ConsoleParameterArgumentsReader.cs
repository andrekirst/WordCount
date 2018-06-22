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

            string dictionaryParameter = argumentsList
                .FirstOrDefault(predicate: p => p.StartsWith(value: "-dictionary="));

            bool isDictionaryParameterPresent = !string.IsNullOrEmpty(value: dictionaryParameter);
            string dictionaryTextFile = dictionaryParameter?.Split('=')[1];

            bool isSourceTextFileParameterPresent = argumentsList.Any();

            return new ArgumentsReaderResult()
            {
                IsDictionaryParameterPresent = isDictionaryParameterPresent,
                IsIndexParameterPresent = isIndexParameterPresent,
                SourceTextFile = isSourceTextFileParameterPresent ? argumentsList.First() : null,
                IsSourceTextFileParameterPresent = isSourceTextFileParameterPresent,
                DictionaryTextFile = dictionaryTextFile
            };
        }
    }
}
