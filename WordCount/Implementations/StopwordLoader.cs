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
            StopwordListParameter stopwordListParameter = StopwordListParameterParser.ParseStopwordListParameter();
            LanguageParameter languageParameter = LanguageParameterParser.ParseLanguageParameter();

            bool isStopwordListParameterPresent = stopwordListParameter.IsPresent;

            string fileName = isStopwordListParameterPresent ?
                stopwordListParameter.FileName :
                $"stopwords.{languageParameter.Language}.txt";

            if (!FileSystem.File.Exists(path: fileName))
            {
                return new List<string>();
            }

            if (isStopwordListParameterPresent)
            {
                DisplayOutput.WriteResourceLine(
                    resourceIdent: "USED_STOPWORDLIST",
                    placeholderValues: fileName);
            }

            return FileSystem
                .File
                .ReadAllLines(path: fileName)
                .ToList();
        }
    }
}
