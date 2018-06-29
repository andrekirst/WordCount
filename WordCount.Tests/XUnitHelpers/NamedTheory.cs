using System.Runtime.CompilerServices;
using Xunit;

namespace WordCount.Tests.XUnitHelpers
{
    public class NamedTheoryAttribute : TheoryAttribute
    {
        public NamedTheoryAttribute([CallerMemberName]string name = "")
        {
            base.DisplayName = name;
        }
    }
}
