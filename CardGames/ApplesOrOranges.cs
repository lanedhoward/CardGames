using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGames.ConsoleUtils;

namespace CardGames
{
    class ApplesOrOranges : Game
    {
        private int score = 0;
        private Deck deck;
        private string currentSuit = "";
        private string currentFullName = "";
        private bool guessedWouldStaySame = false;
        public ApplesOrOranges()
        {
            Name = "Apples or Oranges";
            Players = 1;
            CardsInDeck = 26;
            
        }

        public override void Run()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;

            ShowInstructions();
            SetUpGame();

            Print("\tThe dealer shuffles the deck.");
            deck.Shuffle();
            Print("\tThe dealer cuts the deck.");
            deck.Cut();

            Print("Dealer: Let us begin.");
            Print("Dealer: I drew ");
            Card c = deck.Draw();
            currentSuit = c.Suit;
            currentFullName = c.ReadFullName();
            Print("\t" + currentFullName);

            for (int i = 0; i < CardsInDeck-1-1; i++) // -1 for zero-indexing, -1 for card already drawn
            {

                Print("Dealer: The last card was the " + currentFullName);
                Print("Dealer: Do you think the next card will be the same suit?");
                guessedWouldStaySame = GetInputBool();
                Print("Dealer: I drew ");
                c = deck.Draw();
                
                bool sameSuit;
                if (c.Suit == currentSuit) sameSuit = true; else sameSuit = false;

                currentSuit = c.Suit;
                currentFullName = c.ReadFullName();
                Print("\t" + currentFullName);

                if (guessedWouldStaySame == sameSuit)
                {
                    Print("Dealer: You were correct. +1 point.");
                    score++;
                }
                else
                {
                    Print("Dealer: Better luck next time.");
                }
                Print("Dealer: Your score is " + score);
                WaitForKeyPress(true);
                Console.Clear();

                //every loop has to get input, draw next card, see if guess was right.

            }

            Print("Dealer: Your final score is " + score);
            Print("Dealer: Thanks for playing.");
            WaitForKeyPress(true);
            Console.Clear();

        }

        public override void ShowInstructions()
        {
            Print("A card will be drawn from a two-suit deck. You will guess whether the next card drawn will be the same suit or not.");
            Print("Every correct guess wins a point. Play until you run out of cards in the deck.");
            WaitForKeyPress(true);
            Console.Clear();
        }

        public void SetUpGame()
        {
            deck = new Deck(CardsInDeck, new string[] { "Apples", "Oranges" });
        }
    }
}
