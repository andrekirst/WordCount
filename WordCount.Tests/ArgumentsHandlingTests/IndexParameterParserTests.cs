using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests
{
    public class IndexParameterParserTests
    {
        private readonly Mock<IEnvironment> _mockEnvironment;
        private readonly IndexParameterParser _systemUnderTest;

        public IndexParameterParserTests()
        {
            _mockEnvironment = new Mock<IEnvironment>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockEnvironment.Object)
                .As<IEnvironment>();

            containerBuilder
                .RegisterType<IndexParameterParser>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<IndexParameterParser>();
        }

        [NamedFact]
        public void IndexParameterParserTests_Args_have_no_index_Parameter_Expect_IsPresent_False()
        {
            IndexParameter actual = _systemUnderTest.ParseIndexParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void IndexParameterParserTests_Args_is_null_expect_IsPresent_False()
        {
            IndexParameter actual = _systemUnderTest.ParseIndexParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void IndexParameterParserTests_Args_is_empty_expect_IsPresent_False()
        {
            IndexParameter actual = _systemUnderTest.ParseIndexParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void IndexParameterParserTests_Args_has_index_Parameter_expect_IsPresent_True()
        {
            _mockEnvironment
                .Setup(m => m.GetCommandLineArgs())
                .Returns(new string[] {"-index"});

            IndexParameter actual = _systemUnderTest.ParseIndexParameter();

            Assert.NotNull(@object: actual);
            Assert.True(condition: actual.IsPresent);
        }
    }
}
