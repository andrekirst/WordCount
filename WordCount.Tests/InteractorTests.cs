using System;
using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class InteractorTests
    {
        private readonly Mock<ITextInput> _mockTextInput;
        private readonly Mock<IWordCountAnalyzer> _mockWordCountAnalyzer;
        private readonly Mock<IWordCountAnalyzerOutput> _mockWordCountAnalyzerOutput;
        private readonly Mock<IArgumentsReader> _mockArgumentsReader;
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<IIndexOutput> _mockIndexOutput;
        private readonly Interactor _systemUnderTest;

        public InteractorTests()
        {
            _mockTextInput = new Mock<ITextInput>();
            _mockWordCountAnalyzer = new Mock<IWordCountAnalyzer>();
            _mockWordCountAnalyzerOutput = new Mock<IWordCountAnalyzerOutput>();
            _mockArgumentsReader = new Mock<IArgumentsReader>();
            _mockDisplayOutput = new Mock<IDisplayOutput>();
            _mockIndexOutput = new Mock<IIndexOutput>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockIndexOutput.Object)
                .As<IIndexOutput>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _mockTextInput.Object)
                .As<ITextInput>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _mockWordCountAnalyzer.Object)
                .As<IWordCountAnalyzer>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _mockWordCountAnalyzerOutput.Object)
                .As<IWordCountAnalyzerOutput>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _mockArgumentsReader.Object)
                .As<IArgumentsReader>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _mockDisplayOutput.Object)
                .As<IDisplayOutput>()
                .SingleInstance();

            containerBuilder
                .RegisterType<Interactor>()
                .SingleInstance();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<Interactor>();
        }

        [Fact]
        public void InteractorTests_TextInput_Throws_Error_Expect_ReturnCode_1()
        {
            _mockTextInput
                .Setup(m => m.GetInputText(It.IsAny<ArgumentsReaderResult>()))
                .Throws<Exception>();

            _mockArgumentsReader
                .Setup(m => m.ReadArguments(It.IsAny<string[]>()))
                .Returns(value: new ArgumentsReaderResult()
                {
                    IsSourceTextFileParameterPresent = true
                });

            int actual = _systemUnderTest.Execute(args: It.IsAny<string[]>());

            Assert.Equal(expected: 1, actual: actual);
        }

        [Fact]
        public void InteractorTests_Call_ReadArguments_Verify_1_Call()
        {
            string[] args = { "mytext.txt" };

            _mockArgumentsReader
                .Setup(expression: m => m.ReadArguments(args))
                .Returns(value: new ArgumentsReaderResult()
                {
                    IsSourceTextFileParameterPresent = true
                });

            _systemUnderTest.Execute(args: args);

            _mockArgumentsReader
                .Verify(expression: v => v.ReadArguments(args), times: Times.Once);
        }

        [Fact]
        public void InteractorTests_ArgumentReader_no_file_is_present_expect_DisplayOutput_Enter_Text()
        {
            _mockArgumentsReader
                .Setup(expression: m => m.ReadArguments(It.IsAny<string[]>()))
                .Returns(value: new ArgumentsReaderResult()
                {
                    IsSourceTextFileParameterPresent = false
                });

            _systemUnderTest.Execute(args: It.IsAny<string[]>());

            _mockDisplayOutput
                .Verify(
                    expression: v => v.Write("Enter text: "),
                    times: Times.Once);
        }

        [Fact]
        public void InteractorTests_ArgumentReader_file_is_present_expect_not_DisplayOutput_Enter_Text()
        {
            _mockArgumentsReader
                .Setup(m => m.ReadArguments(It.IsAny<string[]>()))
                .Returns(new ArgumentsReaderResult()
                {
                    IsSourceTextFileParameterPresent = true
                });

            _systemUnderTest.Execute(args: It.IsAny<string[]>());

            _mockDisplayOutput
                .Verify(
                    expression: v => v.Write("Enter text: "),
                    times: Times.Never);
        }
    }
}
