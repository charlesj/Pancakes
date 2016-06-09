using Pancakes.ServiceLocator;
using SimpleInjector;
using Xunit;

namespace Pancakes.Tests.ServiceLocatorTests
{
    public class SimpleInjectorServiceLocatorTests
    {
        public class RegisterServicess : SimpleInjectorServiceLocatorTests
        {
            [Fact]
            public void RegisterServices_CanBeRetrieved()
            {
                var locator = new SimpleInjectorServiceLocator();
                locator.RegisterServices(new IServiceRegistration[] {new TestServiceRegistration()});
                var service = locator.GetService<IFooService>();
                Assert.NotNull(service);
            }
        }

        public class Get : SimpleInjectorServiceLocatorTests
        {
            [Fact]
            public void CanGetIServiceLocator()
            {
                var locator = new SimpleInjectorServiceLocator();
                locator.RegisterServices(new IServiceRegistration[] { });
                var self = locator.GetService<IServiceLocator>();

                Assert.Same(locator, self);
            }

            [Fact]
            public void CanGetService()
            {
                var locator = new SimpleInjectorServiceLocator();
                locator.RegisterServices(new IServiceRegistration[] { new TestServiceRegistration() });
                var service = locator.GetService(typeof (IFooService));
                Assert.NotNull(service);
            }

            [Fact]
            public void CanGetService_WithGenericMethod()
            {
                var locator = new SimpleInjectorServiceLocator();
                locator.RegisterServices(new IServiceRegistration[] { new TestServiceRegistration() });
                var service = locator.GetService<IFooService>();
                Assert.NotNull(service);
            }

            [Fact]
            public void CanGetService_Directly()
            {
                var locator = new SimpleInjectorServiceLocator();
                locator.RegisterServices(new IServiceRegistration[] { });
                var service = locator.GetService<FooService>();
                Assert.NotNull(service);
            }
        }

        public interface IFooService
        {
            string GetName();
        }

        public class FooService : IFooService
        {
            public string GetName()
            {
                return "Fozzy";
            }
        }

        public class TestServiceRegistration : IServiceRegistration
        {
            public void RegisterServices(Container container)
            {
                container.Register<IFooService, FooService>();
            }
        }
    }
}