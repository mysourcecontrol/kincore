using KinectCore.Patterns.Logging.Loggers;
using KinectCore.Shared.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.Patterns.Logging
{
    public static class LoggingServiceExtensions
    {
        public static IServiceCollection AddNewRelicLogger(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<NewRelicLoggingConfiguration>();

            services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(NewRelicLogger<>)));

            return services;
        }
    }
}
