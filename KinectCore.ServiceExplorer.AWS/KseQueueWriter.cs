using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using KinectCore.Queue.AWS;
using KinectCore.ServiceExplorer.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KinectCore.ServiceExplorer.AWS
{
    public class KseQueueWriter : IQueueWriter
    {
        private readonly string accessKey;
        private readonly string secretKey;
        private readonly string queueUrl;

        public KseQueueWriter()
        {
            queueUrl = "https://sqs.us-east-2.amazonaws.com/134352817405/kse_queue";
            accessKey = "AKIAI3AEBVBDOXQYXG6A";
            secretKey = "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";
        }
        
        public async Task SendAsync(string message)
        {
            using (var writer = new SqsQueueWriter(this.accessKey, this.secretKey, RegionEndpoint.USEast2))
            {
                await writer.SendMessage(queueUrl, message);
            }
        }
    }
}
