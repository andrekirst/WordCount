using System.Collections.Generic;
using WordCount.Helpers;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.EnumerableHelpersTests
{
    public class CountUnknownWordsTests
    {
        [NamedFact]
        public void CountUnknownWordsTests_IEnumerables_null_Expect_0()
        {
            int actual = EnumerableHelpers.CountUnknownWords(
                distinctWords: null,
                dictionaryWords: null);

            Assert.Equal(expected: 0, actual: actual);
        }

        [NamedFact]
        public void CountUnknownWordsTests_distinctWords_null_DictionaryWords_1_Entry_Expect_0()
        {
            int actual = EnumerableHelpers.CountUnknownWords(
                distinctWords: null,
                dictionaryWords: new List<string>() { "eintrag1" });

            Assert.Equal(expected: 0, actual: actual);
        }

        [NamedFact]
        public void CountUnknownWordsTests_distinctWords_1_Eintrag_DictionaryWords_null_Expect_1()
        {
            int actual = EnumerableHelpers.CountUnknownWords(
                distinctWords: new List<string>() { "eintrag1" },
                dictionaryWords: null);

            Assert.Equal(expected: 1, actual: actual);
        }


        [NamedFact]
        public void CountUnknownWordsTests_same_values_in_lists_Expect_0()
        {
            int actual = EnumerableHelpers.CountUnknownWords(
                distinctWords: new List<string>() { "eintrag1" },
                dictionaryWords: new List<string>() { "eintrag1" });

            Assert.Equal(expected: 0, actual: actual);
        }

        [NamedFact]
        public void CountUnknownWordsTests_distinctWords_abc_dictionaryWords_def_Expect_1()
        {
            int actual = EnumerableHelpers.CountUnknownWords(
                distinctWords: new List<string>() { "abc" },
                dictionaryWords: new List<string>() { "def" });

            Assert.Equal(expected: 1, actual: actual);
        }
    }
}
