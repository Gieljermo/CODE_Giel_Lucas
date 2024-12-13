using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameView
{
    public class ItemView
    {
        public void drawItem(IItem item)
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
            else if (item.Type == "key")
            {
                string colorName = ((Key)item).Color;
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
        }
    }
}
