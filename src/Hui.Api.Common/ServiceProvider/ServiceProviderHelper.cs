using System;
using Microsoft.Extensions.DependencyInjection;

namespace Hui.Api.Common.ServiceProvider
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceProviderHelper
    {
        private static IServiceProvider _serviceProvider;
         
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
         
        public static IServiceScope CreateServiceScope()
        {
            return _serviceProvider.CreateScope();
        }
         
        public static IServiceProvider CreateServiceProvider()
        {
            return CreateServiceScope().ServiceProvider;
        }
         
        /// <summary>
        /// 获取服务，未找到抛异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRequiredService<T>()
            where T : class
        {
            return CreateServiceProvider().GetRequiredService<T>();
        }
         
        /// <summary>
        /// 获取服务，未找到返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
            where T : class
        {
            return CreateServiceProvider().GetService<T>();
        }
    }
}