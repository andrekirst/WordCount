using Autofac;
using System.IO.Abstractions;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Extensions;

namespace WordCount
{
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

            containerBuilder.Register<IInteractor, Interactor>();
            containerBuilder.Register<ITextInput, ConsoleTextInput>();
            containerBuilder.Register<IWordCountAnalyzer, WordCountAnalyzer>();
            containerBuilder.Register<IWordCountAnalyzerOutput, WordCountAnalyzerOutput>();
            containerBuilder.Register<IStopwordLoader, StopwordLoader>();
            containerBuilder.Register<IFileSystem, FileSystem>();
            containerBuilder.Register<IDisplayOutput, ConsoleDisplayOutput>();
            containerBuilder.Register<IArgumentsReader, ConsoleParameterArgumentsReader>();

            return containerBuilder
                .Build()
                .Resolve<IInteractor>();
        }
    }
}
