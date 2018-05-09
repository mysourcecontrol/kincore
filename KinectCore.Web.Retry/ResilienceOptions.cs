using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.Patterns.Resilience
{
    public class ResilienceOptions
    {
        public bool IsRetryPolicyEnabled { get; set; }

        public int RetryCount { get; set; }

        public bool IsCircuitBreakerEnabled { get; set; }

        public int ExceptionsAllowedBeforeBreaking { get; set; }

        public int MinutesCircuitOpenBeforeRetrying { get; set; }
    }
}
