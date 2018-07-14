using System.Threading.Tasks;

namespace WordCount.Abstractions.SystemAbstractions.Net.Http
{
    public interface IHttpClient
    {
        Task<string> ReadString(string url);
    }
}
