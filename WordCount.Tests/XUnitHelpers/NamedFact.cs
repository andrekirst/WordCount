using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WordCount.Tests.XUnitHelpers
{
    public class NamedFactAttribute : FactAttribute
    {
        public NamedFactAttribute([CallerMemberName]string name = "")
        {
            base.DisplayName = name;
        }
    }
}
