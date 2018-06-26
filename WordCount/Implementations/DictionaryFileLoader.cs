using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class DictionaryFileLoader : IDictionaryFileLoader
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDisplayOutput _displayOutput;

        public DictionaryFileLoader(
            IFileSystem fileSystem,
            IDisplayOutput displayOutput)
        {
            _fileSystem = fileSystem;
            _displayOutput = displayOutput;
        }

        public List<string> ReadWords(string path)
        {
            if (!_fileSystem.File.Exists(path: path))
            {
                _displayOutput.WriteErrorLine(errorMessage: $"File \"{path}\" not found.");
                return new List<string>();
            }

            string[] result = _fileSystem
                .File
                .ReadAllLines(path: path);
            
            return result == null ? new List<string>() : result.ToList();
        }
    }
}
