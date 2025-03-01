using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WordCount.Implementations;
using WordCount.Interfaces;

namespace WordCount
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static async Task<int> Main()
        {
            var interactor = CreateInteractor();

            return await interactor.Execute();
        }

        private static IInteractor CreateInteractor()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IInteractor, Interactor>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IInteractor>();
        }
    }
}
