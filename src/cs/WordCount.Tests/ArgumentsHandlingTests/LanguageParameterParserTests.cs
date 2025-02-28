using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class LanguageParameterParserTests
{
    [Theory, AutoMoqData]
    public void LanguageParameterParserTests_args_is_null_expect_ispresent_false(
        [Frozen] Mock<IEnvironment> environment,
        LanguageParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(() => null);

        var actual = sut.ParseLanguageParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void LanguageParameterParserTests_args_is_empty_expect_ispresent_false(
        [Frozen] Mock<IEnvironment> environment,
        LanguageParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(Array.Empty<string>());

        var actual = sut.ParseLanguageParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void LanguageParameterParserTests_args_has_valid_language_parameter_expect_ispresent_true(
        [Frozen] Mock<IEnvironment> environment,
        LanguageParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-lang=de" });

        var actual = sut.ParseLanguageParameter();

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public void LanguageParameterParserTests_args_has_valid_de_language_parameter_expect_language_de(
        [Frozen] Mock<IEnvironment> environment,
        LanguageParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-lang=de" });

        var actual = sut.ParseLanguageParameter();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("de");
    }

    [Theory, AutoMoqData]
    public void LanguageParameterParserTests_Double_Test_FromCache(
        [Frozen] Mock<IEnvironment> environment,
        LanguageParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(new[] { "-lang=it" });

        var actual = sut.ParseLanguageParameter();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("it");

        actual = sut.ParseLanguageParameter();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("it");

        environment.Verify(v => v.GetCommandLineArgs(), Times.Once);
    }

    [Theory, AutoMoqData]
    public void LanguageParameterParserTests_parameter_is_not_present_expect_language_empty(
        [Frozen] Mock<IEnvironment> environment,
        LanguageParameterParser sut)
    {
        environment
            .Setup(m => m.GetCommandLineArgs())
            .Returns(Array.Empty<string>());

        var actual = sut.ParseLanguageParameter();

        actual.Should().NotBeNull();
        actual.Language.Should().BeEmpty();
    }
}