using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.Shared.Logging
{
    public interface ILogger
    {
        void WriteInfo(string message);

        void WriteWarning(string message);

        void WriteError(Exception ex, string message);
    }

    /// <summary>
    /// A generic interface for logging where the category name is derived from the specified
    /// <typeparamref name="TCategoryName"/> type name.
    /// Generally used to enable activation of a named <see cref="ILogger"/> from dependency injection.
    /// </summary>
    /// <typeparam name="TCategoryName">The type who's name is used for the logger category name.</typeparam>
    public interface ILogger<out TCategoryName> : ILogger
    {

    }
}
