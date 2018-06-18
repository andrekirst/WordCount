using System.IO.Abstractions;
using WordCount.Abstractions.Console;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class TextInput : ITextInput
    {
        private readonly IConsole _console;
        private readonly IFileSystem _fileSystem;

        public TextInput(
            IConsole console,
            IFileSystem fileSystem)
        {
            _console = console;
            _fileSystem = fileSystem;
        }

        public string GetInputText(ArgumentsReaderResult argumentsReaderResult)
        {
            return argumentsReaderResult != null && argumentsReaderResult.IsSourceTextFilePresent ?
                _fileSystem.File.ReadAllText(path: argumentsReaderResult.SourceTextFile) :
                _console.ReadLine();
        }
    }
}
