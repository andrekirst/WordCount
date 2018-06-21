﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.Console
{
    [ExcludeFromCodeCoverage]
    public class Console : IConsole
    {
        public ConsoleColor ForegroundColor { get => System.Console.ForegroundColor; set => System.Console.ForegroundColor = value; }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public void ResetColor()
        {
            System.Console.ResetColor();
        }

        public void Write(string text)
        {
            System.Console.Write(value: text);
        }

        public void WriteLine(string text)
        {
            System.Console.WriteLine(value: text);
        }
    }
}
