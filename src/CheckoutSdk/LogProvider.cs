#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Checkout
{
    public static class LogProvider
    {
        private static readonly ConcurrentDictionary<string, Lazy<ILogger>> Loggers = 
            new ConcurrentDictionary<string, Lazy<ILogger>>();
        private static ILoggerFactory _loggerFactory = new LoggerFactory();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            _loggerFactory?.Dispose();
            _loggerFactory = factory;
            Loggers.Clear();
        }

        public static ILogger GetLogger(Type clazz)
        {
            var category = clazz?.FullName;
            return category != null
                ? Loggers.GetOrAdd(category, CreateLogger).Value
                : NullLogger.Instance;
        }

        private static Lazy<ILogger> CreateLogger(string category)
        {
            return new Lazy<ILogger>(
                () => _loggerFactory?.CreateLogger(category)
                      ?? NullLogger.Instance,
                LazyThreadSafetyMode.ExecutionAndPublication);
        }
    }
}
#endif