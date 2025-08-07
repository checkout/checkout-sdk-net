#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Concurrent;

namespace Checkout
{
    public static class LogProvider
    {
        private static readonly object SyncRoot = new object();
        private static ILoggerFactory _loggerFactory = new LoggerFactory();
        private static readonly ConcurrentDictionary<Type, ILogger> Loggers = new ConcurrentDictionary<Type, ILogger>();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            lock (SyncRoot)
            {
                _loggerFactory?.Dispose();
                _loggerFactory = factory ?? new LoggerFactory();
                Loggers.Clear();
            }
        }

        public static ILogger GetLogger(Type loggerType)
        {
            if (loggerType == null)
            {
                return NullLogger.Instance;
            }

            return Loggers.GetOrAdd(loggerType, type =>
            {
                lock (SyncRoot)
                {
                    var name = type.FullName ?? type.Name;
                    var logger = _loggerFactory.CreateLogger(name);
                    return logger ?? NullLogger.Instance;
                }
            });
        }
    }
}
#endif