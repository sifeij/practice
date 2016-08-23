using System;
using System.Collections.Generic;

namespace MoreEffectiveLinq
{
    static class MyLinqExtensions
    {
        public static TimeSpan Sum(this IEnumerable<TimeSpan> times)
        {
            var total = TimeSpan.Zero;
            foreach (var time in times)
            {
                total += time;
            }
            return total;
        }

        public static string Concat(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings);
        }
    }
}