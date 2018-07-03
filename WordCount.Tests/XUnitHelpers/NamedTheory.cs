using System.Runtime.CompilerServices;
using Xunit;

namespace WordCount.Tests.XUnitHelpers
{
    public sealed class NamedTheoryAttribute : TheoryAttribute
    {
        public NamedTheoryAttribute([CallerMemberName]string name = "")
        {
            DisplayName = name;
        }
    }
}
