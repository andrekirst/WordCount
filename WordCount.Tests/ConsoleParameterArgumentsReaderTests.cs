using System;
using System.Linq;
using WordCount.Implementations;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests
{
    public class ConsoleParameterArgumentsReaderTests
    {
        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Arguments_null_Expect_null()
        {
            ConsoleParameterArgumentsReader systemUnderTest = new ConsoleParameterArgumentsReader();

            ArgumentsReaderResult actual = systemUnderTest.ReadArguments(args: null);

            Assert.Null(@object: actual);
        }

        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Arguments_empty_Expect_null()
        {
            ConsoleParameterArgumentsReader systemUnderTest = new ConsoleParameterArgumentsReader();

            ArgumentsReaderResult actual = systemUnderTest.ReadArguments(args: new string[] { });

            Assert.Null(@object: actual);
        }

        [Fact]
        public void ConsoleParameterArgumentsReaderTests_Arguments_One_Entry_mytext_Expect_mytext()
        {
            ConsoleParameterArgumentsReader systemUnderTest = new ConsoleParameterArgumentsReader();

            ArgumentsReaderResult actual = systemUnderTest.ReadArguments(args: new string[] { "mytext.txt" });

            Assert.Equal(expected: "mytext.txt", actual: actual.SourceTextFile);
        }
    }
}
