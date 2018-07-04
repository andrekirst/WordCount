using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class TextInputTests
    {
        private readonly Mock<IConsole> _mockConsole;
        private readonly Mock<ITextFileLoader> _mockTextFileLoader;
        private readonly Mock<ISourceFileParameterParser> _mockSourceFileParameterParser;
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly TextInput _systemUnderTest;

        public TextInputTests()
        {
            _mockConsole = new Mock<IConsole>();
            _mockTextFileLoader = new Mock<ITextFileLoader>();
            _mockSourceFileParameterParser = new Mock<ISourceFileParameterParser>();
            _mockDisplayOutput = new Mock<IDisplayOutput>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockConsole.Object)
                .As<IConsole>();

            containerBuilder
                .RegisterInstance(instance: _mockTextFileLoader.Object)
                .As<ITextFileLoader>();

            containerBuilder
                .RegisterInstance(instance: _mockSourceFileParameterParser.Object)
                .As<ISourceFileParameterParser>();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterType<TextInput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<TextInput>();
        }

        [NamedFact]
        public void TextInputTests_Console_ReadLine_Bla_Expect_bla()
        {
            _mockSourceFileParameterParser
                .Setup(m => m.ParseSourceFileParameter())
                .Returns(new SourceFileParameter());

            _mockConsole
                .Setup(expression: m => m.ReadLine())
                .Returns(value: "Bla");

            string actual = _systemUnderTest.GetInputText();

            Assert.Equal(expected: "Bla", actual: actual);
        }

        [NamedFact]
        public void TextInputTests_Parameter_argumentsReaderResult_Value_Blub_Expect_Blub()
        {
            _mockSourceFileParameterParser
                .Setup(m => m.ParseSourceFileParameter())
                .Returns(new SourceFileParameter
                {
                    IsPresent = true,
                    FileName = It.IsAny<string>()
                });

            _mockTextFileLoader
                .Setup(m => m.ReadTextFile(It.IsAny<string>()))
                .Returns(value: "Blub");

            string actual = _systemUnderTest.GetInputText();

            Assert.Equal(expected: "Blub", actual: actual);
        }

        [NamedFact]
        public void TextInputTests_If_Parameter_is_not_present_Then_Show_Enter_Text()
        {
            _mockSourceFileParameterParser
                .Setup(expression: m => m.ParseSourceFileParameter())
                .Returns(value: new SourceFileParameter {IsPresent = false});

            _systemUnderTest.GetInputText();

            _mockDisplayOutput
                .Verify(
                    expression: v => v.Write("Enter text: "),
                    times: Times.Once);
        }
    }
}
