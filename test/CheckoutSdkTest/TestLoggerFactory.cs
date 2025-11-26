using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Checkout
{
    /// <summary>
    /// Utility class for creating logger factories in tests with fallback support.
    /// This ensures tests work both locally and in CI/CD environments.
    /// </summary>
    public static class TestLoggerFactory
    {
        /// <summary>
        /// Creates an ILoggerFactory with fallback support.
        /// First attempts to create NLogLoggerFactory, falls back to basic LoggerFactory if it fails.
        /// </summary>
        /// <returns>A working ILoggerFactory instance</returns>
        public static ILoggerFactory Create()
        {
            try
            {
                return new NLogLoggerFactory();
            }
            catch
            {
                return new LoggerFactory();
            }
        }
    }
}