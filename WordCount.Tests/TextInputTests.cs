using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models.Results;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class TextInputTests
    {
        private readonly Mock<IConsole> _mockConsole;
        private readonly Mock<ITextFileLoader> _mockTextFileLoader;
        private readonly Mock<ITextUrlFileLoader> _mockTextUrlFileLoader;
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly TextInput _systemUnderTest;

        public TextInputTests()
        {
            _mockConsole = new Mock<IConsole>();
            _mockTextFileLoader = new Mock<ITextFileLoader>();
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockTextUrlFileLoader = new Mock<ITextUrlFileLoader>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockConsole.Object)
                .As<IConsole>();

            containerBuilder
                .RegisterInstance(instance: _mockTextFileLoader.Object)
                .As<ITextFileLoader>();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockTextUrlFileLoader.Object)
                .As<ITextUrlFileLoader>();

            containerBuilder
                .RegisterType<TextInput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<TextInput>();
        }

        [NamedFact]
        public void TextInputTests_TextFileLoader_returns_bla_Expect_HasEnteredText_false_and_Text_bla()
        {
            _mockTextFileLoader
                .Setup(expression: m => m.ReadTextFile())
                .Returns(value: "bla");

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: "bla", actual: actual.Text);
            Assert.False(condition: actual.HasEnteredConsoleText);
        }

        [NamedFact]
        public void TextInputTests_TextUrlFileLoader_returns_bla_Expect_HasEnteredText_false_and_Text_bla()
        {
            _mockTextUrlFileLoader
                .Setup(expression: m => m.ReadTextFile())
                .Returns(value: "bla");

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: "bla", actual: actual.Text);
            Assert.False(condition: actual.HasEnteredConsoleText);
        }

        [NamedFact]
        public void TextInputTests_ConsoleInput_bla_Expect_EnterText_and_content_bla_and_HasEnteredConsoleText_true()
        {
            _mockConsole
                .Setup(expression: m => m.ReadLine())
                .Returns(value: "bla");

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: "bla", actual: actual.Text);
            Assert.True(condition: actual.HasEnteredConsoleText);

            _mockDisplayOutput
                .Verify(v => v.WriteResourceStringWithValues("ENTER_TEXT"), Times.Once);
        }

        [NamedFact]
        public void TextInputTests_ConsoleInput_empty_Expect_EnterText_and_content_empty_and_HasEnteredConsoleText_false()
        {
            _mockConsole
                .Setup(expression: m => m.ReadLine())
                .Returns(value: string.Empty);

            InputTextResult actual = _systemUnderTest.GetInputText();

            Assert.NotNull(@object: actual);
            Assert.Equal(expected: string.Empty, actual: actual.Text);
            Assert.False(condition: actual.HasEnteredConsoleText);

            _mockDisplayOutput
                .Verify(v => v.WriteResourceStringWithValues("ENTER_TEXT"), Times.Once);
        }
    }
}
