using Domain;
using GameView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace TempleOfDoom.view
{
    public class Board
    {
        public void DrawRoom(Room gameRoom, Player player)
        {
            Console.Clear();
            Console.WriteLine("Welcom to The Temple of Doom!");
            Console.WriteLine("Current level: TempleOfDoom.json");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
            foreach (Field field in gameRoom.Fields)
            {
                if(field.X == player.XPositon && field.Y == player.YPositon)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if(player.XPositon == gameRoom.Width - 1){
                        Console.WriteLine(" X");
                        continue;
                    }
                    Console.Write(" X");
                    continue;
                }

                new FieldView().DrawField(field, gameRoom);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Lives: " + player.AmountOfLives);
            Console.WriteLine("Amount of stones: " + player.AmountOfStones);
        }

        public void DrawWinScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" __      _____  _  _ ");
            Console.WriteLine(" \\ \\    / / _ \\| \\| |");
            Console.WriteLine("  \\ \\/\\/ / (_) | .` |");
            Console.WriteLine("   \\_/\\_/ \\___/|_|\\_|");
        }
        public void DrawLosingScreen()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("       _____          __  __ ______    ______      ________ _____ ");
            Console.WriteLine("      / ____|   /\\   |  \\/  |  ____|  / __ \\ \\    / /  ____|  __ \\");
            Console.WriteLine("     | |  __   /  \\  | \\  / | |__    | |  | \\ \\  / /| |__  | |__) |");
            Console.WriteLine("     | | |_ | / /\\ \\ | |\\/| |  __|   | |  | |\\ \\/ / |  __| |  _  /");
            Console.WriteLine("     | |__| |/ ____ \\| |  | | |____  | |__| | \\  /  | |____| | \\ \\");
            Console.WriteLine("      \\_____/_/    \\_\\_|  |_|______|  \\____/   \\/   |______|_|  \\_\\");
        }

        public ConsoleKey checkForInput(Room gameRoom)
        {
            var ch = Console.ReadKey(false).Key;
            return ch;
        }
    }
}
