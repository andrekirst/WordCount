using System.IO.Abstractions;
using WordCount.Abstractions.Console;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class TextInput : ITextInput
    {
        private readonly IConsole _console;
        private readonly ITextFileLoader _textFileLoader;

        public TextInput(
            IConsole console,
            ITextFileLoader textFileLoader)
        {
            _console = console;
            _textFileLoader = textFileLoader;
        }

        public string GetInputText(ArgumentsReaderResult argumentsReaderResult)
        {
            return argumentsReaderResult != null && argumentsReaderResult.IsSourceTextFileParameterPresent ?
                _textFileLoader.ReadTextFile(path: argumentsReaderResult.SourceTextFile) :
                _console.ReadLine();
        }
    }
}
