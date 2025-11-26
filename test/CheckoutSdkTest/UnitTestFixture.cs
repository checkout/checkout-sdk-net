using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace Checkout
{
    /// <summary>
    /// Utility class for creating logger factories in tests with fallback support.
    /// This ensures tests work both locally and in CI/CD environments across all .NET versions.
    /// Uses a singleton pattern to prevent resource leaks.
    /// </summary>
    public static class TestLoggerFactoryHelper
    {
        private static readonly Lazy<DisposableLoggerFactory> _instance = new Lazy<DisposableLoggerFactory>(CreateInstance);
        
        /// <summary>
        /// Gets a singleton instance of ILoggerFactory with fallback support.
        /// First attempts to create NLogLoggerFactory, falls back to basic LoggerFactory if it fails.
        /// </summary>
        /// <returns>A singleton ILoggerFactory instance</returns>
        public static DisposableLoggerFactory Create() => _instance.Value;
        
        private static DisposableLoggerFactory CreateInstance()
        {
            try
            {
                return new DisposableLoggerFactory(new NLogLoggerFactory());
            }
            catch
            {
                return new DisposableLoggerFactory(new LoggerFactory());
            }
        }
    }

    /// <summary>
    /// Wrapper for ILoggerFactory that ensures proper disposal in test scenarios.
    /// This prevents resource leaks in CI/CD environments.
    /// </summary>
    public class DisposableLoggerFactory : ILoggerFactory, IDisposable
    {
        private readonly ILoggerFactory _innerFactory;
        private bool _disposed = false;

        public DisposableLoggerFactory(ILoggerFactory innerFactory)
        {
            _innerFactory = innerFactory ?? throw new ArgumentNullException(nameof(innerFactory));
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DisposableLoggerFactory));
            return _innerFactory.CreateLogger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DisposableLoggerFactory));
            _innerFactory.AddProvider(provider);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_innerFactory is IDisposable disposableFactory)
                {
                    disposableFactory.Dispose();
                }
                _disposed = true;
            }
        }
    }

    public abstract class UnitTestFixture
    {
        /// <summary>
        /// Creates a logger factory with fallback to default implementation if NLog fails
        /// </summary>
        protected static DisposableLoggerFactory CreateLoggerFactory() => TestLoggerFactoryHelper.Create();

        // Previous
        protected static readonly string ValidPreviousPk = System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_PUBLIC_KEY");
        protected static readonly string ValidPreviousSk = System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_SECRET_KEY");
        protected const string InvalidPreviousPk = "pk_test_q414dasds";
        protected const string InvalidPreviousSk = "sk_test_asdsad3q4dq";

        // Default
        protected static readonly string ValidDefaultPk = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_PUBLIC_KEY");
        protected static readonly string ValidDefaultSk = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_SECRET_KEY");
        protected const string InvalidDefaultPk = "pk_sbox_pkh";
        protected const string InvalidDefaultSk = "sk_sbox_m73dzbpy7c-f3gfd46xr4yj5xo4e";
    }
}