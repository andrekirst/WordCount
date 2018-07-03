using System.Runtime.CompilerServices;
using Xunit;

namespace WordCount.Tests.XUnitHelpers
{
    public sealed class NamedFactAttribute : FactAttribute
    {
        public NamedFactAttribute([CallerMemberName]string name = "")
        {
            DisplayName = name;
        }
    }
}
