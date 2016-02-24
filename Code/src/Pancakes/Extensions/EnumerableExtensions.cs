using System;
using System.Collections.Generic;
using System.Linq;

namespace Pancakes.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static List<T> Evaluate<T>(this IEnumerable<T> source)
        {
            return source.ToList();
        }
    }
}
