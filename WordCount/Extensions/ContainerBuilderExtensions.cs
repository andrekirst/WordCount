using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
