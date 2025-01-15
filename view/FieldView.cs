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
            if (field.X == gameRoom.Width - 1)
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
            if (field.IsWall)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" #");
            }
            else if (field.Item != null)
            {
                new ItemView().drawItem(field.Item);
            }
            else if (field.Door != null)
            {
                bool horizontal = false;
                if(field.Y == 0 || field.Y == height - 1)
                {
                    horizontal = true;
                }
                new DoorView().DrawDoor(field.Door, horizontal);
            }

            else if (field.isLadder)
            {
                Console.Write(" L");
            }

            else
            {
                Console.Write("  ");
            }
        }
    }

}
