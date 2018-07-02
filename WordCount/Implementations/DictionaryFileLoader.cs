using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

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

            if (!_fileSystem.File.Exists(path: path))
            {
                _displayOutput.WriteErrorLine(errorMessage: $"File \"{path}\" not found.");
                return new List<string>();
            }

            string[] result = _fileSystem
                .File
                .ReadAllLines(path: path);

            return result == null ?
                new List<string>() :
                result.ToList();
        }
    }
}
