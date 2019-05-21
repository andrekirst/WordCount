using System.Collections.Generic;
using System.IO.Abstractions;
using WordCount.Extensions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;

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
            DictionaryParameter dictionaryParameter = DictionaryParameterParser.ParseDictionaryParameter();

            string path = dictionaryParameter.FileName;

            if (dictionaryParameter.IsPresent &&
                !FileSystem.File.Exists(path: path))
            {
                DisplayOutput.WriteErrorResourceLine(
                    resourceIdent: "FILE_NOT_FOUND",
                    placeholderValues: path);
                return new List<string>();
            }

            if (!dictionaryParameter.IsPresent)
            {
                return new List<string>();
            }

            return FileSystem
                .File
                .ReadAllLines(path: path)
                .ToEmptyIfNullList();
        }
    }
}
