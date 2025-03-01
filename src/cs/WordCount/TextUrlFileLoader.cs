using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WordCount;

public interface ITextUrlFileLoader
{
    Task<string?> ReadTextFile();
}

public class TextUrlFileLoader(
    IOptions<WordCountCommand.Settings> settings,
    HttpClient httpClient) : ITextUrlFileLoader
{
    private readonly WordCountCommand.Settings _settings = settings.Value;

    public async Task<string?> ReadTextFile()
    {
        return _settings.TextUrl != null
            ? await httpClient.GetStringAsync(_settings.TextUrl)
            : null;
    }
}
