using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace WordCount.Abstractions.HttpClient
{
    [ExcludeFromCodeCoverage]
    public class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public HttpClient()
        {
            _httpClient = new System.Net.Http.HttpClient();
        }

        public async Task<string> ReadString(string url)
        {
            return await _httpClient.GetStringAsync(requestUri: url);
        }
    }
}
