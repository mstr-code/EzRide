using System.Reflection;

using Autofac;
using EzRide.Infrastructure.Services;

namespace EzRide.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            Assembly assembly = typeof(ServiceModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            // Register Encrypter:
            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();
        }
    }
}