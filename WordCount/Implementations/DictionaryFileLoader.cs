using System.Collections.Generic;
using System.IO.Abstractions;
using WordCount.Extensions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;

namespace WordCount.Implementations
{
    public class DictionaryFileLoader : IDictionaryFileLoader
    {
        private IFileSystem FileSystem { get; }
        private IDisplayOutput DisplayOutput { get; }
        private IDictionaryParameterParser DictionaryParameterParser { get; }

        public DictionaryFileLoader(
            IFileSystem fileSystem,
            IDisplayOutput displayOutput,
            IDictionaryParameterParser dictionaryParameterParser)
        {
            FileSystem = fileSystem;
            DisplayOutput = displayOutput;
            DictionaryParameterParser = dictionaryParameterParser;
        }

        public List<string> ReadWords()
        {
            var dictionaryParameter = DictionaryParameterParser.ParseDictionaryParameter();

            var path = dictionaryParameter.FileName;

            switch (dictionaryParameter.IsPresent)
            {
                case true when
                    !FileSystem.File.Exists(path):
                    DisplayOutput.WriteErrorResourceLine(
                        "FILE_NOT_FOUND",
                        path);
                    return new List<string>();
                case false:
                    return new List<string>();
                default:
                    return FileSystem
                        .File
                        .ReadAllLines(path)
                        .ToEmptyIfNullList();
            }
        }
    }
}
