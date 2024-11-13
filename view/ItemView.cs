using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameView
{
    public class ItemView
    {
        public void drawItem(string type)
        {
            if(type == "disappearing boobytrap")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" @");
            }
            else if(type == "sankara stone")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" S");
            }
            else if(type == "boobytrap")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" O");
            }
            else if (type == "key")
            {
                if(type == "key")
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" K");
            }
            else if (type == "pressure plate")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" T");
            }
        }
    }
}
