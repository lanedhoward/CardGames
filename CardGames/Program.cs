using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGames.ConsoleUtils;

namespace CardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck(26, new string[] { "Apples", "Oranges" });

            Card myCard = deck.Draw();
            deck.RemoveCard(myCard);
            Print("Drew " + myCard.ReadFullName());

            deck.Shuffle();

            foreach (Card card in deck.Inventory)
            {
                Print(card.ReadFullName());
            }

            WaitForKeyPress(true);

        }
    }
}
