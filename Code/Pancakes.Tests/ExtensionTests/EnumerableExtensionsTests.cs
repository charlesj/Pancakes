using Pancakes.Extensions;
using System.Collections.Generic;
using Xunit;

namespace Pancakes.Tests.ExtensionTests
{
    public class EnumerableExtensionsTests
    {
        private static readonly string[] Data = { "hello", "world" };
        public class Evaluate
        {
            [Fact]
            public void EvaluateWrapsToList()
            {
                var list = Data.Evaluate();
                Assert.IsType<List<string>>(list);
            }
        }

        public class ForEach
        {
            [Fact]
            public void CanCallForEachWithEmptyAction()
            {
                Data.Each(datum => { });
            }

            [Fact]
            public void ClosureWorksAsExpected()
            {
                var list = new List<string>();
                Data.Each(datum => list.Add(datum));
                Assert.Collection(list,
                    datum => Assert.Equal(Data[0], datum),
                    datum => Assert.Equal(Data[1], datum));
            }
        }
    }
}
