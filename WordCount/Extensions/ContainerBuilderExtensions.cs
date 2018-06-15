using Autofac;

namespace WordCount.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void Register<TService, TImplementation>(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<TImplementation>()
                .As<TService>();
        }
    }
}
