using System;
using System.Collections.Generic;
using System.Linq;

namespace FaroShuffle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var startingDeck = GetStartingDeck();
            var result = ShuffleCountWhenEqual(startingDeck);
            Console.WriteLine($"It needs {result} times to shuffle back as original deck.");
            //GetStartingDeckOrigin();
        }

        static int ShuffleCountWhenEqual<T>(IEnumerable<T> startingDeck)
        {
            var times = 0;
            var shuffle = startingDeck;
            do
            {
                shuffle = Shuffle(shuffle);
                times++;
            } while (!startingDeck.SequenceEquals(shuffle));
            return times;
        }

        static IEnumerable<T> Shuffle<T>(IEnumerable<T> deck)
        {
            var top = deck.Take(26)
                          .LogQuery("Top Half")
                          .ToArray();
            var bottom = deck.Skip(26)
                             .LogQuery("Bottom Half")
                             .ToArray();

            // //in shuffle
            var shuffle = top.InterleaveSequenceWith(bottom)
                             .LogQuery("In Shuffle")
                             .ToArray();

            // //out shuffle
            // var shuffle = bottom.InterleaveSequenceWith(top)
            //                     .LogQuery("Out Shuffle")
            //                     .ToArray();

            foreach (var c in shuffle)
            {
                Console.WriteLine(c);
            }
            return shuffle;
        }

        static IEnumerable<dynamic> GetStartingDeck()
        {
            var startingDeck = (from s in Suits()
                   from r in Ranks()
                   select new { Suit = s, Rank = r })
                   .LogQuery("Starting Deck using ToArray")
                   .ToArray();

            foreach (var card in startingDeck)
            {
                Console.WriteLine($"{card.Suit} {card.Rank}");
            }
            return startingDeck;
        }

        static IEnumerable<string> Suits()
        {
            yield return "♠";
            yield return "♦";
            yield return "♥";
            yield return "♣";
        }

        static IEnumerable<string> Ranks()
        {
            yield return "2";
            yield return "3";
            yield return "4";
            yield return "5";
            yield return "6";
            yield return "7";
            yield return "8";
            yield return "9";
            yield return "10";
            yield return "J";
            yield return "Q";
            yield return "K";
            yield return "A";
        }
    }
}
