using System;

namespace WordCount.Abstractions.SystemAbstractions
{
    public interface IConsole
    {
        string ReadLine();

        void Write(string text);

        void Write(string text, params object[] values);

        void WriteLine();

        void WriteLine(string text);

        void WriteLine(string text, params object[] values);

        ConsoleColor ForegroundColor { get; set; }

        void ResetColor();
    }
}
