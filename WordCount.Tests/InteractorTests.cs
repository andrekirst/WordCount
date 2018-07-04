using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
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

        [NamedFact]
        public void InteractorTests_Loop_While_Enter_Text_Expect_2_Calls_of_GetInputText()
        {
            MockSequence sequence = new MockSequence();

            _mockTextInput
                .InSequence(sequence: sequence)
                .Setup(expression: m => m.GetInputText())
                .Returns(value: new Models.InputTextResult() {HasEnteredText = true, Text = "Bla bla"});

            WordCountAnalyzerResult wordCountAnalyzerResult = new WordCountAnalyzerResult();

            _mockWordCountAnalyzer
                .InSequence(sequence: sequence)
                .Setup(m => m.Analyze("Bla bla"))
                .Returns(value: wordCountAnalyzerResult);

            _mockTextInput
                .InSequence(sequence: sequence)
                .Setup(m => m.GetInputText())
                .Returns(value: new InputTextResult() { HasEnteredText = false, Text = ""});

        int actual = _systemUnderTest.Execute();

            Assert.Equal(
                expected: 0,
                actual: actual);

            _mockTextInput
                .Verify(
                    expression: v => v.GetInputText(),
                    times: Times.Exactly(callCount: 2));
        }
    }
}
