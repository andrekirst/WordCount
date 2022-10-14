using System;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;

namespace WordCount.Implementations.Output
{
    public class ConsoleDisplayOutput : IDisplayOutput
    {
        private IConsole Console { get; }
        private ILanguageResource LanguageResource { get; }

        public ConsoleDisplayOutput(
            IConsole console,
            ILanguageResource languageResource)
        {
            Console = console;
            LanguageResource = languageResource;
        }

        public void Write(string text) => Console.Write(text);

        public void WriteResource(string resourceIdent, params object[] placeholderValues)
        {
            var resourceValue = LanguageResource.GetResourceStringById(resourceIdent);
            Console.Write(resourceValue, placeholderValues);
        }

        public void WriteResourceLine(string resourceIdent, params object[] placeholderValues)
        {
            var resourceValue = LanguageResource.GetResourceStringById(resourceIdent);
            Console.WriteLine(resourceValue, placeholderValues);
        }

        public void WriteErrorResourceLine(string resourceIdent, params object[] placeholderValues)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var resourceValue = LanguageResource.GetResourceStringById(resourceIdent);
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
}
