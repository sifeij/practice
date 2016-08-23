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

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
		Func<TSource, TKey> selector)
        {
            var comparer = Comparer<TKey>.Default;
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var max = sourceIterator.Current;
                var maxKey = selector(max);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, maxKey) > 0)
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                return max;
            }
        }
    }
}