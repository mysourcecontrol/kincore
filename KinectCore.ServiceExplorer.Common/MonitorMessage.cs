using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.ServiceExplorer.Core
{
    public class MonitorMessage : KseMessage
    {
        public string Timestamp { get; set; }

        public List<OutgoingCallMetric> CallMetrics { get; set; }

        public List<IncomingCallMetric> InMetrics { get; set; }
    }

    public class OutgoingCallMetric
    {
        public string Method { get; set; }

        public string Destination { get; set; }

        public int Count { get; set; }
    }

    public class IncomingCallMetric
    {
        public string Method { get; set; }

        public string Destination { get; set; }

        public int Count { get; set; }
    }
}
