using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using WordCount.Extensions;

namespace WordCount.Tests.RegexStringExtensionsTests;

public class SplitByRegexTests
{
    [Fact]
    public void SplitByRegexTests_Pattern_a_zA_Z_text_Hello_World_Expect_2_Words_Hello_and_World()
    {
        const string regexPattern = @"[a-zA-Z]{1,}";

        const string text = "Hello World";

        var actual = text.SplitByRegex(regexPattern);

        var expected = new List<string> {"Hello", "World"};

        actual.Should().BeEquivalentTo(expected);
    }
}