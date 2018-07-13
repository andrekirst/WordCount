using System;
using System.IO.Abstractions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class TextFileLoader : ITextFileLoader
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDisplayOutput _displayOutput;
        private readonly ISourceFileParameterParser _sourceFileParameterParser;

        public TextFileLoader(
            IFileSystem fileSystem,
            IDisplayOutput displayOutput,
            ISourceFileParameterParser sourceFileParameterParser)
        {
            _fileSystem = fileSystem;
            _displayOutput = displayOutput;
            _sourceFileParameterParser = sourceFileParameterParser;
        }

        public string ReadTextFile()
        {
            SourceFileParameter sourceFileParameter = _sourceFileParameterParser.ParseSourceFileParameter();

            if (!sourceFileParameter.IsPresent)
            {
                return string.Empty;
            }

            string fileName = sourceFileParameter.FileName;

            if (_fileSystem.File.Exists(path: fileName))
            {
                string text = _fileSystem.File.ReadAllText(path: fileName);
                text = text.Replace(
                    oldValue: $"-{Environment.NewLine}",
                    newValue: string.Empty);
                return text;
            }
            else
            {
                _displayOutput
                    .WriteErrorResourceLine(
                        resourceIdent: "FILE_NOT_FOUND",
                        placeholderValues: fileName);
                return string.Empty;
            }
        }
    }
}
