using System;
using System.Linq;
using Autofac;
using Moq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests
{
    public class DisplayParameterParserTests
    {
        private readonly Mock<IEnvironment> _mockEnvironment;
        private readonly DisplayParameterParser _systemUnderTest;

        public DisplayParameterParserTests()
        {
            _mockEnvironment = new Mock<IEnvironment>();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(instance: _mockEnvironment.Object)
                .As<IEnvironment>();

            containerBuilder
                .RegisterType<DisplayParameterParser>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<DisplayParameterParser>();
        }


        [NamedFact]
        public void DisplayParameterParserTests_Args_have_no_index_Parameter_Expect_IsPresent_False()
        {
            DisplayParameter actual = _systemUnderTest.ParseDisplayParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DisplayParameterParserTests_Args_is_null_expect_IsPresent_False()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: null);

            DisplayParameter actual = _systemUnderTest.ParseDisplayParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DisplayParameterParserTests_Args_is_empty_expect_IsPresent_False()
        {
            _mockEnvironment
                .Setup(expression: m => m.GetCommandLineArgs())
                .Returns(value: Array.Empty<string>());

            DisplayParameter actual = _systemUnderTest.ParseDisplayParameter();

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DisplayParameterParserTests_Args_has_display_Parameter_expect_IsPresent_True()
        {
            _mockEnvironment
                .Setup(m => m.GetCommandLineArgs())
                .Returns(new[] { "-display" });

            DisplayParameter actual = _systemUnderTest.ParseDisplayParameter();

            Assert.NotNull(@object: actual);
            Assert.True(condition: actual.IsPresent);
        }
    }
}
