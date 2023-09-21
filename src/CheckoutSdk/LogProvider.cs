#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;

namespace Checkout
{
    public static class LogProvider
    {
        private static IDictionary<string, ILogger> _loggers = new Dictionary<string, ILogger>();
        private static ILoggerFactory _loggerFactory = new LoggerFactory();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            _loggerFactory?.Dispose();
            _loggerFactory = factory;
            _loggers.Clear();
        }

        public static ILogger GetLogger(Type clazz)
        {
            var category = clazz.FullName;
            if (category != null && !_loggers.ContainsKey(category))
            {
                _loggers[category] = _loggerFactory?.CreateLogger(category) ?? NullLogger.Instance;
            }

            return category is null ? NullLogger.Instance : _loggers[category];
        }
    }
}
#endif