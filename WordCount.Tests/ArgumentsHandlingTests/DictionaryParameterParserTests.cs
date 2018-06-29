using System;
using System.Linq;
using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests
{
    public class DictionaryParameterParserTests
    {
        DictionaryParameterParser _systemUnderTest;
        public DictionaryParameterParserTests()
        {
            _systemUnderTest = new DictionaryParameterParser();
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_have_no_Dictionary_Parameter_Expect_IsPresent_False()
        {
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter(args: new[] {"mytext.txt"});

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_is_null_Expect_IsPresent_False()
        {
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter(args: null);

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_is_empty_Expect_IsPresent_False()
        {
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter(args: new string[0]);

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_has_DictionaryParameter_Expect_IsPresent_True()
        {
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter(args: new[]{"-dictionary=mytext.txt"});

            Assert.NotNull(@object: actual);
            Assert.True(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_has_DictionaryParameter_with_no_equal_sign_Expect_IsPresent_False()
        {
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter(args: new[] { "-dictionarymytext.txt" });

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void DictionaryParameterParserTests_Args_has_DictionaryParameter_Without_File_Expect_IsPresent_False()
        {
            DictionaryParameter actual = _systemUnderTest
                .ParseDictionaryParameter(args: new[] { "-dictionary=" });

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }
    }
}
