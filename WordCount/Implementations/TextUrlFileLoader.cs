using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCount.Abstractions.HttpClient;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;

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
            throw new NotImplementedException();
        }
    }
}
