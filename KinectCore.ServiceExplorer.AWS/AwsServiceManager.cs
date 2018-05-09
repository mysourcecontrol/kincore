using KinectCore.ServiceExplorer.Core;
using KinectCore.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.ServiceExplorer.AWS
{
    public class AwsServiceManager : ServiceManager
    {
        public AwsServiceManager(KseConfiguration configuration, ILogger<AwsServiceManager> logger) : base(new KseQueueWriter(), configuration, logger)
        {
        }
    }
}
