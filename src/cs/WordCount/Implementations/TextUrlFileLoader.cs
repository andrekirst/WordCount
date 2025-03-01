using System;
using System.Net.Http;
using System.Threading.Tasks;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations;

public class TextUrlFileLoader(
    IParameterParser<TextUrlParameter> textUrlParameterParser,
    HttpClient httpClient) : ITextUrlFileLoader
{
    public async Task<string> ReadTextFile()
    {
        var args = Environment.GetCommandLineArgs();
        var textUrlParameter = textUrlParameterParser.ParseParameter(args);

        return textUrlParameter.IsPresent
            ? await httpClient.GetStringAsync(textUrlParameter.Url)
            : null;
    }
}
