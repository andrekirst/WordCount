using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace WordCount.Abstractions.HttpClient
{
    [ExcludeFromCodeCoverage]
    public class HttpClient : IHttpClient
    {
        [ExcludeFromCodeCoverage]
        public async Task<string> ReadString(string url)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient()
            {
                BaseAddress = new Uri(uriString: url)
            };
            return await httpClient.GetStringAsync(requestUri: url);
        }
    }
}
