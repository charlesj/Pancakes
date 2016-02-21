using System.Reflection;
using Pancakes.Tests.TestUtilities;
using Pancakes.Utility;
using Xunit;

namespace Pancakes.Tests.UtilityTests
{
    public class AssemblyCollectionTests : BaseUnitTest<AssemblyCollection>
    {
        [Fact]
        public void CanAddType()
        {
            var assembly = typeof (Foo).GetTypeInfo().Assembly;
            SystemUnderTest.Add(assembly);
        }

        [Fact]
        public void CanGetTypesFromCollectionImplementingInterface()
        {
            var assembly = typeof(Foo).GetTypeInfo().Assembly;
            SystemUnderTest.Add(assembly);
            var types = SystemUnderTest.GetTypesImplementing(typeof (IFoo));
            Assert.Collection(types, item => Assert.Equal(typeof(Foo), item));
        }

        public class Foo : IFoo
        {
        }

        public interface IFoo
        {
             
        }
    }

    
}
