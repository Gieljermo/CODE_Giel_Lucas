using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;

namespace GameView
{
    public class ItemView
    {
        public void drawItem(IInteractiveFieldElement item)
        {
            if(item.Type == "disappearing boobytrap")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" @");
            }
            else if(item.Type == "sankara stone")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" S");
            }
            else if(item.Type == "boobytrap")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" O");
            }
            //else if (item.Type == "key")
            else if (item is Key keyObject)
                {
                string colorName = keyObject.Color;
                if (Enum.TryParse(colorName, true, out ConsoleColor consoleColor))
                {
                    Console.ForegroundColor = consoleColor;
                }
                Console.Write(" K");
            }
            else if (item.Type == "pressure plate")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" T");
            }

            else if (item is ConveyorBelt ConveyorBeltObject)
            {
                if (ConveyorBeltObject.Direction == Direction.NORTH)
                {
                    Console.ResetColor();
                    Console.Write(" ^");
                }
                else if (ConveyorBeltObject.Direction == Direction.EAST)
                {
                    Console.ResetColor();
                    Console.Write(" >");
                }
                else if (ConveyorBeltObject.Direction == Direction.SOUTH)
                {
                    Console.ResetColor();
                    Console.Write(" v");
                }
                else if (ConveyorBeltObject.Direction == Direction.WEST)
                {
                    Console.ResetColor();
                    Console.Write(" <");
                }
            }
            
        }
    }
}
