using System;
using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class InteractorTests
    {
        private readonly Mock<ITextInput> _mockTextInput;
        private readonly Mock<IWordCountAnalyzer> _mockWordCountAnalyzer;
        private readonly Mock<IWordCountAnalyzerOutput> _mockWordCountAnalyzerOutput;
        private readonly Mock<IDisplayOutput> _mockDisplayOutput;
        private readonly Mock<IIndexOutput> _mockIndexOutput;
        private readonly Interactor _systemUnderTest;

        public InteractorTests()
        {
            _mockTextInput = new Mock<ITextInput>();
            _mockWordCountAnalyzer = new Mock<IWordCountAnalyzer>();
            _mockWordCountAnalyzerOutput = new Mock<IWordCountAnalyzerOutput>();
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

        [NamedFact]
        public void InteractorTests_TextInput_Throws_Error_Expect_ReturnCode_1()
        {
            _mockTextInput
                .Setup(m => m.GetInputText())
                .Throws<Exception>();

            int actual = _systemUnderTest.Execute();

            Assert.Equal(expected: 1, actual: actual);
        }
    }
}
