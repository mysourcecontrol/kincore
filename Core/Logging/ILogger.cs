using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Logging
{
    public interface ILogger
    {
        void WriteInfo(string message);

        void WriteWarning(string message);

        void WriteError(string message);
    }
}
