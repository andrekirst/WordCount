using System;
using WordCount.Abstractions.Console;
using WordCount.Interfaces;

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

        public void WriteResourceStringWithValues(string resourceIdent, params object[] values)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: resourceIdent);
            _console.Write(text: resourceValue, values: values);
        }

        public void WriteResourceStringWithValuesLine(string resourceIdent, params object[] values)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: resourceIdent);
            _console.WriteLine(text: resourceValue, values: values);
        }

        public void WriteErrorResourceStringWithValuesLine(string resourceIdent, params object[] values)
        {
            _console.ForegroundColor = ConsoleColor.Red;
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: resourceIdent);
            _console.WriteLine(text: resourceValue, values: values);
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
