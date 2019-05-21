using WordCount.Abstractions.SystemAbstractions.Net.Http;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class TextUrlFileLoader : ITextUrlFileLoader
    {
        private ITextUrlParameterParser TextUrlParameterParser { get; }
        private IHttpClient HttpClient { get; }

        public TextUrlFileLoader(
            ITextUrlParameterParser textUrlParameterParser,
            IHttpClient httpClient)
        {
            TextUrlParameterParser = textUrlParameterParser;
            HttpClient = httpClient;
        }

        public string ReadTextFile()
        {
            TextUrlParameter textUrlParameter = TextUrlParameterParser.ParseTextUrlParameter();

            return textUrlParameter.IsPresent ?
                HttpClient.ReadString(url: textUrlParameter.Url).Result
                : null;
        }
    }
}
