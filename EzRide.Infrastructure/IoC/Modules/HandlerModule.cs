using System.Reflection;

using Autofac;
using EzRide.Infrastructure.Handlers;

namespace EzRide.Infrastructure.IoC.Modules
{
    public class HandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = typeof(HandlerModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
            
            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance();
        }
    }
}