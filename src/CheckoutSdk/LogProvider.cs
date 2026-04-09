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
        private static readonly object SyncRoot = new object();
        private static ILoggerFactory _loggerFactory = new LoggerFactory();

        // Replaced on every SetLogFactory call. Declared volatile so every GetLogger
        // call reads the most current dictionary without needing a lock.
        private static volatile ConcurrentDictionary<Type, Lazy<ILogger>> _loggers =
            new ConcurrentDictionary<Type, Lazy<ILogger>>();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            lock (SyncRoot)
            {
                _loggerFactory?.Dispose();
                _loggerFactory = factory ?? new LoggerFactory();
                // Replace the cache so new GetLogger calls start fresh with the new factory.
                // The old dictionary is abandoned; any concurrent GetOrAdd call already in
                // flight against it will complete safely (ConcurrentDictionary is fully
                // thread-safe) and its result will simply be discarded on the next call.
                _loggers = new ConcurrentDictionary<Type, Lazy<ILogger>>();
            }
        }

        public static ILogger GetLogger(Type loggerType)
        {
            if (loggerType == null)
            {
                return NullLogger.Instance;
            }

            // Read the current cache directly (volatile field — atomic on all platforms).
            // All concurrent GetLogger calls that see the same _loggers reference share one
            // ConcurrentDictionary, so GetOrAdd returns the same Lazy<ILogger> per key and
            // LazyThreadSafetyMode.ExecutionAndPublication ensures a single ILogger is created.
            // If SetLogFactory replaces _loggers between two calls, those calls use different
            // dictionaries; that is acceptable because the factory itself changed.
            return _loggers.GetOrAdd(loggerType, type =>
                new Lazy<ILogger>(() =>
                {
                    ILoggerFactory factory;
                    lock (SyncRoot)
                    {
                        factory = _loggerFactory;
                    }

                    var name = type.FullName ?? type.Name;
                    return factory.CreateLogger(name) ?? NullLogger.Instance;
                }, LazyThreadSafetyMode.ExecutionAndPublication)
            ).Value;
        }
    }
}
#endif