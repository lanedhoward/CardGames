using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGames.ConsoleUtils;

namespace CardGames
{
    class HigherOrLower : Game
    {
        private int score = 0;
        private Deck deck;
        private int currentValue = 0;
        private string currentFullName = "";
        private bool guessedWouldBeHigher = false;
        public HigherOrLower()
        {
            Name = "Higher or Lower";
            Players = 1;
            CardsInDeck = 52;
            
        }

        public override void Run()
        {
            
            Console.ForegroundColor = ConsoleColor.Red;
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
            currentValue = c.NumberValue;
            currentFullName = c.ReadFullName();
            Print("\t" + currentFullName);

            int round = 1;

            for (int i = 0; i < 26-1; i++) // 26 bc thats how long ApplesOrOranges is and that is a good length
            {
                if (round != 1) Print($"\tRound {round}");
                Print("Dealer: The last card was the " + currentFullName);
                Print("Dealer: Do you think the next card will have a higher value?");
                guessedWouldBeHigher = GetInputBool();
                Print("Dealer: I drew ");
                c = deck.Draw();
                
                bool higher;
                if (c.NumberValue > currentValue) higher = true; else higher = false;

                currentValue = c.NumberValue;
                currentFullName = c.ReadFullName();
                Print("\t" + currentFullName);

                if (guessedWouldBeHigher == higher)
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
                round++;
            }

            Print("Dealer: Your final score is " + score);
            Print("Dealer: Thanks for playing.");
            WaitForKeyPress(true);
            Console.Clear();

        }

        public override void ShowInstructions()
        {
            Print("A card will be drawn from a standard, 52 card, 4-suit deck. ");
            Print("You will guess whether the next card drawn will be of a higher value or not.");
            Print("Values are as follows: ");
            Print("\tAce < 2 < 3 < 4 < 5 < 6 < 7 < 8 < 9 < 10 < Jack < Queen < King");
            Print("Every correct guess wins a point. Play 26 rounds.");
            WaitForKeyPress(true);
            Console.Clear();
        }
        public void SetUpGame()
        {
            deck = new Deck(CardsInDeck, new string[] { "Hearts", "Diamonds", "Clubs", "Spades" });
            score = 0;
        }
    }
}
