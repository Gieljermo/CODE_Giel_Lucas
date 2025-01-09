using Domain;
using System;
using System.Collections.Generic;
using TempleOfDoom.model;
using TempleOfDoom.view;

namespace GameView
{
    public class FieldView
    {
        private const int RIGHT_EDGE_OFFSET = 1;
        public void DrawField(Field field, Room gameRoom)
        {
            // Check for right edge of the room to print a newline
            if (field.Position.X == gameRoom.Width - RIGHT_EDGE_OFFSET)
            {
                DrawElement(field, gameRoom.Height);
                Console.WriteLine();
                return;
            }
            else
            {
                DrawElement(field, gameRoom.Height);
            }
        }

        private void DrawElement(Field field, int height)
        {
            if (field.IsWall)
            {
                DrawWall();
            }
            else if (field.InteractiveFieldElement != null)
            {
                DrawInteractiveElement(field);
            }
            else if (field.Doors != null && field.Doors.Any())
            {
                DrawDoors(field);
            }
            else if (field.Connection != null && field.Connection.Portals.Any())
            {
                DrawPortals(field);
            }
            else
            {
                DrawEmptySpace();
            }
        }

        private void DrawWall()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" #");
        }

        private void DrawInteractiveElement(Field field)
        {
            new ElementView().DrawItem(field.InteractiveFieldElement);
        }

        private void DrawDoors(Field field)
        {
            foreach (var item in field.Doors)
            {
                new ElementView().DrawDoor(item);
            }
        }

        private void DrawPortals(Field field)
        {
            foreach (var portal in field.Connection.Portals)
            {
                if (portal.RoomId == field.Room.Id)
                {
                    new ElementView().DrawPortal(portal);
                }
            }
        }

        private void DrawEmptySpace()
        {
            Console.Write("  ");
        }
    }
}
