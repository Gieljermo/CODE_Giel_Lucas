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
                if(field.X == player.X && field.Y == player.Y)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if(player.X == gameRoom.Width - 1){
                        Console.WriteLine(" X");
                        continue;
                    }
                    Console.Write(" X");
                    continue;
                }

                var enemyOnField = gameRoom.Enemies.FirstOrDefault(e => e.X == field.X && e.Y == field.Y);
                if (enemyOnField != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" E");
                    continue;
                }

                var specialTile = gameRoom.SpecialFloorTiles.FirstOrDefault(s => s.x == field.X && s.y == field.Y);
                if (specialTile != null)
                {
                    if (specialTile.type == "ice")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" ~");
                    }
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
