using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CardGames.ConsoleUtils;

namespace CardGames
{
    public class Hand : Deck
    {
        
        public string ShowCards()
        {
            string s = "";
            int index = 1;
            foreach (Card c in Inventory)
            {
                s += "\t" + index + ".  " + c.ReadFullName() + "\n";
                index++;
            }
            return s;
        }
    }
}