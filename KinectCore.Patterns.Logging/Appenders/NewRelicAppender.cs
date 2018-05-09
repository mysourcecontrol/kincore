using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KinectCore.Patterns.Logging.Appenders
{
    public class NewRelicAppender : AppenderSkeleton
    {
        //protected override void Append(LoggingEvent loggingEvent)
        //{
        //    IDictionary<string, string> parameters = new Dictionary<string, string>();

        //    parameters.Add("Level", loggingEvent.Level.ToString());
        //    parameters.Add("LoggerName", loggingEvent.LoggerName);
        //    parameters.Add("ThreadName", loggingEvent.ThreadName);
        //    parameters.Add("Machine", Environment.MachineName);
        //    parameters.Add("Identity", loggingEvent.Identity);

        //    global::NewRelic.Api.Agent.NewRelic.NoticeError(loggingEvent.MessageObject.ToString(), parameters);
        //}

        private static HttpClient client = new HttpClient();
        private string applicationJson = "application/json";
        public string ApiEndPoint { get; set; }
        private string InsertKey {
            get {
                return "055ee00f049fc45f5bc24a5e42ee80c0747fa1f3";
            }
        }
        private Uri Uri { get; set; }

        public NewRelicAppender()
        {
            //client = new HttpClient();
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();

            Uri = new Uri(ApiEndPoint);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(applicationJson));
            client.DefaultRequestHeaders.Add("X-Insert-Key", InsertKey);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var payload = this.RenderLoggingEvent(loggingEvent);
            PostEvent(payload).Wait();
        }

        private async Task PostEvent(string payload)
        {
            try
            {
                var content = new StringContent(payload, System.Text.Encoding.UTF8, applicationJson);
                var response = await client.PostAsync(Uri, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                this.ErrorHandler.Error($"Unable to send logging event to remote host {this.ApiEndPoint}", ex);
            }
        }
    }
}
