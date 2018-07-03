using WordCount.Tests.XUnitHelpers;
using Xunit;
using WordCount.Extensions;

namespace WordCount.Tests.StringExtensionsTests
{
    public class IsFilledTests
    {
        [NamedTheory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData(null, false)]
        [InlineData("text", true)]
        public void IsFilledTests_Parameter_Test_with_input_Text_and_expected_value(
            string inputText,
            bool expectedValue)
        {
            bool actual = inputText.IsFilled();
            Assert.Equal(
                expected: expectedValue,
                actual: actual);
        }
    }
}
