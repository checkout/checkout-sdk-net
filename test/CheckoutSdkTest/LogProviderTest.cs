using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace Checkout
{
    [Collection("NonParallel")]
    public sealed class LogProviderTests : IDisposable
    {
        private readonly ILoggerFactory _loggerFactory;
        
        private static readonly object RandLock = new object();
        private static readonly Random Random = new Random();

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
        public async Task ShouldCreateASingleLoggerInstanceForMultipleConcurrentRequests()
        {
            LogProvider.SetLogFactory(_loggerFactory);
            Type[] loggerTypes = { typeof(LogProviderTests), typeof(AnotherTestClass), typeof(NoInitializedType) };

            Task<ILogger>[] createLoggerTasks = Enumerable.Range(1, 50)
                .Select(async index =>
                {
                    int delay;
                    lock (RandLock)
                    {
                        delay = Random.Next(1, 5);
                    }
                    await Task.Delay(delay);
                    return await Task.FromResult(LogProvider.GetLogger(loggerTypes[index % loggerTypes.Length]));
                }).ToArray();

            ILogger[] loggers = await Task.WhenAll(createLoggerTasks);
            Assert.Equal(loggerTypes.Length, loggers.Distinct().Count());
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
        public void ShouldNotThrowExceptionWhenSetLogFactoryWithNullParameter()
        {
            Assert.Null(Record.Exception(() => LogProvider.SetLogFactory(null)));
        }
        
        [Fact]
        public void ShouldReplaceLoggerFactoryCorrectly()
        {
            var loggerBefore = LogProvider.GetLogger(typeof(LogProviderTests));

            var newFactory = LoggerFactory.Create(builder => builder.AddFilter(_ => false));
            LogProvider.SetLogFactory(newFactory);

            var loggerAfter = LogProvider.GetLogger(typeof(LogProviderTests));

            Assert.NotSame(loggerBefore, loggerAfter);
        }
        
        [Fact]
        public void ShouldClearLoggersWhenFactoryChanges()
        {
            LogProvider.SetLogFactory(_loggerFactory);
            var logger1 = LogProvider.GetLogger(typeof(LogProviderTests));

            var newFactory = new LoggerFactory();
            LogProvider.SetLogFactory(newFactory);
            var logger2 = LogProvider.GetLogger(typeof(LogProviderTests));

            Assert.NotSame(logger1, logger2);
        }
        
        [Fact]
        public void ShouldReturnSameLoggerOnMultipleCalls()
        {
            LogProvider.SetLogFactory(_loggerFactory);

            var logger1 = LogProvider.GetLogger(typeof(LogProviderTests));
            var logger2 = LogProvider.GetLogger(typeof(LogProviderTests));

            Assert.Same(logger1, logger2);
        }
        
        [Fact]
        public async Task ShouldNotThrowWhenCallingSetLogFactoryConcurrently()
        {
            var tasks = Enumerable.Range(0, 10).Select(_ => Task.Run(() =>
            {
                LogProvider.SetLogFactory(new LoggerFactory());
            }));

            var exception = await Record.ExceptionAsync(async () => await Task.WhenAll(tasks));

            Assert.Null(exception);
        }
        
        [Fact]
        public void ShouldAllowMultipleNullLoggerFactoryAssignments()
        {
            LogProvider.SetLogFactory(null);
            LogProvider.SetLogFactory(null);
            var logger = LogProvider.GetLogger(typeof(LogProviderTests));
    
            Assert.NotNull(logger);
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