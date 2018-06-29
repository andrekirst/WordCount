using System;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class ParameterParser : IParameterParser
    {
        private readonly IDictionaryParameterParser _dictionaryParameterParser;
        private readonly IIndexParameterParser _indexParameterParser;
        private readonly ISourceFileParameterParser _sourceFileParameterParser;

        public ParameterParser(
            IDictionaryParameterParser dictionaryParameterParser,
            IIndexParameterParser indexParameterParser,
            ISourceFileParameterParser sourceFileParameterParser)
        {
            _dictionaryParameterParser = dictionaryParameterParser;
            _indexParameterParser = indexParameterParser;
            _sourceFileParameterParser = sourceFileParameterParser;
        }

        public Parameters ParseArguments(string[] args)
        {
            //return new Parameters
            //{
            //    DictionaryParameter = _dictionaryParameterParser.ParseDictionaryParameter(args: args),
            //    IndexParamater = _indexParameterParser.ParseIndexParameter(args: args),
            //    SourceFileParameter = _sourceFileParameterParser.ParseSourceFileParameter(args: args)
            //};
            throw new NotImplementedException();
        }
    }
}
