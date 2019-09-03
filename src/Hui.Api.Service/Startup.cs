using AspectCore.Extensions.DependencyInjection;
using Hui.Api.Models.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Hui.Api.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Hui.Api API"
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Hui.Api.Service.xml");

                options.IncludeXmlComments(xmlPath);
                //options.IncludeXmlComments(xmlPathModel);
                options.IgnoreObsoleteActions();
            });
            services.AddMvc();
            // 添加dbcontext
            services.AddDbContext<ApiContext>(opt => opt.UseNpgsql(Configuration["ConnectionStrings:PostgreSql"]));
            //配置AspectCore

            services.AddDiPrivateConfig();
            BootStrapper.Initialize(services, Configuration);
            return services.ConfigureDynamicProxy(p =>
            {
                p.ThrowAspectException = false;
            }).BuildDynamicProxyServiceProvider();

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(option =>
            {
                option.AllowAnyHeader();
                option.AllowAnyMethod();
                option.AllowAnyOrigin();
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {

            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hui.Api.Service API V1");
            });
        }
    }
}
