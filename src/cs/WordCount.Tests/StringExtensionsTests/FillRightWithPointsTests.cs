using FluentAssertions;
using Xunit;
using WordCount.Extensions;

namespace WordCount.Tests.StringExtensionsTests;

public class FillRightWithPointsTests
{
    [Fact]
    public void FillRightWithPointsTests_Text_bla_maxLength_10_Expect_Bla_and_7_Points()
    {
        var actual = "Bla".FillRightWithPoints(10);
        const string expected = "Bla.......";

        actual.Should().Be(expected);
    }
}