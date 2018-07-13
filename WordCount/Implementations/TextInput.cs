using WordCount.Abstractions.Console;
using WordCount.Extensions;
using WordCount.Interfaces;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    public class TextInput : ITextInput
    {
        private readonly IConsole _console;
        private readonly ITextFileLoader _textFileLoader;
        private readonly ITextUrlFileLoader _textUrlFileLoader;
        private readonly IDisplayOutput _displayOutput;

        public TextInput(
            IConsole console,
            ITextFileLoader textFileLoader,
            ITextUrlFileLoader textUrlFileLoader,
            IDisplayOutput displayOutput)
        {
            _console = console;
            _textFileLoader = textFileLoader;
            _textUrlFileLoader = textUrlFileLoader;
            _displayOutput = displayOutput;
        }

        public InputTextResult GetInputText()
        {
            string text = _textFileLoader.ReadTextFile();
            if (text.IsFilled())
            {
                return new InputTextResult
                {
                    HasEnteredConsoleText = false,
                    Text = text
                };
            }

            text = _textUrlFileLoader.ReadTextFile();

            if (text.IsFilled())
            {
                return new InputTextResult
                {
                    HasEnteredConsoleText = false,
                    Text = text
                };
            }

            _displayOutput.WriteResource(
                resourceIdent: "ENTER_TEXT");

            text = _console.ReadLine();
            return new InputTextResult
            {
                HasEnteredConsoleText = text.IsFilled(),
                Text = text
            };
        }
    }
}
