using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using Xunit;

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
                .RegisterInstance(instance: _textInput.Object)
                .As<ITextInput>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _wordCountAnalyzer.Object)
                .As<IWordCountAnalyzer>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _wordCountAnalyzerOutput.Object)
                .As<IWordCountAnalyzerOutput>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _stopwordLoader.Object)
                .As<IStopwordLoader>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _argumentsReader.Object)
                .As<IArgumentsReader>()
                .SingleInstance();

            containerBuilder
                .RegisterInstance(instance: _displayOutput.Object)
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
        public void InteractorTests_Call_ReadArguments_Verify_1_Call()
        {
            string[] args = new string[] { "mytext.txt" };

            _systemUnderTest.Execute(args: args);

            _argumentsReader
                .Verify(expression: v => v.ReadArguments(args), times: Times.Once);
        }
    }
}
