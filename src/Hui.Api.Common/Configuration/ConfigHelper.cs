

namespace Hui.Api.Common.Configuration
{
    using Microsoft.Extensions.Configuration;
    using System;

    public class ConfigHelper
    {
        static ConfigHelper()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(ProcessDirectory)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static string ProcessDirectory
        {
            get
            {
                return AppContext.BaseDirectory;
            }
        }

        public static IConfiguration Configuration { get; }

    }
}
