using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WordCount.Implementations;
using WordCount.Interfaces;

namespace WordCount;

public static class Program
{
    public static async Task<int> Main()
    {
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddOptions<WordCountOptions>();
        builder.Services.AddSingleton<IInteractor, Interactor>();
        
        var host = builder.Build();

        var interactor = host.Services.GetService<IInteractor>();

        return await interactor.Execute();
    }
}
