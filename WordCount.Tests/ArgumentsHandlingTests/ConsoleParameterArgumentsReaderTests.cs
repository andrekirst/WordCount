using WordCount.Implementations;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests
{
    public class ConsoleParameterArgumentsReaderTests
    {
        private readonly ConsoleParameterArgumentsReader _systemUnderTest;

        public ConsoleParameterArgumentsReaderTests()
        {
            _systemUnderTest = null;//new ConsoleParameterArgumentsReader();
        }

        //[NamedFact]
        //public void ConsoleParameterArgumentsReaderTests_Arguments_null_Expect_IsSourceTextFilePresent_False()
        //{
        //    ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: null);

        //    Assert.False(condition: actual.IsSourceTextFileParameterPresent);
        //}

        //[NamedFact]
        //public void ConsoleParameterArgumentsReaderTests_Arguments_empty_Expect_IsSourceTextFilePresent_False()
        //{
        //    ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new string[] { });

        //    Assert.False(condition: actual.IsSourceTextFileParameterPresent);
        //}

        //[NamedFact]
        //public void ConsoleParameterArgumentsReaderTests_Arguments_One_Entry_mytext_Expect_mytext()
        //{
        //    ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new[] { "mytext.txt" });

        //    Assert.Equal(expected: "mytext.txt", actual: actual.SourceTextFile);
        //}

        //[NamedFact]
        //public void ConsoleParameterArgumentsReaderTests_Arguments_One_Entry_mytext_Expect_IsSourceTextFilePresent_True()
        //{
        //    ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new[] { "mytext.txt" });

        //    Assert.True(condition: actual.IsSourceTextFileParameterPresent);
        //}

        //[NamedFact]
        //public void ConsoleParameterArgumentsReaderTests_Argument_Index_Is_Present_Expect_IndexParameterPresent_True()
        //{
        //    ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new[] {"-index"});
        //    Assert.True(condition: actual.IsIndexParameterPresent);
        //}

        //[NamedFact]
        //public void ConsoleParameterArgumentsReaderTests_Argument_Dictionary_Is_Present_Expect_DictionaryParameterPresent_True()
        //{
        //    ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new[] { "-dictionary=dict.txt" });
        //    Assert.True(condition: actual.IsDictionaryParameterPresent);
        //}

        //[NamedFact]
        //public void ConsoleParameterArgumentsReaderTests_Argument_Dictionary_Is_Present_Expect_DictionaryName_Dict_txt()
        //{
        //    ArgumentsReaderResult actual = _systemUnderTest.ReadArguments(args: new[] { "-dictionary=dict.txt" });
        //    Assert.Equal(expected: "dict.txt", actual: actual.DictionaryTextFile);
        //}
    }
}
