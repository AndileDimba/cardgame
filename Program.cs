using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Card Game Assessment Solution
/// This works on Windows
/// </summary>
namespace CardGameAssessment
{
    /// <summary>
    /// Custom exception for game-specific errors.
    /// </summary>
    public class CardGameException : Exception
    {
        public CardGameException(string message) : base(message) { }
        public CardGameException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Represents a playing card.
    /// </summary>
    public class Card
    {
        public string Suit { get; set; } = String.Empty;
        public string Value { get; set; } = String.Empty;
        public int NumericValue { get; set; }
        public int SuitValue { get; set; }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }

    /// <summary>
    /// Represents a player in the game.
    /// </summary>
    public class Player
    {
        public string Name { get; set; } = String.Empty;
        public List<Card> Cards { get; set; } = new List<Card>();
        public int Score { get; set; }
        public long SuitScore { get; set; }

        /// <summary>
        /// Calculates the player's total card score.
        /// </summary>
        public void CalculateScore()
        {
            Score = Cards.Sum(card => card.NumericValue);
        }

        /// <summary>
        /// Calculates the player's suit score for tie-breaking.
        /// </summary>
        public void CalculateSuitScore()
        {
            // Breaking ties
            SuitScore = Cards.Aggregate(1L, (current, card) => current * card.SuitValue);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Card Game...");

                // Initialize players
                var players = Enumerable.Range(1, 6)
                    .Select(i => new Player { Name = $"Player {i}" })
                    .ToList();

                // Create and shuffle deck
                var deck = CreateDeck();

                // Deal cards to players
                DealCards(players, deck);

                // Display each player's hand and score
                Console.WriteLine("\nInitial Hands:");
                foreach (var player in players)
                {
                    Console.WriteLine($"\n{player.Name} (Score: {player.Score}):");
                    foreach (var card in player.Cards)
                        Console.WriteLine($"  {card}");
                }

                // Determine winner(s)
                int maxScore = players.Max(p => p.Score);
                var winners = players.Where(p => p.Score == maxScore).ToList();

                bool tieBreaker = false;

                // Handle tie-breaker if necessary
                if (winners.Count > 1)
                {
                    tieBreaker = true;
                    Console.WriteLine("\nBreaking ties");
                    foreach (var winner in winners)
                    {
                        winner.CalculateSuitScore();
                        Console.WriteLine($"{winner.Name} (Suit Score: {winner.SuitScore})");
                    }

                    long maxSuitScore = winners.Max(w => w.SuitScore);
                    winners = winners.Where(w => w.SuitScore == maxSuitScore).ToList();
                }

                // Display winner(s)
                Console.WriteLine("\nWinner(s):");
                foreach (var winner in winners)
                {
                    Console.WriteLine($"{winner.Name} with score {winner.Score}" +
                        (tieBreaker ? $" and suit score {winner.SuitScore}" : ""));
                }
            }
            catch (CardGameException ex)
            {
                Console.WriteLine($"\nGame Error: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Additional Details: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Creates a deck with two standard 52-card decks plus 2 Jokers.
        /// </summary>
        static List<Card> CreateDeck()
        {
            var deck = new List<Card>();
            var suits = new Dictionary<string, int>
            {
                {"Diamonds", 1},
                {"Hearts", 2},
                {"Spades", 3},
                {"Clubs", 4}
            };
            var values = new Dictionary<string, int>
            {
                {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6},
                {"7", 7}, {"8", 8}, {"9", 9}, {"10", 10},
                {"J", 11}, {"Q", 12}, {"K", 13}, {"A", 11}
            };

            // Add two standard decks
            for (int i = 0; i < 2; i++)
            {
                foreach (var suit in suits)
                {
                    foreach (var value in values)
                    {
                        deck.Add(new Card
                        {
                            Suit = suit.Key,
                            Value = value.Key,
                            NumericValue = value.Value,
                            SuitValue = suit.Value
                        });
                    }
                }
            }

            // Add 2 Jokers (value 14, suit value 1)
            deck.Add(new Card { Suit = "Joker", Value = "Joker", NumericValue = 14, SuitValue = 1 });
            deck.Add(new Card { Suit = "Joker", Value = "Joker", NumericValue = 14, SuitValue = 1 });

            // Shuffle deck
            Shuffle(deck);

            return deck;
        }

        /// <summary>
        /// Deals 5 cards to each player from the deck.
        /// </summary>
        static void DealCards(List<Player> players, List<Card> deck)
        {
            var random = new Random();
            foreach (var player in players)
            {
                player.Cards = new List<Card>();
                for (int i = 0; i < 5; i++)
                {
                    if (deck.Count == 0)
                        throw new CardGameException("Ran out of cards while dealing.");
                    int cardIndex = random.Next(deck.Count);
                    player.Cards.Add(deck[cardIndex]);
                    deck.RemoveAt(cardIndex);
                }
                player.CalculateScore();
            }
        }

        /// <summary>
        /// Shuffles the deck using Fisher-Yates algorithm. (shuffles entire deck once and no list restructuring needed)
        /// </summary>
        static void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}