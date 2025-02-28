using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class HelpParameterParserTests
{
    [Theory, AutoMoqData]
    public void HelpParameterParserTests_args_is_empty_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        HelpParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(Array.Empty<string>);

        var actual = sut.ParseHelpParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void HelpParameterParserTests_args_is_null_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        HelpParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(() => null);

        var actual = sut.ParseHelpParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void HelpParameterParserTests_args_have_sourcefile_but_no_help_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        HelpParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "mytext.txt" });

        var actual = sut.ParseHelpParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void HelpParameterParserTests_args_have_help_Expect_IsPresent_True(
        [Frozen] Mock<IEnvironment> environment,
        HelpParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-help" });

        var actual = sut.ParseHelpParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void HelpParameterParserTests_args_have_h_Expect_IsPresent_True(
        [Frozen] Mock<IEnvironment> environment,
        HelpParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-h" });

        var actual = sut.ParseHelpParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }
}