using System;
using System.Linq;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class IndexParameterParser : IIndexParameterParser
    {
        public IndexParameter ParseIndexParameter(string[] args)
        {
            args = args ?? new string[0];
            return new IndexParameter()
            {
                IsPresent = args.Contains(value: "-index")
            };
        }
    }
}
