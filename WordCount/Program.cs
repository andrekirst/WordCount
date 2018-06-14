using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCount.Implementations;
using WordCount.Interfaces;

namespace WordCount
{
    public class Program
    {
        public static int Main(string[] args)
        {
            IInteractor interactor = CreateInteractor();

            return interactor.Execute();
        }

        private static IInteractor CreateInteractor()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterType<Interactor>()
                .As<IInteractor>()
                .SingleInstance();

            containerBuilder
                .RegisterType<ConsoleTextInput>()
                .As<ITextInput>();

            containerBuilder
                .RegisterType<WordCountAnalyzer>()
                .As<IWordCountAnalyzer>();

            containerBuilder
                .RegisterType<WordCountAnalyzerOutput>()
                .As<IWordCountAnalyzerOutput>();

            return containerBuilder
                .Build()
                .Resolve<IInteractor>();
        }
    }
}
