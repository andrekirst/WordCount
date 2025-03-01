using System;

namespace WordCount;

public class ConsoleDisplayOutput(ILanguageResource languageResource) : IDisplayOutput
{
    public void Write(string text) => Console.Write(text);

    public void WriteResource(string resourceIdent, params object[] placeholderValues)
    {
        var resourceValue = languageResource.GetResourceStringById(resourceIdent);
        Console.Write(resourceValue, placeholderValues);
    }

    public void WriteResourceLine(string resourceIdent, params object[] placeholderValues)
    {
        var resourceValue = languageResource.GetResourceStringById(resourceIdent);
        Console.WriteLine(resourceValue, placeholderValues);
    }

    public void WriteErrorResourceLine(string resourceIdent, params object[] placeholderValues)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        var resourceValue = languageResource.GetResourceStringById(resourceIdent);
        Console.WriteLine(resourceValue, placeholderValues);
        Console.ResetColor();
    }

    public void WriteLine() => Console.WriteLine();

    public void WriteErrorLine(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
    }

    public void WriteLine(string text) => Console.WriteLine(text);
}
