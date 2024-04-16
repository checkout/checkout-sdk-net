using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace Checkout
{
    public sealed class LogProviderTests : IDisposable
    {
        private readonly ILoggerFactory _loggerFactory;

        public LogProviderTests()
        {
            _loggerFactory = new LoggerFactory();
        }

        [Fact]
        public void ShouldGetLoggerReturnsValidLogger()
        {
            ExecuteAndAssertLogger(typeof(LogProviderTests), typeof(AnotherTestClass), typeof(NoInitializedType));
        }

        [Fact]
        public void ShouldGetLoggerReturnsSameLoggerForSameType()
        {
            ExecuteAndAssertSameLogger(typeof(LogProviderTests), typeof(LogProviderTests));
        }

        [Fact]
        public void ShouldGetLoggerReturnsDifferentLoggerForDifferentType()
        {
            ExecuteAndAssertDifferentLogger(typeof(LogProviderTests), typeof(AnotherTestClass));
        }

        [Fact]
        public void ShouldGetLoggerReturnsNullLoggerForNullType()
        {
            var logger = LogProvider.GetLogger(null);
            Assert.Same(NullLogger.Instance, logger);
        }
        
        [Fact]
        public async Task ShouldCreateASingleLoggerInstanceForMultipleConcurrentRequests()
        {
            LogProvider.SetLogFactory(_loggerFactory);
            Type[] loggerTypes = new[] { typeof(LogProviderTests), typeof(AnotherTestClass), typeof(NoInitializedType) };
            Task<ILogger>[] createLoggerTasks = Enumerable.Range(1, 50)
                .Select(async index =>
                {
                    int randomDelayMs = new Random().Next(1, 5);
                    await Task.Delay(randomDelayMs);
                    return await Task.FromResult(LogProvider.GetLogger(loggerTypes[index % loggerTypes.Length]));
                }).ToArray();
            ILogger[] loggers = await Task.WhenAll(createLoggerTasks);
            Assert.Equal(loggerTypes.Length, loggers.Distinct().Count());
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenSetLogFactoryWithNullParameter()
        {
            Assert.Null(Record.Exception(() => LogProvider.SetLogFactory(null)));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _loggerFactory?.Dispose();
            }
        }

        private void ExecuteAndAssertLogger(params Type[] types)
        {
            LogProvider.SetLogFactory(_loggerFactory);

            types.Select(LogProvider.GetLogger)
                .ToList()
                .ForEach(logger =>
                {
                    Assert.NotNull(logger);
                    Assert.IsAssignableFrom<ILogger>(logger);
                });
        }

        private void ExecuteAndAssertSameLogger(params Type[] types)
        {
            LogProvider.SetLogFactory(_loggerFactory);

            var logger0 = LogProvider.GetLogger(types[0]);
            var logger1 = LogProvider.GetLogger(types[1]);

            Assert.Same(logger0, logger1);
        }

        private void ExecuteAndAssertDifferentLogger(params Type[] types)
        {
            LogProvider.SetLogFactory(_loggerFactory);

            var logger0 = LogProvider.GetLogger(types[0]);
            var logger1 = LogProvider.GetLogger(types[1]);
            
            Assert.NotSame(logger0, logger1);
        }
    }

    internal class AnotherTestClass
    {
    }

    internal class NoInitializedType
    {
        private NoInitializedType() { throw new Exception("This class cannot be initialized."); }
    }
}