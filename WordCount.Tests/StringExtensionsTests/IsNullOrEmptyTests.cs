using WordCount.Extensions;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.StringExtensionsTests
{
    public class IsNullOrEmptyTests
    {
        [NamedTheory]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData(null, true)]
        [InlineData("text", false)]
        public void IsNullOrEmptyTests_Parameter_Test_with_input_Text_and_expected_value(
            string inputText,
            bool expectedValue)
        {
            bool actual = inputText.IsNullOrEmpty();
            Assert.Equal(
                expected: expectedValue,
                actual: actual);
        }
    }
}
