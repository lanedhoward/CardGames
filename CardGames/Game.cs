using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGames
{
    public class Game
    {
        public string Name;
        protected int Players;
        protected int CardsInDeck;

        public virtual void Run()
        {
            
        }


        public virtual void ShowInstructions()
        {

        }
    }
}