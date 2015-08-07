using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using DependencyInjectionContainer.DependencyInjection;
namespace DependencyInjectionContainer.Tests
{
    [TestFixture]
    public class DependencyResolverTests
    {
        [Test]
        public void testRegisterTransient()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>();
            Assert.IsTrue(dependencyResolver.getRegisteredTransients().ContainsKey(typeof(ILogger)));
        }

        [Test]
        public void testRegisterSingleton()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>(lifeStyleType.Singleton);
            Assert.IsTrue(dependencyResolver.getRegisteredSingletons().ContainsKey(typeof(ILogger)));
        }

        [Test]
        public void testRegisterSpecificInstanceSingleton()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            ConsoleLogger theLogger = new ConsoleLogger();
            dependencyResolver.Register<ILogger, ConsoleLogger>(theLogger);
            
            Assert.IsTrue(theLogger.getID() == dependencyResolver.Resolve<ILogger>().getID());
        }

        [Test]
        public void testResolveTransient()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>(lifeStyleType.Transient);

            ILogger theLogger = dependencyResolver.Resolve<ILogger>();
            Assert.IsTrue(theLogger.GetType() == typeof(ConsoleLogger));
        }

        [Test]
        public void testResolveSingleton()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>(lifeStyleType.Singleton);

            ILogger theLogger = dependencyResolver.Resolve<ILogger>();
            ILogger theLogger2 = dependencyResolver.Resolve<ILogger>();

            Assert.IsTrue(theLogger.getID() == theLogger2.getID());
        }

        [Test]
        public void testResolveUnregistered()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            try
            {
                dependencyResolver.Resolve<ILogger>();
            }
            catch (InvalidOperationException e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void testRecursiveResolveSingletons()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>(lifeStyleType.Singleton);
            dependencyResolver.Register<ILoggerWrapper, LoggerWrapper>(lifeStyleType.Singleton);

            ILoggerWrapper wrapper = dependencyResolver.Resolve<ILoggerWrapper>();
            ILogger theLogger = dependencyResolver.Resolve<ILogger>();
            Assert.IsTrue(wrapper.getWrappedLogger().getID() == theLogger.getID());
        }

        [Test]
        public void testRecursiveResolveTransients()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>(lifeStyleType.Transient);
            dependencyResolver.Register<ILoggerWrapper, LoggerWrapper>(lifeStyleType.Transient);

            ILoggerWrapper wrapper = dependencyResolver.Resolve<ILoggerWrapper>();
            ILogger theLogger = dependencyResolver.Resolve<ILogger>();
            Assert.IsTrue(wrapper.getWrappedLogger().getID() != theLogger.getID());
        }

        [Test]
        public void testRecursiveResolveSingletonParameter()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>(lifeStyleType.Singleton);
            dependencyResolver.Register<ILoggerWrapper, LoggerWrapper>(lifeStyleType.Transient);

            ILoggerWrapper wrapper = dependencyResolver.Resolve<ILoggerWrapper>();
            ILogger theLogger = dependencyResolver.Resolve<ILogger>();
            Assert.IsTrue(wrapper.getWrappedLogger().getID() == theLogger.getID());
        }

        [Test]
        public void testRecursiveResolveTransientParameter()
        {
            DependencyResolver dependencyResolver = new DependencyResolver();
            dependencyResolver.Register<ILogger, ConsoleLogger>(lifeStyleType.Transient);
            dependencyResolver.Register<ILoggerWrapper, LoggerWrapper>(lifeStyleType.Singleton);

            ILoggerWrapper wrapper = dependencyResolver.Resolve<ILoggerWrapper>();
            ILogger theLogger = dependencyResolver.Resolve<ILogger>();
            Assert.IsTrue(wrapper.getWrappedLogger().getID() != theLogger.getID());
        }
    }
}