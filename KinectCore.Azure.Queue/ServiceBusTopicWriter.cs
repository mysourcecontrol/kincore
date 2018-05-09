using KinectCore.Shared.Logging;
using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KinectCore.Queue.Azure
{
    public class ServiceBusTopicWriter : IDisposable
    {
        private static ITopicClient client;
        
        public ServiceBusTopicWriter(string connString, string topic, ILogger logger)
        {
            try
            {
                client = new TopicClient(connString, topic); // Ioc injection to maintain static instance
            }
            catch (Exception ex)
            {
                // log messages
            }
        }

        public void Dispose()
        {
            client.CloseAsync().Wait();
        }

        public async Task SendAsync(string message)
        {
            await client.SendAsync(new Message(Encoding.UTF8.GetBytes(message)));
        }
    }
}
