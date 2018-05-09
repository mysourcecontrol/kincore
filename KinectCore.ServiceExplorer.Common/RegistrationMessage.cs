using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.ServiceExplorer.Core
{
    public class RegistrationMessage : KseMessage
    {
        public string Documentation { get; set; }

        public string BasePath { get; set; }

        public string StartTimestamp { get; set; }
    }
}
