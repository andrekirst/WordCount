using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Builder;

namespace WordCount.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<
            TImplementation,
            ConcreteReflectionActivatorData,
            SingleRegistrationStyle>
            Register<TService, TImplementation>(this ContainerBuilder containerBuilder)
        {
            return containerBuilder
                .RegisterType<TImplementation>()
                .As<TService>()
                .SingleInstance();
        }
    }
}
