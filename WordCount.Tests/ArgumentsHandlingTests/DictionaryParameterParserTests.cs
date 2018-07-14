using Autofac;
using Moq;
using System;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests
{
    public class DictionaryParameterParserTests
    {
        private readonly Mock<IEnvironment> _mockEnvironment;
        private readonly DictionaryParameterParser _systemUnderTest;

        public DictionaryParameterParserTests()
        {
            _mockEnvironment = new Mock<IEnvironment>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockEnvironment.Object)
                .As<IEnvironment>();

            containerBuilder
                .RegisterType<DictionaryParameterParser>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<DictionaryParameterParser>();
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_have_no_Dictionary_Parameter_Expect_IsPresent_False()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: new[] {"bla.txt"});

            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_is_null_Expect_IsPresent_False()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: null);

            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_is_empty_Expect_IsPresent_False()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: Array.Empty<string>());

            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_has_DictionaryParameter_with_no_equal_sign_Expect_IsPresent_False()
        {
            _mockEnvironment
                .Setup(m => m.GetCommandLineArgs())
                .Returns(new[] {"-dictionary"});

            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_has_DictionaryParameter_Without_File_Expect_IsPresent_False()
        {
            _mockEnvironment
                .Setup(m => m.GetCommandLineArgs())
                .Returns(new[] { "-dictionary=" });

            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_has_DictionaryParameter_With_File_bla_txt_Expect_FileName_bla_txt()
        {
            _mockEnvironment
                .Setup(m => m.GetCommandLineArgs())
                .Returns(new[] { "-dictionary=bla.txt" });

            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: "bla.txt", actual: actual.FileName);
        }
    }
}
