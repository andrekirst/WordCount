using System.Collections.Generic;
using FluentAssertions;
using WordCount.Helpers;
using Xunit;

namespace WordCount.Tests.EnumerableHelpersTests;

public class CountUnknownWordsTests
{
    [Fact]
    public void CountUnknownWordsTests_IEnumerables_null_Expect_0()
    {
        var actual = EnumerableHelpers.CountUnknownWords(null, null);

        actual.Should().Be(0);
    }

    [Theory, AutoMoqData]
    public void CountUnknownWordsTests_distinctWords_null_DictionaryWords_1_Entry_Expect_0(
        List<string> dictionaryWords)
    {
        var actual = EnumerableHelpers.CountUnknownWords(
            null,
            dictionaryWords);

        actual.Should().Be(0);
    }

    [Theory, AutoMoqData]
    public void CountUnknownWordsTests_distinctWords_1_Eintrag_DictionaryWords_null_Expect_1(
        List<string> distinctWords)
    {
        var actual = EnumerableHelpers.CountUnknownWords(
            distinctWords,
            null);

        actual.Should().Be(distinctWords.Count);
    }


    [Theory, AutoMoqData]
    public void CountUnknownWordsTests_same_values_in_lists_Expect_0(
        List<string> list)
    {
        var actual = EnumerableHelpers.CountUnknownWords(
            list,
            list);

        actual.Should().Be(0);
    }

    [Theory, AutoMoqData]
    public void CountUnknownWordsTests_distinctWords_abc_dictionaryWords_def_Expect_1(
        List<string> distinctWords,
        List<string> dictionaryWords)
    {
        var actual = EnumerableHelpers.CountUnknownWords(
            distinctWords,
            dictionaryWords);

        actual.Should().Be(dictionaryWords.Count);
    }
}