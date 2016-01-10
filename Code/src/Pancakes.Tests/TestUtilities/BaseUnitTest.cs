using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;

namespace Pancakes.Tests.TestUtilities
{
    public class BaseUnitTest<TSystemUnderTest>
    {
        private readonly MockRegistry mockRegistry;

        public BaseUnitTest()
        {
            this.mockRegistry = new MockRegistry();
            this.SystemUnderTest = this.Build();
        }

        public TSystemUnderTest SystemUnderTest { get; private set; }

        public TSystemUnderTest Build()
        {
            var constructorParams = new List<object>();
            var constructors = (typeof(TSystemUnderTest)).GetConstructors();
            if (constructors.Length > 1)
                throw new InvalidOperationException("Cannot Test Types with multiple constructors - Code Smell");

            var constructor = constructors[0];
            var parameters = constructor.GetParameters();
            foreach (var parameter in parameters)
            {
                var parametType = parameter.ParameterType;
                if (!parametType.GetTypeInfo().IsAbstract && !parametType.GetTypeInfo().IsInterface)
                    throw new InvalidOperationException("Use more appropriate types (interface or abstract) for your dependencies");

                constructorParams.Add(mockRegistry.Get(parameter.ParameterType));
            }

            var instance = constructor.Invoke(constructorParams.ToArray());
            return (TSystemUnderTest)instance;
        }

        public TMockType GetMock<TMockType>()
        {
            return (TMockType)mockRegistry.Get(typeof(TMockType));
        }

        public Mock<TMockType> Setup<TMockType>() where TMockType : class
        {
            var mockObject = mockRegistry.Get(typeof (TMockType));
            return Mock.Get((TMockType)mockObject);

        }
    }
}
