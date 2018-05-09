using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.ServiceExplorer.Core
{
    public class KseMessage
    {
        public string Type { get; set; }

        public string Domain { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string InstanceId { get; set; }
    }
}
