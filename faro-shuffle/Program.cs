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
            Shuffle(startingDeck);
            //GetStartingDeckOrigin();
        }


        static IEnumerable<dynamic> GetStartingDeck()
        {
            var startingDeck = from s in Suits()
                   from r in Ranks()
                   select new { Suit = s, Rank = r };

            foreach (var card in startingDeck)
            {
                Console.WriteLine($"{card.Suit} {card.Rank}" );
            }
            return startingDeck;
        }

        static void Shuffle<T>(IEnumerable<T> startingDeck)
        {
            var top = startingDeck.Take(26);
            var bottom = startingDeck.Skip(26);

            var shuffle = top.InterleaveSequenceWith(bottom);
            foreach (var c in shuffle)
            {
                Console.WriteLine(c);
            }
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

        // static void GetStartingDeckOrigin()
        // {
        //     var startingDeck = from s in SuitsOrigin()
        //            from r in RanksOrigin()
        //            select new { Suit = s, Rank = r };

        //     foreach (var card in startingDeck)
        //     {
        //         Console.WriteLine($"{card.Suit} {card.Rank}" );
        //     }
        // }

        // static IEnumerable<string> SuitsOrigin()
        // {
        //     yield return "clubs";
        //     yield return "diamonds";
        //     yield return "hearts";
        //     yield return "spades";
        // }

        // static IEnumerable<string> RanksOrigin()
        // {
        //     yield return "two";
        //     yield return "three";
        //     yield return "four";
        //     yield return "five";
        //     yield return "six";
        //     yield return "seven";
        //     yield return "eight";
        //     yield return "nine";
        //     yield return "ten";
        //     yield return "jack";
        //     yield return "queen";
        //     yield return "king";
        //     yield return "ace";
        // }
    }
}
