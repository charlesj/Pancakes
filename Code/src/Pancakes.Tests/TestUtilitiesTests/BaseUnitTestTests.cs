using System;
using Pancakes.Tests.TestUtilities;
using Xunit;

namespace Pancakes.Tests.TestUtilitiesTests
{
    public class BaseUnitTestTests
    {
        public class SystemUnderTest
        {
            [Fact]
            public void SystemUnderTest_AvailableUponInstantiation()
            {
                var sut = new BaseUnitTest<TypeWithInterfaceDependencies>();
                Assert.NotNull(sut.SystemUnderTest);
            }
        }

        public class Build
        {
            [Fact]
            public void TypeWithMultipleConstructors_ThrowsException()
            {
                Assert.Throws<InvalidOperationException>(() => new BaseUnitTest<TypeWithMultipleConstructors>());
            }

            [Fact]
            public void CanBuildType_WithNoDependencies()
            {
                var sut = new BaseUnitTest<TestableTypeNoDependencies>();
                var built = sut.Build();
            }

            [Fact]
            public void CanBuildType_WithInterfaceDependencies()
            {
                var sut = new BaseUnitTest<TypeWithInterfaceDependencies>();
                var built = sut.Build();
            }

            [Fact]
            public void CanBuildType_WithAbstractDependencies()
            {
                var sut = new BaseUnitTest<TypeWithAbstractDependcies>();
                var built = sut.Build();
            }

            [Fact]
            public void ThrowsException_WhenBuildingClassWithInappropriateDependencies()
            {
                Assert.Throws<InvalidOperationException>(() => new BaseUnitTest<TypeWithInappropriateDependencies>());
            }
        }

        public class GetMocked
        {
            [Fact]
            public void CanGetInstance()
            {
                var sut = new BaseUnitTest<TypeWithInterfaceDependencies>();
                var bar = sut.GetMocked<IBarService>();
            }

            [Fact]
            public void Instances_AreSingletons()
            {
                var sut = new BaseUnitTest<TypeWithInterfaceDependencies>();
                var first = sut.GetMocked<IBarService>();
                var second = sut.GetMocked<IBarService>();
                Assert.Equal(first, second);
            }

            [Fact]
            public void Instances_AreSameAsSystemUnderTest()
            {
                var sut = new BaseUnitTest<TypeWithInterfaceDependencies>();
                var first = sut.GetMocked<IFooService>();
                var second = sut.Build().FooService;
                Assert.Equal(first, second);
            }
        }

        public class Mock
        {
            [Fact]
            public void CanGet_TheMock()
            {
                var sut = new BaseUnitTest<TypeWithInterfaceDependencies>();
                var mock = sut.Mock<IFooService>();
                Assert.NotNull(mock);
            }

            [Fact]
            public void CanSetup_TheMock()
            {
                var sut = new BaseUnitTest<TypeWithInterfaceDependencies>();
                var testObject = sut.Build();

                var initial = testObject.FooService.Name();

                var mock = sut.Mock<IFooService>();
                var expected = "hello";
                mock.Setup(foo => foo.Name()).Returns(expected);

                var result = testObject.FooService.Name();
                Assert.NotEqual(expected, initial);
                Assert.Equal(expected, result);
            }
        }

        public class TypeWithInappropriateDependencies
        {
            public TypeWithInappropriateDependencies(string inappropriate)
            { }
        }

        public class TypeWithInterfaceDependencies
        {
            public TypeWithInterfaceDependencies(IFooService fooService, IBarService barService)
            {
                this.FooService = fooService;
            }

            public IFooService FooService { get; private set; }
        }

        public class TypeWithAbstractDependcies
        {
            public TypeWithAbstractDependcies(TestAbstractClass tac)
            { }
        }

        public abstract class TestAbstractClass
        { }

        public class TypeWithMultipleConstructors
        {
            public TypeWithMultipleConstructors()
            {

            }

            public TypeWithMultipleConstructors(IFooService fooService)
            {

            }
        }

        public class TestableTypeNoDependencies
        {
        }

        public interface IFooService
        {
            string Name();
        }

        public interface IBarService
        {
        }
    }
}
