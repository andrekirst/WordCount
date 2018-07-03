using System.IO;
using System.IO.Abstractions;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class TextFileLoader : ITextFileLoader
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDisplayOutput _displayOutput;

        public TextFileLoader(
            IFileSystem fileSystem,
            IDisplayOutput displayOutput)
        {
            _fileSystem = fileSystem;
            _displayOutput = displayOutput;
        }

        public string ReadTextFile(string path)
        {
            if (_fileSystem.File.Exists(path: path))
            {
                return _fileSystem.File.ReadAllText(path: path);
            }
            else
            {
                _displayOutput.WriteErrorLine(errorMessage: $"File \"{path}\" not found.");
                return string.Empty;
            }
        }
    }
}
