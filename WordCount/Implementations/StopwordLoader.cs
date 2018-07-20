using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class StopwordLoader : IStopwordLoader
    {
        private readonly IFileSystem _fileSystem;
        private readonly IStopwordListParameterParser _stopwordListParameterParser;
        private readonly IDisplayOutput _displayOutput;
        private readonly ILanguageParameterParser _languageParameterParser;

        public StopwordLoader(
            IFileSystem fileSystem,
            IStopwordListParameterParser stopwordListParameterParser,
            IDisplayOutput displayOutput,
            ILanguageParameterParser languageParameterParser)
        {
            _fileSystem = fileSystem;
            _stopwordListParameterParser = stopwordListParameterParser;
            _displayOutput = displayOutput;
            _languageParameterParser = languageParameterParser;
        }

        public List<string> GetStopwords()
        {
            StopwordListParameter stopwordListParameter = _stopwordListParameterParser.ParseStopwordListParameter();
            LanguageParameter languageParameter = _languageParameterParser.ParseLanguageParameter();

            bool isStopwordListParameterPresent = stopwordListParameter.IsPresent;

            string fileName = isStopwordListParameterPresent ?
                stopwordListParameter.FileName :
                $"stopwords.{languageParameter.Language}.txt";

            if (!_fileSystem.File.Exists(path: fileName))
            {
                return new List<string>();
            }

            if (isStopwordListParameterPresent)
            {
                _displayOutput.WriteResourceLine(
                    resourceIdent: "USED_STOPWORDLIST",
                    placeholderValues: fileName);
            }

            return _fileSystem
                .File
                .ReadAllLines(path: fileName)
                .ToList();
        }
    }
}
