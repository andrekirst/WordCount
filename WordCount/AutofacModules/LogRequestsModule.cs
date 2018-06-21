using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Core;

namespace WordCount.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class LogRequestsModule : Module
    {
        [ExcludeFromCodeCoverage]
        protected override void AttachToComponentRegistration(
            IComponentRegistry componentRegistry,
            IComponentRegistration registration)
        {
            void RegistrationAction(IInstanceActivator activator, string actionName)
            {
                Debug.WriteLine(value: $"{actionName} concrete type {activator.LimitType}");
            }

            registration.Preparing += (sender, args) =>
            {
                RegistrationAction(
                    activator: args.Component.Activator,
                    actionName: "Resolving");
            };

            registration.Activated += (sender, args) =>
            {
                RegistrationAction(
                    activator: args.Component.Activator,
                    actionName: "Activated");
            };

            registration.Activating += (sender, args) =>
            {
                RegistrationAction(
                    activator: args.Component.Activator,
                    actionName: "Activating");
            };
        }
    }
}
