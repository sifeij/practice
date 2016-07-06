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
        }

        static int ShuffleCountWhenEqual(IEnumerable<PlayingCard> startingDeck)
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

        static IEnumerable<PlayingCard> Shuffle(IEnumerable<PlayingCard> deck)
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
                Console.WriteLine(c.ToString());
            }
            return shuffle;
        }

        static IEnumerable<PlayingCard> GetStartingDeck()
        {
            var startingDeck = (
                   from s in Suits().LogQuery("Suit Generation")
                   from r in Ranks().LogQuery("Rank Generation")
                   select new PlayingCard(s, r))
                   .LogQuery("Starting Deck")
                   .ToArray();

            foreach (var card in startingDeck)
            {
                Console.WriteLine($"{card.CardSuit} {card.CardRank}");
            }
            return startingDeck;
        }

        // static IEnumerable<string> Suits()
        // {
        //     yield return "♠";
        //     yield return "♦";
        //     yield return "♥";
        //     yield return "♣";
        // }

        // static IEnumerable<string> Ranks()
        // {
        //     yield return "2";
        //     yield return "3";
        //     yield return "4";
        //     yield return "5";
        //     yield return "6";
        //     yield return "7";
        //     yield return "8";
        //     yield return "9";
        //     yield return "10";
        //     yield return "J";
        //     yield return "Q";
        //     yield return "K";
        //     yield return "A";
        // }

        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        static IEnumerable<Suit> Suits()
        {
            yield return Suit.Clubs;
            yield return Suit.Diamonds;
            yield return Suit.Hearts;
            yield return Suit.Spades;
        }

        public enum Rank
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        static IEnumerable<Rank> Ranks()
        {
            yield return Rank.Two;
            yield return Rank.Three;
            yield return Rank.Four;
            yield return Rank.Five;
            yield return Rank.Six;
            yield return Rank.Seven;
            yield return Rank.Eight;
            yield return Rank.Nine;
            yield return Rank.Ten;
            yield return Rank.Jack;
            yield return Rank.Queen;
            yield return Rank.King;
            yield return Rank.Ace;
        }
    }
}
