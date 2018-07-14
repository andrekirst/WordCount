using WordCount.Abstractions.SystemAbstractions.Net.Http;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class TextUrlFileLoader : ITextUrlFileLoader
    {
        private readonly ITextUrlParameterParser _textUrlParameterParser;
        private readonly IHttpClient _httpClient;

        public TextUrlFileLoader(
            ITextUrlParameterParser textUrlParameterParser,
            IHttpClient httpClient)
        {
            _textUrlParameterParser = textUrlParameterParser;
            _httpClient = httpClient;
        }

        public string ReadTextFile()
        {
            TextUrlParameter textUrlParameter = _textUrlParameterParser.ParseTextUrlParameter();

            return textUrlParameter.IsPresent ?
                _httpClient.ReadString(url: textUrlParameter.Url).Result
                : null;
        }
    }
}
