using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCount.Interfaces;

namespace WordCount.Implementations
{
    public class ConsoleDisplayOutput : IDisplayOutput
    {
        public void Write(string text)
        {
            Console.Write(value: text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(value: text);
        }
    }
}
