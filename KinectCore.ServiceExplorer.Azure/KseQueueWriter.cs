using KinectCore.Queue.Azure;
using KinectCore.ServiceExplorer.Core;
using KinectCore.Shared.Logging;
using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KinectCore.ServiceExplorer.Azure
{
    public class KseQueueWriter : IQueueWriter
    {
        private string topicName = "KSE";
        private readonly string connectionString;
        private readonly ServiceBusTopicWriter queueWriter;

        public KseQueueWriter()
        {
            connectionString = "";
        }

        public async Task SendAsync(string message)
        {
            await queueWriter.SendAsync(message);
        }
    }
}
