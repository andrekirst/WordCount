using System;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;

namespace WordCount.Implementations.Output;

public class ConsoleDisplayOutput(
    IConsole console,
    ILanguageResource languageResource) : IDisplayOutput
{
    public void Write(string text) => console.Write(text);

    public void WriteResource(string resourceIdent, params object[] placeholderValues)
    {
        var resourceValue = languageResource.GetResourceStringById(resourceIdent);
        console.Write(resourceValue, placeholderValues);
    }

    public void WriteResourceLine(string resourceIdent, params object[] placeholderValues)
    {
        var resourceValue = languageResource.GetResourceStringById(resourceIdent);
        console.WriteLine(resourceValue, placeholderValues);
    }

    public void WriteErrorResourceLine(string resourceIdent, params object[] placeholderValues)
    {
        console.ForegroundColor = ConsoleColor.Red;
        var resourceValue = languageResource.GetResourceStringById(resourceIdent);
        console.WriteLine(resourceValue, placeholderValues);
        console.ResetColor();
    }

    public void WriteLine() => console.WriteLine();

    public void WriteErrorLine(string errorMessage)
    {
        console.ForegroundColor = ConsoleColor.Red;
        console.WriteLine(errorMessage);
        console.ResetColor();
    }

    public void WriteLine(string text) => console.WriteLine(text);
}
