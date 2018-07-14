using System;
using Autofac;
using Moq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests
{
    public class LanguageParameterParserTests
    {
        private readonly Mock<IEnvironment> _mockEnvironment;
        private readonly Mock<IConsole> _mockConsole;
        private readonly LanguageParameterParser _systemUnderTest;

        public LanguageParameterParserTests()
        {
            _mockEnvironment = new Mock<IEnvironment>();
            _mockConsole = new Mock<IConsole>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockEnvironment.Object)
                .As<IEnvironment>();

            containerBuilder
                .RegisterInstance(instance: _mockConsole.Object)
                .As<IConsole>();

            containerBuilder
                .RegisterType<LanguageParameterParser>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<LanguageParameterParser>();
        }

        [NamedFact]
        public void LanguageParameterParserTests_args_is_null_expect_ispresent_false()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: null);

            LanguageParameter actual = _systemUnderTest.ParseLanguageParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void LanguageParameterParserTests_args_is_empty_expect_ispresent_false()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: Array.Empty<string>());

            LanguageParameter actual = _systemUnderTest.ParseLanguageParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void LanguageParameterParserTests_args_has_valid_language_parameter_expect_ispresent_true()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: new[] {"-lang=de"});

            LanguageParameter actual = _systemUnderTest.ParseLanguageParameter();

            Assert.NotNull(@object: actual);
            Assert.True(condition: actual.IsPresent);
        }

        [NamedFact]
        public void LanguageParameterParserTests_args_has_valid_de_language_parameter_expect_language_de()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: new[] { "-lang=de" });

            LanguageParameter actual = _systemUnderTest.ParseLanguageParameter();

            Assert.NotNull(@object: actual);
            Assert.Equal(
                expected: "de",
                actual: actual.Language);
        }

        [NamedFact]
        public void LanguageParameterParserTests_Double_Test_FromCache()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: new[] { "-lang=it" });

            LanguageParameter actual = _systemUnderTest.ParseLanguageParameter();

            Assert.NotNull(@object: actual);
            Assert.Equal(
                expected: "it",
                actual: actual.Language);

            actual = _systemUnderTest.ParseLanguageParameter();

            Assert.NotNull(@object: actual);
            Assert.Equal(
                expected: "it",
                actual: actual.Language);

            _mockEnvironment
                .Verify(v => v.GetCommandLineArgs(), Times.Once);
        }

        [NamedFact]
        public void LanguageParameterParserTests_parameter_is_not_present_expect_language_empty()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: Array.Empty<string>());

            LanguageParameter actual = _systemUnderTest.ParseLanguageParameter();

            Assert.NotNull(@object: actual);
            Assert.Equal(
                expected: string.Empty,
                actual: actual.Language);
        }
    }
}
