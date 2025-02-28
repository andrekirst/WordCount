using Moq;
using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class IndexParameterParserTests
{
    [Theory, AutoMoqData]
    public void IndexParameterParserTests_Args_have_no_index_Parameter_Expect_IsPresent_False(
        IndexParameterParser sut)
    {
        var actual = sut.ParseIndexParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void IndexParameterParserTests_Args_is_null_expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        IndexParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(() => null);

        var actual = sut.ParseIndexParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void IndexParameterParserTests_Args_is_empty_expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        IndexParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(Array.Empty<string>());

        var actual = sut.ParseIndexParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void IndexParameterParserTests_Args_has_index_Parameter_expect_IsPresent_True(
        [Frozen] Mock<IEnvironment> environment,
        IndexParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-index" });

        var actual = sut.ParseIndexParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }
}