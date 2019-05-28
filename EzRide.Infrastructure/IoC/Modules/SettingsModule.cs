using Autofac;
using EzRide.Infrastructure.Extensions;
using EzRide.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace EzRide.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration configuration;

        public SettingsModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
            builder.RegisterInstance(configuration.GetSettings<JwtSettings>())
                .SingleInstance();
        }
    }
}