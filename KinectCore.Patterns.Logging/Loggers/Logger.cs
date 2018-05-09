using KinectCore.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.Patterns.Logging.Loggers
{
    public abstract class Logger<T> : ILogger<T>
    {
        private readonly log4net.ILog log;

        protected Logger(log4net.Appender.IAppender appender, string logLevel)
        {
            log = log4net.LogManager.GetLogger(typeof(T));
            log4net.Repository.Hierarchy.Logger l = (log4net.Repository.Hierarchy.Logger)log.Logger;
            l.Level = l.Hierarchy.LevelMap[logLevel];
            l.Additivity = true;
            l.AddAppender(appender);
        }
        
        public void WriteError(Exception ex, string message)
        {
            log.Error(message, ex);
        }

        public void WriteInfo(string message)
        {
            log.Info(message);
        }

        public void WriteWarning(string message)
        {
            log.Warn(message);
        }
    }
}
