using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class ConsoleParameterArgumentsReader : IArgumentsReader
    {
        private readonly IParameterParser _parameterParser;

        public ConsoleParameterArgumentsReader(IParameterParser parameterParser)
        {
            _parameterParser = parameterParser;
        }

        public ArgumentsReaderResult ReadArguments(string[] args)
        {
            Parameters parameters = _parameterParser.ParseArguments(args: args);

            IndexParameter indexParameter = parameters.IndexParamater;
            DictionaryParameter dictionaryParameter = parameters.DictionaryParameter;
            SourceFileParameter sourceFileParameter = parameters.SourceFileParameter;

            return new ArgumentsReaderResult
            {
                IsDictionaryParameterPresent = dictionaryParameter.IsPresent,
                IsIndexParameterPresent = indexParameter.IsPresent,
                SourceTextFile = sourceFileParameter.FileName,
                IsSourceTextFileParameterPresent = sourceFileParameter.IsPresent,
                DictionaryTextFile = dictionaryParameter.FileName
            };
        }
    }
}
