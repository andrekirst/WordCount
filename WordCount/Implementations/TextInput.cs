using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    public class TextInput : ITextInput
    {
        private IConsole Console { get; }
        private ITextFileLoader TextFileLoader { get; }
        private ITextUrlFileLoader TextUrlFileLoader { get; }
        private IDisplayOutput DisplayOutput { get; }

        public TextInput(
            IConsole console,
            ITextFileLoader textFileLoader,
            ITextUrlFileLoader textUrlFileLoader,
            IDisplayOutput displayOutput)
        {
            Console = console;
            TextFileLoader = textFileLoader;
            TextUrlFileLoader = textUrlFileLoader;
            DisplayOutput = displayOutput;
        }

        public InputTextResult GetInputText()
        {
            string text = TextFileLoader.ReadTextFile();
            if (text.IsFilled())
            {
                return new InputTextResult
                {
                    HasEnteredConsoleText = false,
                    Text = text
                };
            }

            text = TextUrlFileLoader.ReadTextFile();

            if (text.IsFilled())
            {
                return new InputTextResult
                {
                    HasEnteredConsoleText = false,
                    Text = text
                };
            }

            DisplayOutput.WriteResource(
                resourceIdent: "ENTER_TEXT");

            text = Console.ReadLine();
            return new InputTextResult
            {
                HasEnteredConsoleText = text.IsFilled(),
                Text = text
            };
        }
    }
}
