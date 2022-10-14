using FluentAssertions;
using WordCount.Extensions;
using Xunit;

namespace WordCount.Tests.StringExtensionsTests;

public class IsNullOrEmptyTests
{
    [Theory]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData(null, true)]
    [InlineData("text", false)]
    public void IsNullOrEmptyTests_Parameter_Test_with_input_Text_and_expected_value(
        string inputText,
        bool expectedValue)
    {
        var actual = inputText.IsNullOrEmpty();
        actual.Should().Be(expectedValue);
    }
}