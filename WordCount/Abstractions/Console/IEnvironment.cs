using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Abstractions.Console
{
    public interface IEnvironment
    {
        string[] GetCommandLineArgs();
    }
}
