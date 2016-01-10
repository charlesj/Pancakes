using Pancakes.ServiceLocator;
using SimpleInjector;
using Xunit;

namespace Pancakes.Tests.ServiceLocatorTests
{
    public class SimpleInjectorServiceLocatorTests
    {
        public class Get : SimpleInjectorServiceLocatorTests
        {
            [Fact]
            public void CanGetService()
            {
                var container = new Container();
                container.Register<IFooService, FooService>();

                var locator = new SimpleInjectorServiceLocator(container);
                var service = locator.GetService(typeof (IFooService));
                Assert.NotNull(service);
            }

            [Fact]
            public void CanGetService_WithGenericMethod()
            {
                var container = new Container();
                container.Register<IFooService, FooService>();

                var locator = new SimpleInjectorServiceLocator(container);
                var service = locator.GetService<IFooService>();
                Assert.NotNull(service);
            }

            [Fact]
            public void CanGetService_Directly()
            {
                var container = new Container();
                var locator = new SimpleInjectorServiceLocator(container);
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
    }
}