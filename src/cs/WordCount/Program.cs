using System.IO.Abstractions;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Spectre.Console.Cli;

namespace WordCount;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var app = new CommandApp();
        app.SetDefaultCommand<WordCountCommand>();
        return await app.RunAsync(args);        
    }
}

public sealed class WordCountCommand : AsyncCommand<WordCountCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddOptions<WordCountOptions>();

        builder.Services.AddSingleton(_ => Options.Create(settings));
        builder.Services.AddSingleton<IInteractor, Interactor>();
        builder.Services.AddSingleton<ITextInput, TextInput>();
        builder.Services.AddSingleton<ITextFileLoader, TextFileLoader>();
        builder.Services.AddSingleton<IFileSystem, FileSystem>();
        builder.Services.AddSingleton<IDisplayOutput, ConsoleDisplayOutput>();
        builder.Services.AddSingleton<ILanguageResource, LanguageResource>();
        builder.Services.AddSingleton<ILanguageDecision, LanguageDecision>();
        builder.Services.AddSingleton<ITextUrlFileLoader, TextUrlFileLoader>();
        builder.Services.AddSingleton(_ => new HttpClient());
        builder.Services.AddSingleton<IWordCountAnalyzer, WordCountAnalyzer>();
        builder.Services.AddSingleton<ITextSplit, TextSplit>();
        builder.Services.AddSingleton<IStopwordRemover, StopwordRemover>();
        builder.Services.AddSingleton<IStopwordLoader, StopwordLoader>();
        builder.Services.AddSingleton<IWordCountAnalyzerOutput, WordCountAnalyzerOutput>();
        builder.Services.AddSingleton<IStatisticsOutput, StatisticsOutput>();
        builder.Services.AddSingleton<IIndexOutput, IndexOutput>();
        builder.Services.AddSingleton<IDictionaryFileLoader, DictionaryFileLoader>();
        builder.Services.AddSingleton(_ => new ResourceManager("WordCount.Resources.Resource", typeof(Program).Assembly));

        var host = builder.Build();

        var interactor = host.Services.GetRequiredService<IInteractor>();

        return await interactor.Execute();
    }

    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "[sourceFile]")]
        public string? SourceFile { get; init; }

        [CommandOption("-d|--dictionary")]
        public string? Dictionary { get; init; }

        [CommandOption("-i|--index")]
        public bool Index { get; init; }

        [CommandOption("-l|--lang|--language")]
        public string? Language { get; init; }

        [CommandOption("-s|--stopwordlist")]
        public string? StopwordList { get; init; }

        [CommandOption("-t|--texturl")]
        public string? TextUrl { get; init; }
    }
}
