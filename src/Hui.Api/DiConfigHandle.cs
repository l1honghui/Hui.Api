using Hui.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hui.Api.Service
{
    public static class DIConfigHandle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDiPrivateConfig(this IServiceCollection services)
        {
            Assembly[] assemblies = {

                Assembly.Load("Hui.Api.Dal")
                ,Assembly.Load("Hui.Api.Bll")

            };

            var types = assemblies
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDependency))))
                .ToArray();

            var tmpTypes = types.Where(_ => _.IsClass);
            var didatas = types.Where(t => t.IsInterface)
                .Select(t => new {
                    serviceType = t,
                    implementationType = tmpTypes.FirstOrDefault(c => c.GetInterfaces().Contains(t))
                }).ToList();

            didatas.ForEach(t =>
            {
                if (t.implementationType != null)
                    services.AddScoped(t.serviceType, t.implementationType);
            });
            return services;
        }
    }
}
