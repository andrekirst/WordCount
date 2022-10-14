//using System.Diagnostics;
//using System.Diagnostics.CodeAnalysis;
//using Autofac;
//using Autofac.Core;

//namespace WordCount.AutofacModules
//{
//    [ExcludeFromCodeCoverage]
//    public class LogRequestsModule : Module
//    {
//        [ExcludeFromCodeCoverage]
//        protected override void AttachToComponentRegistration(
//            IComponentRegistry componentRegistry,
//            IComponentRegistration registration)
//        {
//            registration.Preparing += Registration_Preparing;
//            registration.Activated += Registration_Activated;
//            registration.Activating += Registration_Activating;
//        }

//        [ExcludeFromCodeCoverage]
//        private void Registration_Activating(object sender, ActivatingEventArgs<object> e) =>
//            RegistrationAction(
//                activator: e.Component.Activator,
//                actionName: "Activating");

//        [ExcludeFromCodeCoverage]
//        private void Registration_Activated(object sender, ActivatedEventArgs<object> e) =>
//            RegistrationAction(
//                activator: e.Component.Activator,
//                actionName: "Activated");

//        [ExcludeFromCodeCoverage]
//        private void Registration_Preparing(object sender, PreparingEventArgs e) =>
//            RegistrationAction(
//                activator: e.Component.Activator,
//                actionName: "Resolving");

//        [ExcludeFromCodeCoverage]
//        private static void RegistrationAction(IInstanceActivator activator, string actionName) => Debug.WriteLine(value: $"{actionName} concrete type {activator.LimitType}");
//    }
//}
