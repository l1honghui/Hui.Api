using AspectCore.DynamicProxy;
using Hui.Api.Common.ServiceProvider;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Hui.Api.Common.Attributes
{
    /// <summary>
    /// 接口耗时监控Aop实现
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AspectStopWatchAttribute : AbstractInterceptorAttribute
    {
        private const long WarningMilliseconds = 1000;

        private readonly ILogger<AspectStopWatchAttribute> _logger = ServiceProviderHelper.GetService<ILogger<AspectStopWatchAttribute>>();

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();
            await next(context);
            stopWatch.Stop();
            var timeElapsed = stopWatch.ElapsedMilliseconds;
            var methodInfo =
                $"{context.ServiceMethod.DeclaringType?.Namespace}.{context.ServiceMethod.DeclaringType?.Name}.{context.ServiceMethod.Name}";
            var logMsg =
                $"--->>> MethodInfo:{methodInfo},Invoke ElapsedMilliseconds:{timeElapsed}ms，MethodInfo:{methodInfo}";
            if (timeElapsed > WarningMilliseconds)
            {
                _logger.LogWarning(logMsg);
            }
            else
            {
                _logger.LogInformation(logMsg);
            }
        }
    }
}
