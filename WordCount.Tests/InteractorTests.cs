using Autofac;
using Moq;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;
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
        private readonly Mock<IHelpOutput> _mockHelpOutput;
        private readonly Interactor _systemUnderTest;

        public InteractorTests()
        {
            _mockTextInput = new Mock<ITextInput>();
            _mockWordCountAnalyzer = new Mock<IWordCountAnalyzer>();
            _mockWordCountAnalyzerOutput = new Mock<IWordCountAnalyzerOutput>();
            _mockIndexOutput = new Mock<IIndexOutput>();
            _mockHelpOutput = new Mock<IHelpOutput>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(instance: _mockIndexOutput.Object)
                .As<IIndexOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockTextInput.Object)
                .As<ITextInput>();

            containerBuilder
                .RegisterInstance(instance: _mockWordCountAnalyzer.Object)
                .As<IWordCountAnalyzer>();

            containerBuilder
                .RegisterInstance(instance: _mockWordCountAnalyzerOutput.Object)
                .As<IWordCountAnalyzerOutput>();

            containerBuilder
                .RegisterInstance(instance: _mockHelpOutput.Object)
                .As<IHelpOutput>();

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
                .Returns(value: new InputTextResult() {HasEnteredConsoleText = true, Text = "Bla bla"});

            WordCountAnalyzerResult wordCountAnalyzerResult = new WordCountAnalyzerResult();

            _mockWordCountAnalyzer
                .InSequence(sequence: sequence)
                .Setup(m => m.Analyze("Bla bla"))
                .Returns(value: wordCountAnalyzerResult);

            _mockTextInput
                .InSequence(sequence: sequence)
                .Setup(m => m.GetInputText())
                .Returns(value: new InputTextResult() { HasEnteredConsoleText = false, Text = ""});

            int actual = _systemUnderTest.Execute();

            Assert.Equal(
                expected: 0,
                actual: actual);

            _mockTextInput
                .Verify(
                    expression: v => v.GetInputText(),
                    times: Times.Exactly(callCount: 2));
        }

        [NamedFact]
        public void InteractorTests_If_HelpParameter_is_present_Expect_ReturnCode_1()
        {
            _mockHelpOutput
                .Setup(m => m.ShowHelpIfRequested())
                .Returns(true);

            int actual = _systemUnderTest.Execute();

            Assert.Equal(expected: 1, actual: actual);
        }

        [NamedFact]
        public void InteractorTests_if_the_first_input_is_empty_return_0_and_do_not_execute_Analyzer()
        {
            _mockTextInput
                .Setup(m => m.GetInputText())
                .Returns(new InputTextResult() {HasEnteredConsoleText = true, Text = string.Empty});

            int actual = _systemUnderTest.Execute();

            Assert.Equal(expected: 0, actual: actual);
            _mockWordCountAnalyzer
                .Verify(
                    expression: v => v.Analyze(It.IsAny<string>()),
                    times: Times.Never);
        }
    }
}
