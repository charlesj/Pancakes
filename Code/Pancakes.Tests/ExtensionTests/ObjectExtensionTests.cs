using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Pancakes.Extensions;
using Xunit;

namespace Pancakes.Tests.Extensions
{
    public class ObjectExtensionsTest
    {
        [Fact]
        public void CanConvertEmptyObjectToJson()
        {
            var empty = new EmptyObject();
            var json = empty.ToJson();
            Assert.Equal("{}", json);
        }

        [Fact]
        public void CanConvertSimpleObjectToJson()
        {
            var simple = new SimpleObject { Count = 42 };
            var json = simple.ToJson();
            Assert.Equal("{\"Count\":42}", json);
        }

        public class EmptyObject { }

        public class SimpleObject
        {
            public int Count { get; set; }
        }
    }
}
