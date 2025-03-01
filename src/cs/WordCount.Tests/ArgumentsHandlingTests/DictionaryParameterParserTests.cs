using Moq;
using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using WordCount.Implementations.ArgumentsHandling;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests;

public class DictionaryParameterParserTests
{
    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_have_no_Dictionary_Parameter_Expect_IsPresent_False(
        DictionaryParameterParser sut)
    {
        var actual = sut.ParseParameter(["bla.txt"]);

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_is_null_Expect_IsPresent_False(DictionaryParameterParser sut)
    {
        var actual = sut.ParseParameter(null);

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_is_empty_Expect_IsPresent_False(DictionaryParameterParser sut)
    {
        var actual = sut.ParseParameter([]);

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_has_DictionaryParameter_with_no_equal_sign_Expect_IsPresent_False(DictionaryParameterParser sut)
    {
        var actual = sut.ParseParameter(["-dictionary"]);

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_has_DictionaryParameter_Without_File_Expect_IsPresent_False(DictionaryParameterParser sut)
    {
        var actual = sut.ParseParameter(["-dictionary="]);

        actual.Should().NotBeNull();
        actual.IsPresent.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void DictionaryParameterParserTests_Args_has_DictionaryParameter_With_File_bla_txt_Expect_FileName_bla_txt(DictionaryParameterParser sut)
    {
        var actual = sut.ParseParameter(["-dictionary=bla.txt"]);

        actual.Should().NotBeNull();
        actual.FileName.Should().Be("bla.txt");
    }
}