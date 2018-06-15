using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class ConsoleParameterArgumentsReader : IArgumentsReader
    {
        public ArgumentsReaderResult ReadArguments(string[] args)
        {
            if (args == null || !args.Any())
            {
                return null;
            }

            return new ArgumentsReaderResult()
            {
                SourceTextFile = args[0]
            };
        }
    }
}
