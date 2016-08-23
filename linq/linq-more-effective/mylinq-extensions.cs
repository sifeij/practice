using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
            this IEnumerable<TSource> source, int size)
        {
            return Batch(source, size, x => x);
        }
        
        public static IEnumerable<TResult> Batch<TSource, TResult>(
            this IEnumerable<TSource> source, int size,
            Func<IEnumerable<TSource>, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (size <= 0) throw new ArgumentOutOfRangeException("size");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            return BatchImpl(source, size, resultSelector);
        }

        static IEnumerable<TResult> BatchImpl<TSource, TResult>(
            this IEnumerable<TSource> source, int size,
            Func<IEnumerable<TSource>, TResult> resultSelector)
        {
            Debug.Assert(source != null);
            Debug.Assert(size > 0);
            Debug.Assert(resultSelector != null);

            TSource[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                {
                    bucket = new TSource[size];
                }

                bucket[count++] = item;

                // The bucket is fully buffered before it's yielded
                if (count != size)
                {
                    continue;
                }

                // Select is necessary so bucket contents are streamed too
                yield return resultSelector(bucket.Select(x => x));
               
                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                yield return resultSelector(bucket.Take(count));
            }
        }
    }
}