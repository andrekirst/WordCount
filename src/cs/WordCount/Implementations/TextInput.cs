using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;

namespace WordCount.Implementations;

public class TextInput(
    IConsole console,
    ITextFileLoader textFileLoader,
    ITextUrlFileLoader textUrlFileLoader,
    IDisplayOutput displayOutput) : ITextInput
{
    public InputTextResult GetInputText()
    {
        var text = textFileLoader.ReadTextFile();
        if (text.IsFilled())
        {
            return new InputTextResult
            {
                HasEnteredConsoleText = false,
                Text = text
            };
        }

        text = textUrlFileLoader.ReadTextFile();

        if (text.IsFilled())
        {
            return new InputTextResult
            {
                HasEnteredConsoleText = false,
                Text = text
            };
        }

        displayOutput.WriteResource("ENTER_TEXT");

        text = console.ReadLine();
        return new InputTextResult
        {
            HasEnteredConsoleText = text.IsFilled(),
            Text = text
        };
    }
}
