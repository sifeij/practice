using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MoreEffectiveLinq
{
    static class MyLinqExtensions
    {
        public static TimeSpan Sum(
            this IEnumerable<TimeSpan> times)
        {
            var total = TimeSpan.Zero;
            foreach (var time in times)
            {
                total += time;
            }
            return total;
        }

        public static string Concat(
            this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings);
        }

        public static TSource MaxBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> selector)
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

        public static IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            var counts = new Dictionary<TKey, int>();
            foreach (var item in source)
            {
                var key = selector(item);
                if (!counts.ContainsKey(key))
                {
                    counts[key] = 1;
                }
                else
                {
                    counts[key]++;
                }
            }
            return counts;
        }

        public static IEnumerable<TResult> Pairwise<TSource, TResult>(
            this IEnumerable<TSource> source, 
            Func<TSource, TSource, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            return PairwiseImpl(source, resultSelector);
        }

        static IEnumerable<TResult> PairwiseImpl<TSource, TResult>(
            this IEnumerable<TSource> source, 
            Func<TSource, TSource, TResult> resultSelector)
        {
            Debug.Assert(source != null);
            Debug.Assert(resultSelector != null);

            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                    yield break;

                var previous = e.Current;
                while (e.MoveNext())
                {
                    yield return resultSelector(previous, e.Current);
                    previous = e.Current;
                }
            }
        }
    }
}