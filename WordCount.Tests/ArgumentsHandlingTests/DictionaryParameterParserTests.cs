using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models;
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
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_is_null_Expect_IsPresent_False()
        {
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_is_empty_Expect_IsPresent_False()
        {
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
                .Returns(new string[] {"-dictionary"});

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
                .Returns(new string[] { "-dictionary=" });

            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }
    }
}
