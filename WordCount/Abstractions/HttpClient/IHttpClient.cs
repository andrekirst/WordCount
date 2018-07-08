using System.Threading.Tasks;

namespace WordCount.Abstractions.HttpClient
{
    public interface IHttpClient
    {
        Task<string> ReadString(string url);
    }
}
