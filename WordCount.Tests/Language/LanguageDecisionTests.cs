using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.Language;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using Xunit;

namespace WordCount.Tests.Language;

public class LanguageDecisionTests
{
    [Theory, AutoMoqData]
    public void LanguageDecisionTests_Parameter_is_present_and_language_de_expect_de(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        LanguageDecision sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { IsPresent = true, Language = "de" });

        var actual = sut.DecideLanguage();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("de");
    }

    [Theory, AutoMoqData]
    public void LanguageDecisionTests_Parameter_is_not_present_and_app_settings_has_no_value_expect_en(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IAppSettingsReader> appSettingsReader,
        LanguageDecision sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { IsPresent = false });

        appSettingsReader
            .SetupGet(m => m.DefaultLanguage)
            .Returns(() => null);

        var actual = sut.DecideLanguage();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("en");
    }

    [Theory, AutoMoqData]
    public void LanguageDecisionTests_Parameter_is_not_present_and_app_settings_has_de_expect_de(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IAppSettingsReader> appSettingsReader,
        LanguageDecision sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { IsPresent = false });

        appSettingsReader
            .SetupGet(m => m.DefaultLanguage)
            .Returns("de");

        var actual = sut.DecideLanguage();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("de");
    }

    [Theory, AutoMoqData]
    public void LanguageDecisionTests_Parameter_is_present_and_has_language_it_expect_en_and_console_not_supported_language(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IConsole> console,
        LanguageDecision sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { IsPresent = true, Language = "it" });

        var actual = sut.DecideLanguage();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("en");
            
        console.Verify(v => v.WriteLine("Language \"it\" not supported."), Times.Once);
    }

    [Theory, AutoMoqData]
    public void LanguageDecisionTests_appsetting_has_default_language_it_expect_en_and_console_not_supported_language(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IAppSettingsReader> appSettingsReader,
        [Frozen] Mock<IConsole> console,
        LanguageDecision sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { IsPresent = false });

        appSettingsReader
            .SetupGet(m => m.DefaultLanguage)
            .Returns("it");

        var actual = sut.DecideLanguage();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("en");

        console.Verify(v => v.WriteLine("Language \"it\" not supported."), Times.Once);
    }

    [Theory, AutoMoqData]
    public void LanguageDecisionTests_parameter_has_it_appsetting_has_es_expect_en_and_console_not_supported_language_it(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IAppSettingsReader> appSettingsReader,
        [Frozen] Mock<IConsole> console,
        LanguageDecision sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { IsPresent = true, Language = "it" });

        appSettingsReader
            .SetupGet(m => m.DefaultLanguage)
            .Returns("es");

        var actual = sut.DecideLanguage();

        actual.Should().NotBeNull();
        actual.Language.Should().Be("en");

        console.Verify(v => v.WriteLine("Language \"it\" not supported."), Times.Once);
    }

    [Theory, AutoMoqData]
    public void LanguageDecisionTests_CachingTest(
        [Frozen] Mock<ILanguageParameterParser> languageParameterParser,
        [Frozen] Mock<IAppSettingsReader> appSettingsReader,
        LanguageDecision sut)
    {
        languageParameterParser
            .Setup(m => m.ParseLanguageParameter())
            .Returns(new LanguageParameter { IsPresent = false });

        appSettingsReader
            .SetupGet(m => m.DefaultLanguage)
            .Returns("de");

        var actual = sut.DecideLanguage();

        actual.Should().NotBeNull();
        Assert.Equal("de", actual.Language);

        actual = sut.DecideLanguage();
        actual.Should().NotBeNull();
        actual.Language.Should().Be("de");

        appSettingsReader.VerifyGet(v => v.DefaultLanguage, Times.Once);
        languageParameterParser.Verify(v => v.ParseLanguageParameter(), Times.Once);
    }
}