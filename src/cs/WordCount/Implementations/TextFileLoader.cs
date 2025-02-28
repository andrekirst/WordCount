using System;
using System.IO.Abstractions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class TextFileLoader : ITextFileLoader
    {
        private IFileSystem FileSystem { get; }
        private IDisplayOutput DisplayOutput { get; }
        private ISourceFileParameterParser SourceFileParameterParser { get; }

        public TextFileLoader(
            IFileSystem fileSystem,
            IDisplayOutput displayOutput,
            ISourceFileParameterParser sourceFileParameterParser)
        {
            FileSystem = fileSystem;
            DisplayOutput = displayOutput;
            SourceFileParameterParser = sourceFileParameterParser;
        }

        public string ReadTextFile()
        {
            var sourceFileParameter = SourceFileParameterParser.ParseSourceFileParameter();

            if (!sourceFileParameter.IsPresent)
            {
                return string.Empty;
            }

            var fileName = sourceFileParameter.FileName;

            if (FileSystem.File.Exists(fileName))
            {
                return FileSystem.File.ReadAllText(fileName)
                    .Replace(
                    $"-{Environment.NewLine}",
                    string.Empty);
            }

            DisplayOutput
                .WriteErrorResourceLine(
                    "FILE_NOT_FOUND",
                    fileName);
            return string.Empty;
        }
    }
}
