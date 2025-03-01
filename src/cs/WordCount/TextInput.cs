using System;
using System.Threading.Tasks;
using WordCount.Extensions;

namespace WordCount;

public interface ITextInput
{
    Task<InputTextResult> GetInputText();
}

public class TextInput(
    ITextFileLoader textFileLoader,
    ITextUrlFileLoader textUrlFileLoader,
    IDisplayOutput displayOutput) : ITextInput
{
    public async Task<InputTextResult> GetInputText()
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

        text = await textUrlFileLoader.ReadTextFile();

        if (text.IsFilled())
        {
            return new InputTextResult
            {
                HasEnteredConsoleText = false,
                Text = text
            };
        }

        displayOutput.WriteResource("ENTER_TEXT");

        text = Console.ReadLine();
        return new InputTextResult
        {
            HasEnteredConsoleText = text.IsFilled(),
            Text = text
        };
    }
}

public class InputTextResult
{
    public string Text { get; set; }

    public bool HasEnteredConsoleText { get; set; }
}
