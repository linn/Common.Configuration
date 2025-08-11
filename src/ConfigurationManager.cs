namespace Linn.Common.Configuration
{
    using Extensions;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationManager
    {
        private static readonly object ConfigurationLock = new object();

        private static IConfiguration? configuration;

        public static IConfiguration Configuration
        {
            get
            {
                lock (ConfigurationLock)
                {
                    return configuration ??= new ConfigurationBuilder()
                               .AddEnvironmentVariables()
                               .AddEnvFile("/config.env", true)
                               .AddEnvFile("config.env", true)
                               .Build();
                }
            }
        }
    }
}
