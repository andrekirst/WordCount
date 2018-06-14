using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Interfaces
{
    public interface IStopwordLoader
    {
        List<string> GetStopwords();
    }
}
