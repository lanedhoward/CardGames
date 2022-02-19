using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGames.ConsoleUtils;

namespace CardGames
{
    class Application
    {
        private List<Game> allGames = new List<Game>();

        public Application()
        {
            StartUp();
        }
        public void StartUp()
        {
            //gonna add all the games here

            //test code
            allGames.Add(new Game() { Name = "test" });
            allGames.Add(new Game() { Name = "test2" });
            allGames.Add(new Game() { Name = "test3" });
        }
        public void Menu()
        {
            
            Print(@"Welcome to Lane's Card Game Palace.
Select the game you'd like to play, or select 0 to see the credits.");
            Print();
            Print("\t0. Credits");
            int i = 1;
            foreach (Game g in allGames)
            {
                Print($"\t{i}. {g.Name}");
                i++;
            }

            int input = GetInputInt(0, allGames.Count);
            if (input == 0)
            {
                ShowCredits();
            }
            else
            {
                LoadGame(allGames[input - 1]);
            }
        }
        public void LoadGame(Game game)
        {
            Print("Launching " + game.Name);
            Console.Clear();
            game.Run();
        }

        public void ShowCredits()
        {
            Console.Clear();
            Print("This application and all games inside were written by Lane Howard for PROG201 C# Programming II.");
            WaitForKeyPress(true);
            Console.Clear();
            Menu();
        }
    }
}
