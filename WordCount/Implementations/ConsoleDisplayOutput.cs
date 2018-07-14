using System;
using WordCount.Abstractions.Console;
using WordCount.Interfaces;
using WordCount.Interfaces.Language;

namespace WordCount.Implementations
{
    public class ConsoleDisplayOutput : IDisplayOutput
    {
        private readonly IConsole _console;
        private readonly ILanguageResource _languageResource;

        public ConsoleDisplayOutput(
            IConsole console,
            ILanguageResource languageResource)
        {
            _console = console;
            _languageResource = languageResource;
        }

        public void Write(string text)
        {
            _console.Write(text: text);
        }

        public void WriteResource(string resourceIdent, params object[] placeholderValues)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: resourceIdent);
            _console.Write(text: resourceValue, values: placeholderValues);
        }

        public void WriteResourceLine(string resourceIdent, params object[] placeholderValues)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: resourceIdent);
            _console.WriteLine(text: resourceValue, values: placeholderValues);
        }

        public void WriteErrorResourceLine(string resourceIdent, params object[] placeholderValues)
        {
            _console.ForegroundColor = ConsoleColor.Red;
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: resourceIdent);
            _console.WriteLine(text: resourceValue, values: placeholderValues);
            _console.ResetColor();
        }

        public void WriteLine()
        {
            _console.WriteLine();
        }

        public void WriteErrorLine(string errorMessage)
        {
            _console.ForegroundColor = ConsoleColor.Red;
            _console.WriteLine(text: errorMessage);
            _console.ResetColor();
        }

        public void WriteLine(string text)
        {
            _console.WriteLine(text: text);
        }
    }
}
