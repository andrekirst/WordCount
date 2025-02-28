using Moq;
using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class DictionaryParameterParserTests
{
    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_have_no_Dictionary_Parameter_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        DictionaryParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "bla.txt" });

        var actual = sut.ParseDictionaryParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_is_null_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        DictionaryParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(() => null);

        var actual = sut
            .ParseDictionaryParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_is_empty_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        DictionaryParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(Array.Empty<string>());

        var actual = sut
            .ParseDictionaryParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_has_DictionaryParameter_with_no_equal_sign_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        DictionaryParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-dictionary" });

        var actual = sut
            .ParseDictionaryParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_has_DictionaryParameter_Without_File_Expect_IsPresent_False(
        [Frozen] Mock<IEnvironment> environment,
        DictionaryParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-dictionary=" });

        var actual = sut
            .ParseDictionaryParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_has_DictionaryParameter_With_File_bla_txt_Expect_FileName_bla_txt(
        [Frozen] Mock<IEnvironment> environment,
        DictionaryParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-dictionary=bla.txt" });

        var actual = sut.ParseDictionaryParameter();

        actual.Should().NotBeNull();
        actual.FileName.Should().Be("bla.txt");
    }
}