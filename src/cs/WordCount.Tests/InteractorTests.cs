// using AutoFixture.Xunit2;
// using FluentAssertions;
// using Moq;
// using WordCount.Implementations;
// using WordCount.Interfaces;
// using WordCount.Interfaces.Output;
// using WordCount.Models.Results;
// using Xunit;

// namespace WordCount.Tests;

// public class InteractorTests
// {
//     [Theory, AutoMoqData]
//     public void InteractorTests_Loop_While_Enter_Text_Expect_2_Calls_of_GetInputText(
//         [Frozen] Mock<ITextInput> textInput,
//         [Frozen] Mock<IWordCountAnalyzer> wordCountAnalyzer,
//         Interactor sut)
//     {
//         var sequence = new MockSequence();

//         textInput
//             .InSequence(sequence)
//             .Setup(m => m.GetInputText())
//             .Returns(new InputTextResult() {HasEnteredConsoleText = true, Text = "Bla bla"});

//         var wordCountAnalyzerResult = new WordCountAnalyzerResult();

//         wordCountAnalyzer
//             .InSequence(sequence)
//             .Setup(m => m.Analyze("Bla bla"))
//             .Returns(wordCountAnalyzerResult);

//         textInput
//             .InSequence(sequence)
//             .Setup(m => m.GetInputText())
//             .Returns(new InputTextResult() { HasEnteredConsoleText = false, Text = ""});

//         var actual = sut.Execute();

//         actual.Should().Be(0);

//         textInput.Verify(v => v.GetInputText(), Times.Exactly(2));
//     }

//     [Theory, AutoMoqData]
//     public void InteractorTests_If_HelpParameter_is_present_Expect_ReturnCode_1(
//         [Frozen] Mock<IHelpOutput> helpOutput,
//         Interactor sut)
//     {
//         helpOutput
//             .Setup(m => m.ShowHelpIfRequested())
//             .Returns(true);

//         var actual = sut.Execute();

//         actual.Should().Be(1);
//     }

//     [Theory, AutoMoqData]
//     public void InteractorTests_if_the_first_input_is_empty_return_0_and_do_not_execute_Analyzer(
//         [Frozen] Mock<ITextInput> textInput,
//         [Frozen] Mock<IWordCountAnalyzer> wordCountAnalyzer,
//         Interactor sut)
//     {
//         textInput
//             .Setup(m => m.GetInputText())
//             .Returns(new InputTextResult() {HasEnteredConsoleText = true, Text = string.Empty});

//         var actual = sut.Execute();

//         actual.Should().Be(0);
//         wordCountAnalyzer.Verify(v => v.Analyze(It.IsAny<string>()), Times.Never);
//     }
// }