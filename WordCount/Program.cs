using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using System.IO.Abstractions;
using System.Reflection;
using WordCount.AutofacModules;
using WordCount.Interfaces;

namespace WordCount
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static int Main()
        {
            IInteractor interactor = CreateInteractor();

            return interactor.Execute();
        }

        private static IInteractor CreateInteractor()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

#if DEBUG
            containerBuilder
                    .RegisterModule<LogRequestsModule>(); 
#endif

            var executingAssembly = Assembly.GetExecutingAssembly();
            var externalFileSystemAssembly = Assembly.GetAssembly(type: typeof(IFileSystem));

            containerBuilder.RegisterAssemblyTypes(
                    executingAssembly,
                    externalFileSystemAssembly)
                .AsImplementedInterfaces();

            return containerBuilder
                .Build()
                .Resolve<IInteractor>();
        }
    }
}
