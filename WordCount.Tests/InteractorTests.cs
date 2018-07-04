using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;

namespace WordCount.Tests
{
    public class InteractorTests
    {
        private readonly Mock<ITextInput> _mockTextInput;
        private readonly Mock<IWordCountAnalyzer> _mockWordCountAnalyzer;
        private readonly Mock<IWordCountAnalyzerOutput> _mockWordCountAnalyzerOutput;
        private readonly Mock<IIndexOutput> _mockIndexOutput;
        private readonly Interactor _systemUnderTest;

        public InteractorTests()
        {
            _mockTextInput = new Mock<ITextInput>();
            _mockWordCountAnalyzer = new Mock<IWordCountAnalyzer>();
            _mockWordCountAnalyzerOutput = new Mock<IWordCountAnalyzerOutput>();
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
                .RegisterType<Interactor>()
                .SingleInstance();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<Interactor>();
        }
    }
}
