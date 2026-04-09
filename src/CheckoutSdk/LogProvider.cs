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

        // Replaced (not cleared) on every SetLogFactory call so that in-flight GetLogger
        // calls that already snapshotted the old reference continue to see a stable,
        // consistent cache and always return the same ILogger instance for the same type.
        private static volatile ConcurrentDictionary<Type, Lazy<ILogger>> _loggers =
            new ConcurrentDictionary<Type, Lazy<ILogger>>();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            lock (SyncRoot)
            {
                _loggerFactory?.Dispose();
                _loggerFactory = factory ?? new LoggerFactory();
                // Replace instead of Clear: threads already holding a reference to the
                // old dictionary are unaffected and will keep returning consistent instances.
                _loggers = new ConcurrentDictionary<Type, Lazy<ILogger>>();
            }
        }

        public static ILogger GetLogger(Type loggerType)
        {
            if (loggerType == null)
            {
                return NullLogger.Instance;
            }

            // Snapshot both the cache and the factory atomically so that this call
            // always uses a consistent (loggers, factory) pair regardless of any
            // concurrent SetLogFactory invocation.
            ConcurrentDictionary<Type, Lazy<ILogger>> loggers;
            ILoggerFactory factory;
            lock (SyncRoot)
            {
                loggers = _loggers;
                factory = _loggerFactory;
            }

            // LazyThreadSafetyMode.ExecutionAndPublication guarantees the value factory
            // is invoked at most once per Lazy instance, even under concurrent access.
            // Combined with GetOrAdd storing only one Lazy per key, every caller for the
            // same type receives the exact same ILogger reference.
            return loggers.GetOrAdd(loggerType, type =>
                new Lazy<ILogger>(() =>
                {
                    var name = type.FullName ?? type.Name;
                    return factory.CreateLogger(name) ?? NullLogger.Instance;
                }, LazyThreadSafetyMode.ExecutionAndPublication)
            ).Value;
        }
    }
}
#endif