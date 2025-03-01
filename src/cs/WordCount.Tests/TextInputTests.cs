// using System.Threading.Tasks;
// using AutoFixture.Xunit2;
// using FluentAssertions;
// using Moq;
// using WordCount.Abstractions.SystemAbstractions;
// using WordCount.Implementations;
// using WordCount.Interfaces;
// using WordCount.Interfaces.Output;
// using Xunit;

// namespace WordCount.Tests;

// public class TextInputTests
// {
//     [Theory, AutoMoqData]
//     public async Task TextInputTests_TextFileLoader_returns_bla_Expect_HasEnteredText_false_and_Text_bla(
//         [Frozen] Mock<ITextFileLoader> textFileLoader,
//         TextInput sut)
//     {
//         textFileLoader
//             .Setup(m => m.ReadTextFile())
//             .Returns("bla");

//         var actual = await sut.GetInputText();

//         actual.Should().NotBeNull();
//         actual.Text.Should().Be("bla");
//         actual.HasEnteredConsoleText.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public async Task TextInputTests_TextUrlFileLoader_returns_bla_Expect_HasEnteredText_false_and_Text_bla(
//         [Frozen] Mock<ITextFileLoader> textFileLoader,
//         TextInput sut)
//     {
//         textFileLoader
//             .Setup(m => m.ReadTextFile())
//             .Returns("bla");

//         var actual = await sut.GetInputText();

//         actual.Should().NotBeNull();
//         actual.Text.Should().Be("bla");
//         actual.HasEnteredConsoleText.Should().BeFalse();
//     }

//     [Theory, AutoMoqData]
//     public async Task TextInputTests_ConsoleInput_bla_Expect_EnterText_and_content_bla_and_HasEnteredConsoleText_true(
//         [Frozen] Mock<IConsole> console,
//         [Frozen] Mock<IDisplayOutput> displayOutput,
//         TextInput sut)
//     {
//         console
//             .Setup(m => m.ReadLine())
//             .Returns("bla");

//         var actual = await sut.GetInputText();

//         actual.Should().NotBeNull();
//         actual.Text.Should().Be("bla");
//         actual.HasEnteredConsoleText.Should().BeTrue();

//         displayOutput.Verify(v => v.WriteResource("ENTER_TEXT"), Times.Once);
//     }

//     [Theory, AutoMoqData]
//     public async Task TextInputTests_ConsoleInput_empty_Expect_EnterText_and_content_empty_and_HasEnteredConsoleText_false(
//         [Frozen] Mock<IConsole> console,
//         [Frozen] Mock<IDisplayOutput> displayOutput,
//         TextInput sut)
//     {
//         console
//             .Setup(m => m.ReadLine())
//             .Returns(string.Empty);

//         var actual = await sut.GetInputText();

//         actual.Should().NotBeNull();
//         actual.Text.Should().BeEmpty();
//         actual.HasEnteredConsoleText.Should().BeFalse();

//         displayOutput.Verify(v => v.WriteResource("ENTER_TEXT"), Times.Once);
//     }
// }