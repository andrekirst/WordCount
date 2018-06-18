using WordCount.Abstractions.Console;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class ConsoleDisplayOutput : IDisplayOutput
    {
        private readonly IConsole _console;

        public ConsoleDisplayOutput(IConsole console)
        {
            _console = console;
        }

        public void Write(string text)
        {
            _console.Write(text: text);
        }

        public void WriteLine(string text)
        {
            _console.WriteLine(text: text);
        }
    }
}
