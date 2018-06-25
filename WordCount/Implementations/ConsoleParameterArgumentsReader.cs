using System.Collections.Generic;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class ConsoleParameterArgumentsReader : IArgumentsReader
    {
        private const string IndexParameterName = "-index";
        private const string DictionaryParameterPrefix = "-dictionary=";

        public ArgumentsReaderResult ReadArguments(string[] args)
        {
            List<string> argumentsList = args == null ? new List<string>() : args.ToList();

            // TODO Refactoring ArgumentParamer: Index
            bool isIndexParameterPresent = argumentsList.Contains(item: IndexParameterName);
            argumentsList.Remove(item: IndexParameterName);

            // TODO Refactoring ArgumentParamer: Dictionary
            string dictionaryParameter = argumentsList
                .FirstOrDefault(predicate: p => p.StartsWith(value: DictionaryParameterPrefix));
            bool isDictionaryParameterPresent = !string.IsNullOrEmpty(value: dictionaryParameter);
            argumentsList.RemoveAll(match: p => p.StartsWith(value: DictionaryParameterPrefix));
            string dictionaryTextFile = dictionaryParameter?.Split('=')[1];

            bool isSourceTextFileParameterPresent = argumentsList.Any();

            return new ArgumentsReaderResult
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
