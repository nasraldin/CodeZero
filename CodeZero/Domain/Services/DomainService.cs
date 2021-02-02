//using System;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Abstractions;
//using CodeZero.DependencyInjection;
//using CodeZero.Guids;
//using CodeZero.Linq;
//using CodeZero.MultiTenancy;
//using CodeZero.Timing;

//namespace CodeZero.Domain.Services
//{
//    public abstract class DomainService : IDomainService
//    {
//        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

//        public IServiceProvider ServiceProvider { get; set; }

//        protected IClock Clock => LazyServiceProvider.LazyGetRequiredService<IClock>();

//        public IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);

//        protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();


//        protected IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.LazyGetRequiredService<IAsyncQueryableExecuter>();

//        protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);
//    }
//}
