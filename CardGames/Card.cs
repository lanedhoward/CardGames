using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGames
{
    public class Card
    {
        public string Suit;
        public string Name;
        public int NumberValue;
        public bool FaceUp;

        public string ReadFullName()
        {
            return Name + " of " + Suit;
        }
        public void Flip()
        {
            throw new System.NotImplementedException();
        }
    }
}