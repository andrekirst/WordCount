using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Interfaces
{
    public interface IDisplayOutput
    {
        void Write(string text);

        void WriteLine(string text);
    }
}
