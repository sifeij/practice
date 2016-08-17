using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Queries
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var numbers = MyLinq.Random()
                                .Where(n => n > 0.5)
                                .Take(10).OrderBy(n => n);
            foreach (var number in numbers)
            {
                WriteLine(number);
            }

            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",   Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie { Title = "Casablanca",        Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V",       Rating = 8.7f, Year = 1980 }
            };

            // 1. When defered execution hurts the operation:
            // Count() method - it is not a defered execution,
            // it should use ToList() method before counting to avoid 
            // double calling into yield return
            var query0 = movies.Where(m => m.Year >= 2000)
                               .ToList();

            WriteLine("Number of movies are after year of 2000: " + query0.Count());

            // 2. Error Handling
            // when only use try catch block on defered execution operations
            // it may not throw error till using the filtered items
            // Better to wrap try catch block on real operation instead of just filter operation


            // 3. Deferred Non-Streaming Execution
            // when using OrderBy() or OrderByDescending methods, it is looking through all items
            // in sequence as if it has not being filtered by Where()
            // should be careful when use it against big sequence
            // https://msdn.microsoft.com/en-us/library/mt693095.aspx
            var query1 = movies.Where(m => m.Year >= 2000)
                               .OrderBy(m => m.Title);
            

            var enumerator1 = query1.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                WriteLine(enumerator1.Current.Title 
                            + " - " 
                            + enumerator1.Current.Year);
            }

            var query = from movie in movies
                        where movie.Year > 2000
                        orderby movie.Rating descending
                        select movie;

            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                WriteLine(enumerator.Current.Title);
            }
        }
    }
}
