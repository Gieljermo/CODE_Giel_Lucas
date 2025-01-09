using Domain;
using GameView;
using System;
using System.Linq;
using System.Numerics;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.view
{
    public class BoardView
    {
        private const int RIGHT_EDGE_OFFSET = 1;
        private readonly FieldView _fieldView;

        public BoardView()
        {
            _fieldView = new FieldView();
        }

        public void DrawRoom(Room gameRoom, Player player)
        {
            Console.Clear();
            PrintHeader();
            DrawFields(gameRoom, player);
            PrintPlayerStats(player);
        }

        private void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to The Temple of Doom!");
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
        }

        private void DrawFields(Room gameRoom, Player player)
        {
            foreach (var field in gameRoom.Fields)
            {
                if (field.Position.Equals(player.Position))
                {
                    DrawPlayerPosition(player, gameRoom);
                    continue;
                }

                if (TryDrawOpponent(gameRoom, field))
                {
                    continue;
                }

                _fieldView.DrawField(field, gameRoom);
            }
        }

        private void DrawPlayerPosition(IEntity player, Room gameRoom)
        {
            Console.ForegroundColor = ConsoleColor.White;

            if (PlayerIsAtRightEdge(player, gameRoom))
            {
                Console.WriteLine(" X");
                return;
            }

            Console.Write(" X");
        }

        private bool TryDrawOpponent(Room gameRoom, Field field)
        {
            foreach (var opponent in gameRoom.Opponents)
            {
                if (field.Position.Equals(opponent.Position))
                {
                    DrawOpponent();
                    return true;
                }
            }
            return false;
        }

        private bool PlayerIsAtRightEdge(IEntity player, Room gameRoom)
        {
            return player.Position.X == gameRoom.Width - RIGHT_EDGE_OFFSET;
        }



        private void DrawOpponent()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" E");
        }

        private void PrintPlayerStats(Player player)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Lives: {player.Lives}");
            Console.WriteLine($"Amount of stones: {player.AmountOfStones}");
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
    }
}
