using System;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class ConsoleTextInput : ITextInput
    {
        public string GetInputText()
        {
            return Console.ReadLine();
        }
    }
}
