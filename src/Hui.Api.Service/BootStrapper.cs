using Hui.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hui.Api.Common.ServiceProvider;
using Microsoft.Extensions.Configuration;

namespace Hui.Api.Service
{
    /// <summary>
    /// 
    /// </summary>
    public static class BootStrapper
    {
        
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            ServiceProviderHelper.SetServiceProvider(serviceProvider);
        }
    }
}
