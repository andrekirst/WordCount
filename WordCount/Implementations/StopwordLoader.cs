using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class StopwordLoader : IStopwordLoader
    {
        private const string StopwordFileName = "stopwords.txt";
        private readonly IFileSystem _fileSystem;

        public StopwordLoader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public List<string> GetStopwords()
        {
            if (!_fileSystem.File.Exists(path: StopwordFileName))
            {
                return null;
            }

            return _fileSystem
                .File
                .ReadAllLines(path: StopwordFileName)
                .ToList();
        }
    }
}
