using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.view;

namespace GameView
{
    public class FieldView
    {
        public void DrawField(Field field, Room gameRoom)
        {
            // Check for right edge of the room to print a newline
            if (field.X == gameRoom.Width - 1)
            {
                DrawSymbol(field);
                Console.WriteLine();
                return;
            }
            else
            {
                DrawSymbol(field);
            }
        }

        private void DrawSymbol(Field field)
        {
            // Draw Wall
            if (field.IsWall)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" #");
            }
            // Draw Player
            else if (field.Player != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" X");
            }
            // Draw Item
            else if (field.Item != null)
            {
                new ItemView().drawItem(field.Item.Type);
            }
            // Draw Door
            else if (field.Door != null)
            {
                new DoorView().DrawDoor(field.Door);
            }
            // Empty Field
            else
            {
                Console.Write("  ");
            }
        }
    }

}
