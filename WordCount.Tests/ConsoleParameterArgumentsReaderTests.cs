using WordCount.Implementations;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class ConsoleParameterArgumentsReaderTests
    {
        private readonly ConsoleParameterArgumentsReader _systemUnderTest;

        public ConsoleParameterArgumentsReaderTests()
        {
            _systemUnderTest = new ConsoleParameterArgumentsReader();
        }

        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Arguments_null_Expect_IsSourceTextFilePresent_False()
        {
            ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: null);

            Assert.False(condition: actual.IsSourceTextFileParameterPresent);
        }

        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Arguments_empty_Expect_IsSourceTextFilePresent_False()
        {
            ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new string[] { });

            Assert.False(condition: actual.IsSourceTextFileParameterPresent);
        }

        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Arguments_One_Entry_mytext_Expect_mytext()
        {
            ConsoleParameterArgumentsReader systemUnderTest = new ConsoleParameterArgumentsReader();

            ArgumentsReaderResult actual = systemUnderTest.ReadArguments(args: new[] { "mytext.txt" });

            Assert.Equal(expected: "mytext.txt", actual: actual.SourceTextFile);
        }

        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Arguments_One_Entry_mytext_Expect_IsSourceTextFilePresent_True()
        {
            ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new[] { "mytext.txt" });

            Assert.True(condition: actual.IsSourceTextFileParameterPresent);
        }

        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Argument_Index_Is_Present_Expect_IndexParameterPresent_True()
        {
            ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new[] {"-index"});
            Assert.True(condition: actual.IsIndexParameterPresent);
        }
    }
}
