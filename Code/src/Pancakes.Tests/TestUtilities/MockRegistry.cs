using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;

namespace Pancakes.Tests.TestUtilities
{
    public class MockRegistry
    {
        private Dictionary<Type, object> registry;

        public MockRegistry()
        {
            this.registry = new Dictionary<Type, object>();
        }

        public object Get(Type type)
        {
            if (!this.registry.ContainsKey(type))
            {
                var genericMock = typeof(Mock<>).MakeGenericType(type);
                var genericConstructor = genericMock.GetConstructors()[0];
                var instance = genericConstructor.Invoke(new object[0]);
                this.registry.Add(type, ((dynamic)instance).Object);
            }

            return this.registry[type];
        }
    }
}
