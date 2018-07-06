using Autofac;
using Moq;
using WordCount.Abstractions.Reflection;
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
        private readonly Mock<IAssembly> _mockAssembly;
        private readonly HelpOutput _systemUnderTest;

        public HelpOutputTests()
        {
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockHelpParameterParser = new Mock<IHelpParameterParser>();
            _mockAssembly = new Mock<IAssembly>();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockHelpParameterParser.Object)
                .As<IHelpParameterParser>();

            containerBuilder
                .RegisterInstance(instance: _mockAssembly.Object)
                .As<IAssembly>();

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

        [NamedFact]
        public void HelpOutputTests_If_HelpParameter_Present_Expect_Outputs()
        {
            _mockHelpParameterParser
                .Setup(m => m.ParseHelpParameter())
                .Returns(new HelpParameter {IsPresent = true});

            _mockAssembly
                .SetupGet(m => m.Name)
                .Returns("WordCount");

            _mockAssembly
                .SetupGet(m => m.Version)
                .Returns("1.2.3.4");

            _systemUnderTest.ShowHelpIfRequested();

            _mockDisplayOutput
                .Verify(v => v.WriteLine("WordCount - 1.2.3.4"), Times.Once);

            _mockDisplayOutput
                .Verify(v => v.WriteLine(""), Times.Once);

            _mockDisplayOutput
                .Verify(v => v.WriteLine("-h | -help : Display this help"), Times.Once);
            _mockDisplayOutput
                .Verify(v => v.WriteLine("-index : Display the index of the analyzed Text"), Times.Once);
            _mockDisplayOutput
                .Verify(v => v.WriteLine("-dictionary=file : Uses the dictionary with the given file"), Times.Once);
            _mockDisplayOutput
                .Verify(v => v.WriteLine("-stopwordlist=file : Uses the stopword with the given file. Default: stopword.txt"), Times.Once);
        }

        [NamedFact]
        public void HelpOutputTests_If_HelpParameter_not_present_expect_no_Outputs()
        {
            _mockHelpParameterParser
                .Setup(m => m.ParseHelpParameter())
                .Returns(new HelpParameter { IsPresent = false });

            _systemUnderTest.ShowHelpIfRequested();

            _mockDisplayOutput
                .Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
        }
    }
}
