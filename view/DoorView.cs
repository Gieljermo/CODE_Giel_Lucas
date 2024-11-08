using System;
using TempleOfDoom.model;

namespace TempleOfDoom.view
{
    public class DoorView
    {
        public void DrawDoor(Door door)
        {
            // Handle door colors
            if (!string.IsNullOrEmpty(door.Color))
            {
                // Set the color of the door based on the 'Color' property
                switch (door.Color)
                {
                    case "green":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "red":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
            else
            {
                // Default color if no color is specified
                Console.ForegroundColor = ConsoleColor.White;
            }

            // Handle door types and draw the appropriate symbol
            switch (door.Type)
            {
                case "verticaal": // Vertical door
                    Console.Write(" |");
                    break;
                case "horizontaal": // Horizontal door
                    Console.Write(" _");
                    break;
                case "drukplaat-deur": // Pressure-plate door
                    Console.Write(" ┴");  // Symbol for pressure-plate activated door
                    break;
                case "one-way open door": // One-way door
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ↔");  // Symbol for one-way door
                    break;
                case "colored": // Colored doors based on the color
                    Console.Write(" ");
                    break;
                case "open on stones in room": // Open door when stones are in the room
                    Console.Write(" ⌂");
                    break;
                case "toggle": // Toggle doors
                    Console.Write(" ≡");
                    break;
                case "closing gate": // Closing gate door
                    Console.Write(" ⛔");
                    break;
                default: // Unknown door type
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" ?");
                    break;
            }

            // Reset console color after drawing the door
            Console.ResetColor();
        }
    }
}
