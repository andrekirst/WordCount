using WordCount.Abstractions.Console;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class TextInput : ITextInput
    {
        private readonly IConsole _console;
        private readonly ITextFileLoader _textFileLoader;
        private readonly ISourceFileParameterParser _sourceFileParameterParser;
        private readonly IDisplayOutput _displayOutput;

        public TextInput(
            IConsole console,
            ITextFileLoader textFileLoader,
            ISourceFileParameterParser sourceFileParameterParser,
            IDisplayOutput displayOutput)
        {
            _console = console;
            _textFileLoader = textFileLoader;
            _sourceFileParameterParser = sourceFileParameterParser;
            _displayOutput = displayOutput;
        }

        public InputTextResult GetInputText()
        {
            SourceFileParameter sourceFileParameter = _sourceFileParameterParser.ParseSourceFileParameter();

            if (!sourceFileParameter.IsPresent)
            {
                _displayOutput.Write(text: "Enter text: ");
            }

            string text = sourceFileParameter.IsPresent
                ? _textFileLoader.ReadTextFile(path: sourceFileParameter.FileName)
                : _console.ReadLine();

            return new InputTextResult
            {
                IsConsoleInput = !sourceFileParameter.IsPresent,
                Text = text
            };
        }
    }
}
