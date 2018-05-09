using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace KinectCore.Http.Web
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClient(this IServiceCollection services)
        {
            // Register some message handlers


            // Consider registering HttpClient as a singleton, rather than as transient,
            // if you do not use properties such as BaseAddress on instances of HttpClient.
            // This allows the same instance to be used throughout the application, which
            // improves performance and resource utilisation under heavy server load.
            return services.AddTransient(CreateHttpClient);
        }

        public static IServiceCollection AddSingletonHttpClient(this IServiceCollection services, string baseAddress)
        {
            // Register some message handlers


            // Consider registering HttpClient as a singleton, rather than as transient,
            // if you do not use properties such as BaseAddress on instances of HttpClient.
            // This allows the same instance to be used throughout the application, which
            // improves performance and resource utilisation under heavy server load.
            return services.AddTransient(CreateHttpClient);
        }

        private static HttpClient CreateHttpClient(IServiceProvider service)
        {
            // Create a handler that makes actual HTTP calls
            HttpMessageHandler handler = new HttpClientHandler();

            // Have any delegating handlers been registered?
            var handlers = service.GetServices<DelegatingHandler>().ToList();

            if (handlers.Count > 0)
            {
                // Attach the initial handler to the first delegating handler
                DelegatingHandler previous = handlers.First();
                previous.InnerHandler = handler;

                // Chain any remaining handlers to each other
                foreach (DelegatingHandler next in handlers.Skip(1))
                {
                    next.InnerHandler = previous;
                    previous = next;
                }

                // Replace the initial handler with the last delegating handler
                handler = previous;
            }

            return new System.Net.Http.HttpClient(handler);
        }
    }
}
