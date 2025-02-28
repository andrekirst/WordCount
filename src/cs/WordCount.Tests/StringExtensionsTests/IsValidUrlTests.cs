using FluentAssertions;
using WordCount.Extensions;
using Xunit;

namespace WordCount.Tests.StringExtensionsTests;

public class IsValidUrlTests
{
    [Theory]
    [InlineData("googlede", false)]
    [InlineData("google.de", false)]
    [InlineData("http://google.de", true)]
    public void IsValidUrlTests_ParameterTest_with_Uri_and_expected_Value(
        string uri,
        bool expectedValue)
    {
        var actual = uri.IsValidUrl();
        actual.Should().Be(expectedValue);
    }
}