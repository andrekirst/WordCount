using FluentAssertions;
using Xunit;
using WordCount.Extensions;

namespace WordCount.Tests.StringExtensionsTests;

public class IsFilledTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData(null, false)]
    [InlineData("text", true)]
    public void IsFilledTests_Parameter_Test_with_input_Text_and_expected_value(
        string inputText,
        bool expectedValue)
    {
        var actual = inputText.IsFilled();
        actual.Should().Be(expectedValue);
    }
}