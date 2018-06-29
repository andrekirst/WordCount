using System;
using System.Linq;
using WordCount.Tests.XUnitHelpers;
using WordCount.Extensions;
using Xunit;

namespace WordCount.Tests.RegexStringExtensionsTests
{
    public class IsMatchingRegexTests
    {
        [NamedTheory]
        [InlineData("", "", false)]
        [InlineData(null, "", false)]
        [InlineData(" ", "", false)]
        [InlineData("", null, false)]
        [InlineData(null, null, false)]
        [InlineData(" ", " ", false)]
        [InlineData("asd", "[a-z]{1,}", true)]
        public void IsMatchingRegexTests_Parameterized_Tests_with_input_and_pattern_and_expected_bolean_value(
            string text,
            string pattern,
            bool expectedValue)
        {
            bool actual = text.IsMatchingRegex(pattern: pattern);
            Assert.Equal(
                expected: expectedValue,
                actual: actual);
        }
    }
}
