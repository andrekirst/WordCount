using System.Collections.Generic;
using System.IO.Abstractions;
using WordCount.Extensions;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class DictionaryFileLoader : IDictionaryFileLoader
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDisplayOutput _displayOutput;
        private readonly IDictionaryParameterParser _dictionaryParameterParser;

        public DictionaryFileLoader(
            IFileSystem fileSystem,
            IDisplayOutput displayOutput,
            IDictionaryParameterParser dictionaryParameterParser)
        {
            _fileSystem = fileSystem;
            _displayOutput = displayOutput;
            _dictionaryParameterParser = dictionaryParameterParser;
        }

        public List<string> ReadWords()
        {
            DictionaryParameter dictionaryParameter = _dictionaryParameterParser.ParseDictionaryParameter();

            string path = dictionaryParameter.FileName;

            if (dictionaryParameter.IsPresent &&
                !_fileSystem.File.Exists(path: path))
            {
                _displayOutput.WriteErrorResourceLine(
                    resourceIdent: "FILE_NOT_FOUND",
                    placeholderValues: path);
                return new List<string>();
            }

            if (!dictionaryParameter.IsPresent)
            {
                return new List<string>();
            }

            return _fileSystem
                .File
                .ReadAllLines(path: path)
                .ToEmptyIfNullList();
        }
    }
}
