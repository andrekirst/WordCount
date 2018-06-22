using System.Diagnostics.CodeAnalysis;
using Autofac;
using System.IO.Abstractions;
using WordCount.Abstractions.Console;
using WordCount.AutofacModules;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Extensions;

namespace WordCount
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static int Main(string[] args)
        {
            IInteractor interactor = CreateInteractor();

            return interactor.Execute(args: args);
        }

        private static IInteractor CreateInteractor()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

#if DEBUG
            containerBuilder
                    .RegisterModule<LogRequestsModule>(); 
#endif

            containerBuilder.Register<IInteractor, Interactor>();
            containerBuilder.Register<ITextInput, TextInput>();
            containerBuilder.Register<IWordCountAnalyzer, WordCountAnalyzer>();
            containerBuilder.Register<IWordCountAnalyzerOutput, WordCountAnalyzerOutput>();
            containerBuilder.Register<IFileSystem, FileSystem>();
            containerBuilder.Register<ITextFileLoader, TextFileLoader>();
            containerBuilder.Register<IConsole, Console>();
            containerBuilder.Register<IDisplayOutput, ConsoleDisplayOutput>();
            containerBuilder.Register<IArgumentsReader, ConsoleParameterArgumentsReader>();
            containerBuilder.Register<ITextSplit, TextSplit>();
            containerBuilder.Register<IStopwordLoader, StopwordLoader>();
            containerBuilder.Register<IStopwordRemover, StopwordRemover>();
            containerBuilder.Register<IIndexOutput, IndexOutput>();

            return containerBuilder
                .Build()
                .Resolve<IInteractor>();
        }
    }
}
