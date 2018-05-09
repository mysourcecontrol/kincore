using KinectCore.Shared.Logging;
using Polly;
using System;
using System.Collections.Generic;

namespace KinectCore.Patterns.Resilience
{
    public class PollyPolicyFactory
    {
        private readonly ILogger logger;
        private readonly ResilienceOptions options;

        public PollyPolicyFactory(ResilienceOptions options, ILogger logger)
        {
            this.options = options;
            this.logger = logger;
        }

        public Policy[] CreatePolicies<T>() where T : Exception
        {
            var policies = new List<Policy>();

            if (options.IsRetryPolicyEnabled) { policies.Add(CreateRetryPolicy<T>()); }
            if (options.IsCircuitBreakerEnabled) { policies.Add(CreateCircuitBreakerPolicy<T>()); }

            return policies.ToArray();
        }

        /// <summary>
        /// Creates a retry policy using exponential backoff
        /// </summary>
        /// <typeparam name="T">Exception type used for the policy</typeparam>
        /// <returns>Retry policy</returns>
        private Policy CreateRetryPolicy<T>() where T : Exception
        {
            return Policy.Handle<T>()
                    .WaitAndRetryAsync(
                        // number of retries
                        options.RetryCount,
                        // exponential backofff
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        // on retry
                        (ex, timeSpan, retryCount, context) => 
                        {
                            string message = $"Retry {retryCount} implemented due to {ex}";
                            this.logger.WriteInfo(message);
                        }
                   );        
        }

        /// <summary>
        /// Creates a circuit breaker policy using specified configuration
        /// </summary>
        /// <typeparam name="T">Exception type used for the policy</typeparam>
        /// <returns>Circuit breaker policy</returns>
        private Policy CreateCircuitBreakerPolicy<T>() where T : Exception
        {
            return Policy.Handle<T>()
                    .CircuitBreakerAsync(
                        // exceptions allowed
                        options.ExceptionsAllowedBeforeBreaking,
                        // minutes before retrying
                        TimeSpan.FromMinutes(options.MinutesCircuitOpenBeforeRetrying),
                        (ex, duration) =>
                        {
                            // on circuit opened
                            this.logger.WriteInfo("Circuit breaker opened");
                        },
                        () =>
                        {
                            // on circuit closed
                            this.logger.WriteInfo("Circuit breaker reset");
                        }
                    );                   
        }
    }
}
