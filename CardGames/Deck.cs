using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CardGames.ConsoleUtils;

namespace CardGames
{
    public class Deck
    {
        public List<Card> Inventory = new List<Card>();
        public int Size;
        // had Suits[] stored here originally but i think that should be stored in game class
        // a deck doesn't need to know what suits are in it besides when it is constructing the deck
        public Deck()
        {
            //initialize empty deck
            Size = 0;
        }
        public Deck(int size, string[] suits)
        {
            Size = size;
            

            for (int i = 1; i <= Size / suits.Count(); i++) //start i at 1 because card values aren't 0 indexed
            {
                for (int j = 0; j < suits.Count(); j++)
                {
                    Card card = new Card()
                    {
                        NumberValue = i,
                        Suit = suits[j],
                        FaceUp = false
                    };

                    // this could be a switch statement on second thought
                    // (are switch statements faster? does it matter?)
                    if (i == 1)
                    {
                        card.Name = "Ace";
                    }
                    else if (i == 11)
                    {
                        card.Name = "Jack";
                    }
                    else if (i == 12)
                    {
                        card.Name = "Queen";
                    }
                    else if (i == 13)
                    {
                        card.Name = "King";
                    }
                    else
                    {
                        card.Name = i.ToString();
                    }

                    Inventory.Add(card);

                }
            }

        }

        public Card Draw()
        {
            //draw next card
            return Draw(0);
        }
        public Card Draw(int index)
        {
            // draw card at index
            return Inventory[index];
        }
        
        public void RemoveCard(Card card)
        {
            Inventory.Remove(card);
        }

        public void Deal(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void Shuffle()
        {
            // Knuth-Fisher-Yates shuffle algorithm code adapted from
            // https://blog.codinghorror.com/the-danger-of-naivete/
            Random random = new Random();
            for (int i = Inventory.Count() - 1; i > 0; i--)
            {
                int n = random.Next(i + 1);

                Card temp = Inventory[i];
                Inventory[i] = Inventory[n];
                Inventory[n] = temp;

            }
            
        }

        public void Cut()
        {
            throw new System.NotImplementedException();
        }

        public void Play(Deck location)
        {
            throw new System.NotImplementedException();
        }
    }
}