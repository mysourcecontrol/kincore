using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KinectCore.ServiceExplorer.Core
{
    public interface IQueueWriter
    {
        Task SendAsync(string message);
    }
}
