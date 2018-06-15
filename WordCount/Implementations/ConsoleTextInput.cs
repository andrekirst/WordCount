using System;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class ConsoleTextInput : ITextInput
    {
        private readonly IDisplayOutput _displayOutput;

        public ConsoleTextInput(IDisplayOutput displayOutput)
        {
            _displayOutput = displayOutput;
        }

        public string GetInputText(ArgumentsReaderResult argumentsReaderResult)
        {
            _displayOutput.Write(text: "Enter text: ");
            return Console.ReadLine();
        }
    }
}
