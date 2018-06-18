using System.IO.Abstractions;
using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class TextInputTests
    {
        private readonly Mock<IConsole> _mockConsole;
        private readonly Mock<IFileSystem> _mockFileSystem;
        private readonly TextInput _systemUnderTest;

        public TextInputTests()
        {
            _mockConsole = new Mock<IConsole>();
            _mockFileSystem = new Mock<IFileSystem>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockConsole.Object)
                .As<IConsole>();

            containerBuilder
                .RegisterInstance(instance: _mockFileSystem.Object)
                .As<IFileSystem>();

            containerBuilder
                .RegisterType<TextInput>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<TextInput>();
        }

        [Fact]
        public void TextInputTests_Console_ReadLine_Bla_Expect_bla()
        {
            _mockConsole
                .Setup(expression: m => m.ReadLine())
                .Returns(value: "Bla");

            string actual = _systemUnderTest.GetInputText(argumentsReaderResult: null);

            Assert.Equal(expected: "Bla", actual: actual);
        }

        [Fact]
        public void TextInputTests_Parameter_argumentsReaderResult_Value_Blub_Expect_Blub()
        {
            _mockFileSystem
                .Setup(m => m.File.ReadAllText(It.IsAny<string>()))
                .Returns(value: "Blub");

            string actual = _systemUnderTest.GetInputText(
                argumentsReaderResult: new ArgumentsReaderResult(
                    sourceTextFile: It.IsAny<string>(),
                    isSourceTextFilePresent: true));

            Assert.Equal(expected: "Blub", actual: actual);
        }
    }
}
