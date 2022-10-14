using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Implementations.Output;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;
using WordCount.Models.Requests;
using Xunit;

namespace WordCount.Tests;

public class IndexOutputTests
{
    [Theory, AutoMoqData]
    public void IndexOutputTests_DistinctWords_Bla_bla_Expect_Output_Index_bla_Bla(
        [Frozen] Mock<IIndexParameterParser> indexParameterParser,
        [Frozen] Mock<IDictionaryParameterParser> dictionaryParameterParser,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        IndexOutput sut)
    {
        var verifyList = new List<string>();

        indexParameterParser
            .Setup(m => m.ParseIndexParameter())
            .Returns(new IndexParameter { IsPresent = true });

        dictionaryParameterParser
            .Setup(m => m.ParseDictionaryParameter())
            .Returns(new DictionaryParameter { IsPresent = false });

        displayOutput
            .Setup(m => m.WriteLine(It.IsAny<string>()))
            .Callback<string>(s => verifyList.Add(s));

        var indexOutputRequest = new IndexOutputRequest
        {
            DistinctWords = new List<string> { "Bla", "bla" }
        };

        sut.OutputIndex(indexOutputRequest);

        displayOutput.Verify(v => v.WriteResourceLine("INDEX"), Times.Once);
        displayOutput.Verify(v => v.WriteLine("bla"), Times.Once);
        displayOutput.Verify(v => v.WriteLine("Bla"), Times.Once);

        verifyList[0].Should().Be("bla");
        verifyList[1].Should().Be("Bla");
    }

    [Theory, AutoMoqData]
    public void IndexOutputTests_DistinctWords_Bla_bla_Expect_Output_Index_With_Dict_bla_Bla_star(
        [Frozen] Mock<IIndexParameterParser> indexParameterParser,
        [Frozen] Mock<IDictionaryParameterParser> dictionaryParameterParser,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        [Frozen] Mock<IDictionaryFileLoader> dDictionaryFileLoader,
        IndexOutput sut)
    {
        var verifyList = new List<string>();

        dictionaryParameterParser
            .Setup(m => m.ParseDictionaryParameter())
            .Returns(new DictionaryParameter
            {
                IsPresent = true,
                FileName = It.IsAny<string>()
            });

        indexParameterParser
            .Setup(m => m.ParseIndexParameter())
            .Returns(new IndexParameter { IsPresent = true });

        displayOutput
            .Setup(m => m.WriteLine(It.IsAny<string>()))
            .Callback<string>(s => verifyList.Add(s));

        dDictionaryFileLoader
            .Setup(m => m.ReadWords())
            .Returns(new List<string> { "bla" });

        var indexOutputRequest = new IndexOutputRequest
        {
            DistinctWords = new List<string> { "Bla", "bla" }
        };

        sut.OutputIndex(indexOutputRequest);

        displayOutput.Verify(v => v.WriteResourceLine("INDEX_WITH_UNKNOWN", 1), Times.Once);
        displayOutput.Verify(v => v.WriteLine("bla"), Times.Once);
        displayOutput.Verify(v => v.WriteLine("Bla*"), Times.Once);

        verifyList[0].Should().Be("bla");
        verifyList[1].Should().Be("Bla*");
    }
}