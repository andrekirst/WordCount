using Autofac;
using Moq;
using System;
using System.Linq;
using WordCount.Implementations;
using WordCount.Interfaces;
using Xunit;
using WordCount.Extensions;

namespace WordCount.Tests
{
    public class InteractorTests
    {
        private readonly Mock<ITextInput> _textInput;
        private readonly Mock<IWordCountAnalyzer> _wordCountAnalyzer;
        private readonly Mock<IWordCountAnalyzerOutput> _wordCountAnalyzerOutput;
        private readonly Mock<IStopwordLoader> _stopwordLoader;
        private readonly Mock<IArgumentsReader> _argumentsReader;
        private readonly Mock<IDisplayOutput> _displayOutput;
        private readonly Interactor _systemUnderTest;

        public InteractorTests()
        {
            _textInput = new Mock<ITextInput>();
            _wordCountAnalyzer = new Mock<IWordCountAnalyzer>();
            _wordCountAnalyzerOutput = new Mock<IWordCountAnalyzerOutput>();
            _stopwordLoader = new Mock<IStopwordLoader>();
            _argumentsReader = new Mock<IArgumentsReader>();
            _displayOutput = new Mock<IDisplayOutput>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(_textInput.Object)
                .As<ITextInput>();

            containerBuilder
                .RegisterInstance(_wordCountAnalyzer.Object)
                .As<IWordCountAnalyzer>();

            containerBuilder
                .RegisterInstance(_wordCountAnalyzerOutput.Object)
                .As<IWordCountAnalyzerOutput>();

            containerBuilder
                .RegisterInstance(_stopwordLoader.Object)
                .As<IStopwordLoader>();

            containerBuilder
                .RegisterInstance(_argumentsReader.Object)
                .As<IArgumentsReader>();

            containerBuilder
                .RegisterInstance(_displayOutput.Object)
                .As<IDisplayOutput>();

            containerBuilder
                .RegisterType<Interactor>();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<Interactor>();
        }

        [Fact]
        public void InteractorTests_Call_ReadArguments_Verify_1_Call()
        {
            string[] args = new string[] { "mytext.txt" };

            _systemUnderTest.Execute(args: args);

            _argumentsReader
                .Verify(v => v.ReadArguments(args), Times.Once);
        }
    }
}
