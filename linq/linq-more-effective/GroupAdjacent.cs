using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MoreEffectiveLinq
{
    static partial class MyLinqExtensions
    {
        public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return GroupAdjacent(source, keySelector, null);
        }

        public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");

            return GroupAdjacent(source, keySelector, e => e, comparer);
        }

        public static IEnumerable<IGrouping<TKey, TElement>> GroupAdjacent<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            return GroupAdjacent(source, keySelector, elementSelector, null);
        }

        public static IEnumerable<IGrouping<TKey, TElement>> GroupAdjacent<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");

            return GroupAdjacentImpl(source, keySelector, elementSelector, CreateGroupAdjacentGrouping,
                                     comparer ?? EqualityComparer<TKey>.Default);
        }

        public static IEnumerable<TResult> GroupAdjacent<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            // This should be removed once the target framework is bumped to something that supports covariance
            Func<TKey, IList<TSource>, TResult> resultSelectorWrapper = (key, group) => resultSelector(key, group);

            return GroupAdjacentImpl(source, keySelector, i => i, resultSelectorWrapper,
                                     EqualityComparer<TKey>.Default);
        }

        public static IEnumerable<TResult> GroupAdjacent<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TKey, IEnumerable<TSource>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            
            // This should be removed once the target framework is bumped to something that supports covariance
            Func<TKey, IList<TSource>, TResult> resultSelectorWrapper = (key, group) => resultSelector(key, group);
            return GroupAdjacentImpl(source, keySelector, i => i, resultSelectorWrapper,
                                     comparer ?? EqualityComparer<TKey>.Default);
        }

        static IEnumerable<TResult> GroupAdjacentImpl<TSource, TKey, TElement, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IList<TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            Debug.Assert(source != null);
            Debug.Assert(keySelector != null);
            Debug.Assert(elementSelector != null);
            Debug.Assert(resultSelector != null);
            Debug.Assert(comparer != null);

            using (var iterator = source.GetEnumerator())
            {
                var group = default(TKey);
                var members = (List<TElement>) null;

                while (iterator.MoveNext())
                {
                    var key = keySelector(iterator.Current);
                    var element = elementSelector(iterator.Current);
                    if (members != null && comparer.Equals(group, key))
                    {
                        members.Add(element);
                    }
                    else
                    {
                        if (members != null)
                            yield return resultSelector(group, members);
                        group = key;
                        members = new List<TElement> { element };
                    }
                }

                if (members != null)
                    yield return resultSelector(group, members);
            }
        }

        static IGrouping<TKey, TElement> CreateGroupAdjacentGrouping<TKey, TElement>(TKey key, IList<TElement> members)
        {
            Debug.Assert(members != null);
            return Grouping.Create(key, members.IsReadOnly ? members : new ReadOnlyCollection<TElement>(members));
        }

        static class Grouping
        {
            public static Grouping<TKey, TElement> Create<TKey, TElement>(TKey key, IEnumerable<TElement> members)
            {
                return new Grouping<TKey, TElement>(key, members);
            }
        }

        // #if !NO_SERIALIZATION_ATTRIBUTES
        // [Serializable]
        // #endif
        sealed class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
        {
            readonly IEnumerable<TElement> _members;

            public Grouping(TKey key, IEnumerable<TElement> members)
            {
                Debug.Assert(members != null);
                Key = key;
                _members = members;
            }

            public TKey Key { get; private set; }

            public IEnumerator<TElement> GetEnumerator()
            {
                return _members.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}