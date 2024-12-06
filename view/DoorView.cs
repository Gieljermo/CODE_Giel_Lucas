using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Text;
using TempleOfDoom.model;
using TempleOfDoom.model.Decorators;

namespace TempleOfDoom.view
{
    public class DoorView
    {
        public void DrawDoor(IDoor door, bool isHorizontal)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Default color and symbol
            ConsoleColor doorColor = ConsoleColor.White;
            string doorSymbol = isHorizontal ? " =" : " |";

            // Traverse through the chain of decorators
            IDoor currentDoor = door;
            while (currentDoor is BaseDoorDecorator decorator)
            {
                // Check for specific decorator types
                if (decorator is DoorColorDecorator colorDecorator)
                {
                    doorColor = colorDecorator.color.ToLower() switch
                    {
                        "green" => ConsoleColor.Green,
                        "red" => ConsoleColor.Red,
                        _ => doorColor // Keep the existing color if unrecognized
                    };
                }
                else if (decorator is DoorToggleDecorator)
                {
                    doorSymbol = " Ʇ"; // Override symbol for toggle doors
                }
                else if (decorator is ClosingGateDoorDecorator)
                {
                    doorSymbol = " ∩"; // Override symbol for closing gate
                }

                // Move to the next decorator in the chain
                currentDoor = decorator._wrappee;
            }

            // Apply the color
            Console.ForegroundColor = doorColor;

            // Draw the final door symbol
            Console.Write(doorSymbol);

            // Reset console color
            Console.ResetColor();
        }
    }
}
