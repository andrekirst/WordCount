using Moq;
using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class SourceFileParameterParserTests
{
    [Theory, AutoMoqData]
    public void SourceFileParameterParserTests_Args_is_empty_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        SourceFileParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(Array.Empty<string>());

        var actual = sut.ParseSourceFileParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void SourceFileParameterParserTests_Args_is_null_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        SourceFileParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(() => null);

        var actual = sut.ParseSourceFileParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void SourceFileParameterParserTests_Args_has_FileName_Expect_Is_Present_True(
        [Frozen] Mock<IEnvironment> environment,
        SourceFileParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "bla.txt" });

        var actual = sut.ParseSourceFileParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void SourceFileParameterParserTests_Args_has_FileName_bla_txt_Expect_Is_FileName_bla_txt(
        [Frozen] Mock<IEnvironment> environment,
        SourceFileParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "bla.txt" });

        var actual = sut.ParseSourceFileParameter();

        actual.Should().NotBeNull();
        actual.FileName.Should().Be("bla.txt");
    }

    [Theory, AutoMoqData]
    public void SourceFileParameterParserTests_Args_has_IndexParameter_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        SourceFileParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-index" });

        var actual = sut.ParseSourceFileParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void SourceFileParameterParserTests_Args_are_lang_pramater_and_file_parameter_expect_is_present_and_filename(
        [Frozen] Mock<IEnvironment> environment,
        SourceFileParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-lang=de", "sample.txt" });

        var actual = sut.ParseSourceFileParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
        // TODO Extra Test
        actual.FileName.Should().Be("sample.txt");
    }
}