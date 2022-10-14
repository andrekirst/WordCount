using Moq;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Implementations;
using WordCount.Interfaces;
using Xunit;

namespace WordCount.Tests;

public class StopwordRemoverTests
{
    [Theory, AutoMoqData]
    public void StopwordRemoverTests_Analyze_Stopword_a_Mary_has_a_little_lamb_Expect_4_Words_with_Stopword_a(
        [Frozen] Mock<IStopwordLoader> stopwordLoader,
        StopwordRemover sut)
    {
        var values = new List<string> { "Mary", "had", "a", "little", "lamb" };

        stopwordLoader
            .Setup(m => m.GetStopwords())
            .Returns(new List<string> { "a" });

        var actual = sut.RemoveStopwords(values);

        var expected = new List<string> { "Mary", "had", "little", "lamb" };

        actual.Should().NotBeNull();
        actual.Words.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoMoqData]
    public void StopwordRemoverTests_Analyze_Stopword_a_Mary_has_a_little_lamb_Expect_5_Words_without_Stopwords(
        [Frozen] Mock<IStopwordLoader> stopwordLoader,
        StopwordRemover sut)
    {
        var values = new List<string> { "Mary", "had", "a", "little", "lamb" };

        stopwordLoader
            .Setup(m => m.GetStopwords())
            .Returns(new List<string>());

        var expected = new List<string> { "Mary", "had", "a", "little", "lamb" };

        var actual = sut.RemoveStopwords(values);

        actual.Should().NotBeNull();
        actual.Words.Should().NotBeNullOrEmpty();
        actual.Words.Should().BeEquivalentTo(expected);
    }
}