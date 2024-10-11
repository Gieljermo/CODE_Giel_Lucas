using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace GameView
{
    public class FieldView
    {
        public void DrawField(Field field, Room gameRoom)
        {
            if(field.X == gameRoom.Width - 1)
            {
                if (field.IsWall)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" #");
                    return;
                }
                else if(field.Player != null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" X");
                    return;
                }
                else
                {
                    Console.WriteLine("  ");
                    return;
                }
            }
            else
            {
                if (field.IsWall)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" #");
                    return;
                }
                else if (field.Player != null)
                {
                    Console.Write(" X");
                    return;
                }
                else if (field.Item != null)
                {
                    new ItemView().drawItem(field.Item.Type);
                    return;
                }

                Console.Write("  ");
            }
        }
    }
}
