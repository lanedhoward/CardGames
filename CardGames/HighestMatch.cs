using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGames.ConsoleUtils;

namespace CardGames
{
    class HighestMatch : Game
    {

        private Deck deck;
        private Player player;
        private Player dealer;
        private string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        public HighestMatch()
        {
            Name = "Highest Match";
            Players = 1;
            CardsInDeck = 52;
            

        }

        public override void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
            
            ShowInstructions();

            SetUpGame();

            Print("\tThe dealer shuffles the deck.");
            deck.Shuffle();
            Print("\tThe dealer cuts the deck.");
            deck.Cut();



            Print("\tThe dealer deals both you and them a hand of four cards.");
            for (int i = 0; i < 4; i++)
            {
                player.myHand.Inventory.Add(deck.Draw());
                dealer.myHand.Inventory.Add(deck.Draw());
            }

            Print("Dealer: Let us begin.");

            for (int i = 0; i < 10; i++)
            {
                //each turn:
                // show player hand

                Print("Here is your hand: ");
                Print(player.myHand.ShowCards());

                // give option to compare now, or draw new card.

                Print("Would you like to end the game and compare hands now?");
                if (GetInputBool())
                {
                    
                    // end game
                    
                    break;
                }

                // if draw new card, get input, discard card, draw new
                Print("Which card would you like to discard? ");
                int input = GetInputInt(1,4);
                Card discard = player.myHand.Inventory[input-1];
                //player.myHand.RemoveCard(discard);
                Print("You discard the " + discard.ReadFullName());
                

                Card c = deck.Draw();
                Print("You draw the " + c.ReadFullName());

                player.myHand.Inventory[input-1] = c; // overwrite / replace card instead of remove, so it maintains the same slot in hand

                // handle AI
                TakeAITurn();

                //Print("Here is dealer's hand: ");
                //Print(dealer.myHand.ShowCards());


                WaitForKeyPress(true);

                ClearScrollable();


            }

            ClearScrollable();
            //done with loop, compare cards
            Print("Player hand: ");
            Print(player.myHand.ShowCards());
            var playerAppraisal = AppraiseHand(player);
            Print("You had a highest match of " + playerAppraisal.Item2 + " from the cards " + playerAppraisal.Item1);
            WaitForKeyPress(true);
            Print();

            Print("Dealer hand: ");
            Print(dealer.myHand.ShowCards());
            var dealerAppraisal = AppraiseHand(dealer);
            Print("The dealer had a highest match of " + dealerAppraisal.Item2 + " from the cards " + dealerAppraisal.Item1);
            Print();

            if (playerAppraisal.Item2 > dealerAppraisal.Item2)
            {
                //player wins
                Print("Dealer: You win. Thanks for playing.");
            }
            else if (playerAppraisal.Item2 < dealerAppraisal.Item2)
            {
                //dealer wins
                Print("Dealer: I win. Better luck next time. Thanks for playing.");
            }
            else
            {
                //draw
                Print("Dealer: It's a draw. Thanks for playing.");
            }
            WaitForKeyPress(true);
            Console.Clear();
            


        }

        public override void ShowInstructions()
        {
            Print(@"Both you and the dealer will receive a hand of 4 cards.
Your goal is to get the highest set of cards with the same suit.
Every turn, both players will be able to discard a card from their hand and draw a new card.
At the end of 10 turns, or if you choose to end early, both players hands will be compared.
The player with the highest set of cards of the same suit will win.
Values are as follows:
    Ace < 2 < 3 < 4 < 5 < 6 < 7 < 8 < 9 < 10 < Jack < Queen < King");
            WaitForKeyPress(true);
            Console.Clear();
        }

        private (string, int, int) AppraiseHand(Player p)
        {
            int[] scores = new int[suits.Length];
            int highestScore = 0;
            int indexOfHighestScore = 0;
            //first go through each suit and add up values

            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = 0;
                foreach (Card c in p.myHand.Inventory)
                {
                    if (c.Suit == suits[i])
                    {
                        scores[i] += c.NumberValue;
                        // we will loop back thru for name later
                    }
                }
                //figure out which has the highest value
                if (scores[i] > highestScore)
                {
                    highestScore = scores[i];
                    indexOfHighestScore = i;
                }
            }

            //then whichever suit has highest value, generate string of names
            string resultString = "";

            foreach (Card c in p.myHand.Inventory)
            {
                if (c.Suit == suits[indexOfHighestScore])
                {
                    if (resultString != "") resultString += ", ";
                    resultString += c.ReadFullName();
                }
            }

            //then return values
            return (resultString, highestScore, indexOfHighestScore);
        }

        private void TakeAITurn()
        {
            //appraise cards, determine which suit is highest
            int indexOfHighestScore = AppraiseHand(dealer).Item3;

            //loop through cards, find lowest value card not in suit. also confirm that there is a card that is not the same suit as highest
            int index = 0;
            bool foundNonHighestSuitCard = false;
            int indexOfLowestValue = 0;
            int lowestValue = 15;
            foreach (Card c in dealer.myHand.Inventory)
            {
                if (c.Suit == suits[indexOfHighestScore])
                {
                    //this card is the same suit as highest. we probably dont want to discard it, unless we have a card full of highest.
                    if (!foundNonHighestSuitCard)
                    {
                        //havent found a card that isnt highest suit yet
                        if (c.NumberValue < lowestValue)
                        {
                            indexOfLowestValue = index;
                            lowestValue = c.NumberValue;
                        }
                    }
                }
                else
                {
                    //any other suit
                    if (c.NumberValue < lowestValue)
                    {
                        indexOfLowestValue = index;
                        lowestValue = c.NumberValue;
                    }
                    foundNonHighestSuitCard = true;
                }
                index++;
            }


            //discard lowest value
            Card discard = dealer.myHand.Inventory[indexOfLowestValue];
            
            Card v = deck.Draw();
            
            dealer.myHand.Inventory[indexOfLowestValue] = v; // overwrite / replace card instead of remove, so it maintains the same slot in hand

        }

        private void SetUpGame()
        {
            deck = new Deck(CardsInDeck, suits);

            player = new Player() { Name = "Player", myHand = new Hand() };
            dealer = new Player() { Name = "Dealer", myHand = new Hand() };
        }
    }
}
