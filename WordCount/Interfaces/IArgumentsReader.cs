using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCount.Models;

namespace WordCount.Interfaces
{
    public interface IArgumentsReader
    {
        ArgumentsReaderResult ReadArguments(string[] args);
    }
}
