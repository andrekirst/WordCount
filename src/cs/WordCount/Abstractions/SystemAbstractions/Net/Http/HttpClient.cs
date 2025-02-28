using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace WordCount.Abstractions.SystemAbstractions.Net.Http
{
    [ExcludeFromCodeCoverage]
    public class HttpClient : IHttpClient
    {
        [ExcludeFromCodeCoverage]
        public Task<string> ReadString(string url)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri(url)
            };
            return httpClient.GetStringAsync(url);
        }
    }
}
