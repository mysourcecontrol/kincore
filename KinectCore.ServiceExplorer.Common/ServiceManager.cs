using KinectCore.Shared.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KinectCore.ServiceExplorer.Core
{
    public class ServiceManager : IServiceManager
    {
        private readonly ILogger<ServiceManager> logger;
        private readonly IQueueWriter queueWriter;
        private readonly KseConfiguration configuration;

        private ConcurrentBag<IncomingCallMetric> incomingCallMetrics;
        private ConcurrentBag<OutgoingCallMetric> outgoingCallMetrics;

        public ServiceManager(IQueueWriter queueWriter, KseConfiguration configuration, ILogger<ServiceManager> logger)
        {
            this.queueWriter = queueWriter;
            this.configuration = configuration;
            if (this.configuration == null)
            {
                this.configuration = new KseConfiguration();
            }
            this.logger = logger;

            incomingCallMetrics = new ConcurrentBag<IncomingCallMetric>();
            outgoingCallMetrics = new ConcurrentBag<OutgoingCallMetric>();
        }

        public void Register()
        {
            var message = CreateRegistrationMessage();
            this.logger.WriteInfo("Sending registration message to the queue");
            queueWriter.SendAsync(JsonConvert.SerializeObject(message));

            this.logger.WriteInfo("Successfully sent registration message to the queue");

            var tokenSource = new CancellationTokenSource();
            Start(tokenSource.Token);
        }

        public void AddIncomingCallMetric(string method, string path)
        {
            IncomingCallMetric metric = null;
            if (incomingCallMetrics.TryTake(out metric)) {
                metric.Count++;
            }
            else {
                metric = new IncomingCallMetric
                {
                    Destination = path,
                    Method = method,
                    Count = 1
                };
            }
            incomingCallMetrics.Add(metric);
        }

        public void AddOutgoingCallMetric(string method, string path)
        {
            OutgoingCallMetric metric = null;
            if (outgoingCallMetrics.TryTake(out metric))
            {
                metric.Count++;
            }
            else
            {
                metric = new OutgoingCallMetric
                {
                    Destination = path,
                    Method = method,
                    Count = 1
                };
            }
            outgoingCallMetrics.Add(metric);
        }

        private void Start(CancellationToken token)
        {
            this.logger.WriteInfo(string.Format("Starting to report status with a time interval of {0} minutes", configuration.PollingIntervalInMinutes));
            Task.Run(async () => {
                await StartReportingStatusAsync(token);
            }, token);
        }

        /// <summary>
        /// Starts a task that publishes the monitoring status to the queue
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task StartReportingStatusAsync(CancellationToken cancellationToken)
        {
            TimeSpan spanBetweenCalls = TimeSpan.FromMinutes(configuration.PollingIntervalInMinutes);
            while (true)
            {
                // sleep first to give KSE time to register the application
                await Task.Delay(spanBetweenCalls, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await ExecuteMonitorStatusAsync();
                }
                catch (Exception ex)
                {
                    this.logger.WriteError(ex, "Error monitoring the status");
                }
            }
        }

        private async Task ExecuteMonitorStatusAsync()
        {
            var statusMessage = new MonitorMessage(); // get the monitoring message

            await this.queueWriter.SendAsync(JsonConvert.SerializeObject(statusMessage));
        }

        private RegistrationMessage CreateRegistrationMessage()
        {
            return new RegistrationMessage() {
                Version = this.configuration.SwaggerInfo.Version,
                Name = this.configuration.SwaggerInfo.Title,
                Documentation = $"/swagger/{this.configuration.SwaggerInfo.Version}/swagger.json"
            };
        }
    }
}
