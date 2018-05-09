using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.Shared.Logging
{
    public class DefaultLogger : ILogger
    {
        public void WriteError(Exception ex, string message)
        {
        }

        public void WriteInfo(string message)
        {
        }

        public void WriteWarning(string message)
        {
        }
    }
}
