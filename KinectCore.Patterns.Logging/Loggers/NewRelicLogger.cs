using KinectCore.Patterns.Logging.Appenders;
using KinectCore.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.Patterns.Logging.Loggers
{
    public class NewRelicLogger<T> : Logger<T>
    {
        private readonly NewRelicLoggingConfiguration configuration;

        public NewRelicLogger(NewRelicLoggingConfiguration configuration) : base(new NewRelicAppender(), configuration.LogLevel)
        {
        }        
    }
}
