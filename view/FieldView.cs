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
            if (field.Position.X == gameRoom.Width - 1)
            {
                DrawSymbol(field, gameRoom.Height);
                Console.WriteLine();
                return;
            }
            else
            {
                DrawSymbol(field, gameRoom.Height);
            }
        }

        private void DrawSymbol(Field field, int height)
        {
            // Draw Wall
            if (field.IsWall)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" #");
            }
            // Draw Item
            else if (field.InteractiveFieldElement != null)
            {
                new ItemView().drawItem(field.InteractiveFieldElement);
            }
            // Draw Door
            else if (field.Door != null)
            {
                bool horizontal = false;
                if (field.Position.Y == 0 || field.Position.Y == height - 1)
                {
                    horizontal = true;
                }
                new DoorView().DrawDoor(field.Door, horizontal);
            }
            // Draw Connection
            else if (field.Connection != null)
            {
                Console.ResetColor();
                Console.Write(" *");
            }




            // Empty Field
            else
            {
                Console.Write("  ");
            }
        }

    }

}
