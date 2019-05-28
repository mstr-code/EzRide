using Microsoft.Extensions.Configuration;

namespace EzRide.Infrastructure.Extensions
{
    public static class SettingsExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration) where T : new()
        {
            // Using reflection mechanism to obtain the name of the type
            // and deleting the word "Settings" from the name of that type.
            string section = typeof(T).Name.Replace("Settings", string.Empty);

            // Create a new default configuraiton object.
            T configurationValue = new T();

            // Get the 'section' string and then assign settings
            // from the appsettings*.json file to the object of this class.
            configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}