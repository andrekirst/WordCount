using System.Net.Http;
using System.Threading.Tasks;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;

namespace WordCount.Implementations;

public class TextUrlFileLoader(
    ITextUrlParameterParser textUrlParameterParser,
    HttpClient httpClient) : ITextUrlFileLoader
{
    public async Task<string> ReadTextFile()
    {
        var textUrlParameter = textUrlParameterParser.ParseTextUrlParameter();

        return textUrlParameter.IsPresent
            ? await httpClient.GetStringAsync(textUrlParameter.Url)
            : null;
    }
}
