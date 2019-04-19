using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Hui.Api.Service
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            var loggerFactory = NLogBuilder.ConfigureNLog("nlog.config");

            var hostingEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrEmpty(hostingEnvironment))
            {
                Console.WriteLine($"Setting hosting_environment to {hostingEnvironment}.");
                LogManager.Configuration.Variables["hosting_environment"] = hostingEnvironment;
            }

            var logstashHost = Environment.GetEnvironmentVariable("LOGSTASH_HOST");
            if (!string.IsNullOrEmpty(logstashHost))
            {
                Console.WriteLine($"Setting logstash_host to {logstashHost}.");
                LogManager.Configuration.Variables["logstash_host"] = logstashHost;
            }

            var logger = loggerFactory.GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();
    }
}
