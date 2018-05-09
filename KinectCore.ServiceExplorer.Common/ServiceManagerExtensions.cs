using KinectCore.ServiceExplorer.Core;
using KinectCore.Shared.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.ServiceExplorer.Core
{
    public static class ServiceManagerExtensions
    {
        public static IServiceCollection AddKse<T>(this IServiceCollection services) where T : IServiceManager
        {
            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<KseConfiguration>();
            if (configuration.EnableSwagger)
            {
                if (configuration.SwaggerInfo == null || string.IsNullOrEmpty(configuration.SwaggerInfo.Version) || string.IsNullOrEmpty(configuration.SwaggerInfo.Title))
                {
                    throw new InvalidOperationException("Unable to find required information to generate Swagger doc");
                }

                // add swagger
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(configuration.SwaggerInfo.Version, new Info { Title = configuration.SwaggerInfo.Title, Version = configuration.SwaggerInfo.Version });
                });
            }

            var logger = sp.GetService<ILogger>();
            if (logger == null)
            {
                services.AddSingleton<ILogger, DefaultLogger>();
            }

            services.AddSingleton(typeof(IServiceManager), typeof(T));

            return services;
        }
        public static IApplicationBuilder UseKse(this IApplicationBuilder builder)
        {
            var configuration = builder.ApplicationServices.GetRequiredService<KseConfiguration>();

            builder.UseStaticFiles();
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{configuration.SwaggerInfo.Version}/swagger.json", configuration.SwaggerInfo.Title);
            });

            var serviceManager = builder.ApplicationServices.GetRequiredService<IServiceManager>();
            if (serviceManager == null)
            {
                throw new InvalidOperationException("Unable to find an instance for IServiceManager");
            }

            serviceManager.Register();

            return builder;
        }
    }
}
