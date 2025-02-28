using WordCount.Abstractions.SystemAbstractions.Net.Http;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;

namespace WordCount.Implementations;

public class TextUrlFileLoader(
    ITextUrlParameterParser textUrlParameterParser,
    IHttpClient httpClient) : ITextUrlFileLoader
{
    public string ReadTextFile()
    {
        var textUrlParameter = textUrlParameterParser.ParseTextUrlParameter();

        return textUrlParameter.IsPresent
            ? httpClient.ReadString(textUrlParameter.Url).Result
            : null;
    }
}
