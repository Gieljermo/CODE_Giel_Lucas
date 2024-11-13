using Domain.Interfaces;
using System;
using TempleOfDoom.model;

namespace TempleOfDoom.view
{
    public class DoorView
    {
        public void DrawDoor(IDoor door)
        {
            // Handle door colors
            //if (!string.IsNullOrEmpty(door.Color))
            //{
            //    // Set the color of the door based on the 'Color' property
            //    switch (door.Color)
            //    {
            //        case "green":
            //            Console.ForegroundColor = ConsoleColor.Green;
            //            Console.Write(" =");
            //            break;
            //        case "red":
            //            Console.ForegroundColor = ConsoleColor.Red;
            //            Console.Write(" |");
            //            break;
            //        default:
            //            Console.ForegroundColor = ConsoleColor.White;
            //            break;
            //    }
            //}
            //else
            //{
            //    // Default color if no color is specified
            //    Console.ForegroundColor = ConsoleColor.White;
            //}

            // Handle door types and draw the appropriate symbol
            switch (door.Type)
            {
                case "colored":
                    if(door.Color == "red")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" |");
                    }
                    else if(door.Color == "green")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" =");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                    break;
                case "toggle":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ┴");
                    break;
                case "closing gate":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ∩");
                    break;
                case "open on odd": 
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ↔"); 
                    break;
                case "open on stones in room":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" W");
                    break;
                default: 
                    Console.Write("  ");
                    break;
            }

            Console.ResetColor();
        }
    }
}
