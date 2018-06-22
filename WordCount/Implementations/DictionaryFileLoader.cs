using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class DictionaryFileLoader : IDictionaryFileLoader
    {
        private readonly IFileSystem _fileSystem;

        public DictionaryFileLoader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public List<string> ReadWords(string path)
        {
            if (!_fileSystem.File.Exists(path: path))
            {
                return new List<string>();
            }

            string[] result = _fileSystem.File.ReadAllLines(path: path);

            if (result == null)
            {
                return new List<string>();
            }

            return _fileSystem
                .File
                .ReadAllLines(path: path)
                .ToList();
        }
    }
}
