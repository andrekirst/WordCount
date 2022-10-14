using WordCount.Extensions;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace WordCount.Tests.EnumerableStringExtensionsTests;

public class FirstOfMatchingRegexTests
{
    [Theory, AutoData]
    public void FirstOfMatchingRegexTests_Two_Values_Matching_Only_one_Expect_one(
        List<string> list)
    {
        var actual = list.FirstOfMatchingRegex(list.First());

        actual.Should().Be(list.First());
    }
}