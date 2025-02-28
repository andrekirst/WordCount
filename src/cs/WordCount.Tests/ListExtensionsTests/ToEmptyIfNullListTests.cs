using FluentAssertions;
using WordCount.Extensions;
using Xunit;

namespace WordCount.Tests.ListExtensionsTests;

public class ToEmptyIfNullListTests
{
    [Fact]
    public void ToEmptyIfNullListTests_Enumerable_is_null_NullIfEmpty_is_true_expect_empty_list()
    {
        string[] array = null;

        // ReSharper disable once ExpressionIsAlwaysNull
        var actual = array.ToEmptyIfNullList();

        actual.Should().NotBeNull();
    }
}