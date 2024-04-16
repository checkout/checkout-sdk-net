#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;

namespace Checkout
{
    public static class LogProvider
    {
        private static ILoggerFactory _loggerFactory = new LoggerFactory();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            _loggerFactory?.Dispose();
            _loggerFactory = factory;
        }

        public static ILogger GetLogger(Type loggerType)
        {
            return loggerType is null ? NullLogger.Instance : _loggerFactory.CreateLogger(loggerType);
        }
    }
}
#endif