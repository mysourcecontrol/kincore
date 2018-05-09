using KinectCore.ServiceExplorer.Core;
using KinectCore.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.ServiceExplorer.Azure
{
    public class AzureServiceManager : ServiceManager
    {
        public AzureServiceManager(KseConfiguration configuration, ILogger<AzureServiceManager> logger) : base(new KseQueueWriter(), configuration, logger)
        {
        }
    }
}
