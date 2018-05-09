using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.ServiceExplorer.Core
{
    public class KseConfiguration
    {
        public int PollingIntervalInMinutes { get; set; }  
        
        public bool EnableSwagger { get; set; }

        public SwaggerInfo SwaggerInfo { get; set; }
    }

    public class SwaggerInfo
    {
        public string Version { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Endpoint { get; set; }
    }
}
