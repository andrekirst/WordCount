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
                .Setup(expression: m => m.ParseSourceFileParameter())
                .Returns(value: new SourceFileParameter());

            _mockConsole
                .Setup(expression: m => m.ReadLine())
                .Returns(value: "Bla");

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.Equal(expected: "Bla", actual: actual.Text);
        }

        [NamedFact]
        public void TextInputTests_Parameter_argumentsReaderResult_Value_Blub_Expect_Blub()
        {
            _mockSourceFileParameterParser
                .Setup(expression: m => m.ParseSourceFileParameter())
                .Returns(value: new SourceFileParameter
                {
                    IsPresent = true,
                    FileName = It.IsAny<string>()
                });

            _mockTextFileLoader
                .Setup(expression: m => m.ReadTextFile(It.IsAny<string>()))
                .Returns(value: "Blub");

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.Equal(expected: "Blub", actual: actual.Text);
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

        [NamedFact]
        public void TextInputTests_SourceFileParameter_not_present_and_Text_is_not_empty_Expect_HasEnteredText_True()
        {
            _mockSourceFileParameterParser
                .Setup(expression: m => m.ParseSourceFileParameter())
                .Returns(value: new SourceFileParameter { IsPresent = false });

            _mockConsole
                .Setup(expression: m => m.ReadLine())
                .Returns(value: "Bla bla");

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.True(condition: actual.HasEnteredText);
        }

        [NamedFact]
        public void TextInputTests_SourceFileParameter_not_present_and_Text_is_empty_Expect_HasEnteredText_False()
        {
            _mockSourceFileParameterParser
                .Setup(expression: m => m.ParseSourceFileParameter())
                .Returns(value: new SourceFileParameter { IsPresent = false });

            _mockConsole
                .Setup(expression: m => m.ReadLine())
                .Returns(value: string.Empty);

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.False(condition: actual.HasEnteredText);
        }
    }
}
