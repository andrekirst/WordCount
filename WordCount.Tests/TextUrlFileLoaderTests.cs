using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Abstractions.SystemAbstractions.Net.Http;
using WordCount.Implementations;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;
using Xunit;

namespace WordCount.Tests;

public class TextUrlFileLoaderTests
{
    [Theory, AutoMoqData]
    public void TextUrlFileLoaderTests_HttpClient_returns_abc_expect_abc(
        [Frozen] Mock<ITextUrlParameterParser> textUrlParameterParser,
        [Frozen] Mock<IHttpClient> httpClient,
        TextUrlFileLoader sut)
    {
        textUrlParameterParser
            .Setup(m => m.ParseTextUrlParameter())
            .Returns(new TextUrlParameter { IsPresent = true });

        httpClient
            .Setup(m => m.ReadString(It.IsAny<string>()))
            .Returns(Task.FromResult("abc"));

        var actual = sut.ReadTextFile();

        actual.Should().Be("abc");
    }

    [Theory, AutoMoqData]
    public void TextUrlFileLoaderTests_Parameter_Url_expect_in_call_of_readString(
        [Frozen] Mock<ITextUrlParameterParser> textUrlParameterParser,
        [Frozen] Mock<IHttpClient> httpClient,
        TextUrlFileLoader sut)
    {
        textUrlParameterParser
            .Setup(m => m.ParseTextUrlParameter())
            .Returns(new TextUrlParameter
            {
                IsPresent = true,
                Url = "http://textfiles.rolz.org/adventure/221baker.txt"
            });

        sut.ReadTextFile();

        httpClient.Verify(v => v.ReadString("http://textfiles.rolz.org/adventure/221baker.txt"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void TextUrlFileLoaderTests_Parameter_is_not_present_expect_null(
        [Frozen] Mock<ITextUrlParameterParser> textUrlParameterParser,
        [Frozen] Mock<IHttpClient> httpClient,
        TextUrlFileLoader sut)
    {
        textUrlParameterParser
            .Setup(m => m.ParseTextUrlParameter())
            .Returns(new TextUrlParameter { IsPresent = false });

        var actual = sut.ReadTextFile();

        actual.Should().BeNull();

        httpClient.Verify(v => v.ReadString(It.IsAny<string>()), Times.Never);
    }
}