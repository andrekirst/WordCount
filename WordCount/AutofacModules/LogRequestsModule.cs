using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Core;

namespace WordCount.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class LogRequestsModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            Action<IInstanceActivator, string> action = (act, s) =>
            {
                Debug.WriteLine(value: $"{s} concrete type {act.LimitType}");
            };

            registration.Preparing += (sender, args) =>
            {
                action(arg1: args.Component.Activator, arg2: "Resolving");
            };

            registration.Activated += (sender, args) =>
            {
                action(arg1: args.Component.Activator, arg2: "Activated");
            };

            registration.Activating += (sender, args) =>
            {
                action(arg1: args.Component.Activator, arg2: "Activating");
            };
        }
    }
}
