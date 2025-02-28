using System;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.SystemAbstractions
{
    [ExcludeFromCodeCoverage]
    public class Console : IConsole
    {
        public void WriteLine(string text, params object[] values) => System.Console.WriteLine(text, values);

        public ConsoleColor ForegroundColor
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        public string ReadLine() => System.Console.ReadLine();

        public void ResetColor() => System.Console.ResetColor();

        public void Write(string text) => System.Console.Write(text);

        public void Write(string text, params object[] values) => System.Console.Write(text, values);

        public void WriteLine() => System.Console.WriteLine();

        public void WriteLine(string text) => System.Console.WriteLine(text);
    }
}
