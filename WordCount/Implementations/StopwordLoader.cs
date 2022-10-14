using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;

namespace WordCount.Implementations
{
    public class StopwordLoader : IStopwordLoader
    {
        private IFileSystem FileSystem { get; }
        private IStopwordListParameterParser StopwordListParameterParser { get; }
        private IDisplayOutput DisplayOutput { get; }
        private ILanguageParameterParser LanguageParameterParser { get; }

        public StopwordLoader(
            IFileSystem fileSystem,
            IStopwordListParameterParser stopwordListParameterParser,
            IDisplayOutput displayOutput,
            ILanguageParameterParser languageParameterParser)
        {
            FileSystem = fileSystem;
            StopwordListParameterParser = stopwordListParameterParser;
            DisplayOutput = displayOutput;
            LanguageParameterParser = languageParameterParser;
        }

        public List<string> GetStopwords()
        {
            var stopwordListParameter = StopwordListParameterParser.ParseStopwordListParameter();
            var languageParameter = LanguageParameterParser.ParseLanguageParameter();

            var isStopwordListParameterPresent = stopwordListParameter.IsPresent;

            var fileName = isStopwordListParameterPresent ?
                stopwordListParameter.FileName :
                $"stopwords.{languageParameter.Language}.txt";

            if (!FileSystem.File.Exists(fileName))
            {
                return new List<string>();
            }

            if (isStopwordListParameterPresent)
            {
                DisplayOutput.WriteResourceLine(
                    "USED_STOPWORDLIST",
                    fileName);
            }

            return FileSystem
                .File
                .ReadAllLines(fileName)
                .ToList();
        }
    }
}
