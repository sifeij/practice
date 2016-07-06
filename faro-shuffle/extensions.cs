using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FaroShuffle
{
    public static class Extensions
    {
        public static IEnumerable<T> InterleaveSequenceWith<T>
                (this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                yield return firstIter.Current;
                yield return secondIter.Current;
            }
        }
        
        public static bool SequenceEquals<T>
                (this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while(firstIter.MoveNext() && secondIter.MoveNext())
            {
                if(!firstIter.Current.Equals(secondIter.Current))
                {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<T> LogQuery<T>
                (this IEnumerable<T> sequence, string tag)
        {
            using (var writer = File.AppendText("debug.log"))
            {
                DateTime localDate = DateTime.Now;
                var culture = new CultureInfo("en-US");
                writer.WriteLine($"{localDate.ToString(culture)} Excecuting Query {tag}");
            }
            return sequence;
        }
    }
}