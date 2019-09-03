using System.Linq;
using System.Reflection;
using Hui.Api.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Hui.Api.Service
{
    /// <summary>
    /// 
    /// </summary>
    public static class DiServiceCollectionExtensions
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
            var diTypes = types.Where(t => t.IsInterface)
                .Select(t => new {
                    serviceType = t,
                    implementationType = tmpTypes.FirstOrDefault(c => c.GetInterfaces().Contains(t))
                }).ToList();

            diTypes.ForEach(t =>
            {
                if (t.implementationType != null)
                    services.AddScoped(t.serviceType, t.implementationType);
            });
            return services;
        }
    }
}