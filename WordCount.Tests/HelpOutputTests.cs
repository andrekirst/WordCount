using System;
using System.Linq;
using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class HelpOutputTests
    {
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<IHelpParameterParser> _mockHelpParameterParser;
        private readonly HelpOutput _systemUnderTest;

        public HelpOutputTests()
        {
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockHelpParameterParser = new Mock<IHelpParameterParser>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockHelpParameterParser.Object)
                .As<IHelpParameterParser>();

            containerBuilder
                .RegisterType<HelpOutput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<HelpOutput>();
        }

        [NamedFact]
        public void HelpOutputTests_ShowHelpIfRequested_HelpParamater_is_not_present_Expect_False()
        {
            _mockHelpParameterParser
                .Setup(m => m.ParseHelpParameter())
                .Returns(new HelpParameter {IsPresent = false});

            bool actual = _systemUnderTest.ShowHelpIfRequested();

            Assert.False(condition: actual);
        }
    }
}
