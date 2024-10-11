using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Controlllers
{
    public class FieldController
    {
        //private ItemController itemController = new ItemController();

        public void AddPlayer(Room gameRoom, Player player)
        {
            Field field = gameRoom.Fields.Where(f => f.X == player.XPositon && f.Y == player.YPositon).FirstOrDefault();
            if (field != null)
            {
                field.Player = player;  // Assign the player to only the correct field
            }

        }

        public List<Field> CreateFields(RoomJson room, Player player)
        {
            List<Field> fields = new List<Field>();
            for (int y = 0; y < room.height; y++)
            {
                for (int x = 0; x < room.width; x++)
                {
                    Field field = new Field();
                    field.X = x;
                    field.Y = y;
                    field.Room = room.id;

                    if(room.items != null)
                    {
                        var item = room.items.Where(ri => ri.x == x).FirstOrDefault(ri => ri.y == y);
                        //if(item != null)
                        //{
                        //    field.Item = itemController.CreateItem(item);
                        //}
                    }

                    if (x > 0 && x < room.width - 1 && y > 0 && y < room.height - 1)
                    {
                        field.IsWall = false;
                    }
                    else
                    {
                        field.IsWall = true;
                    }
                    fields.Add(field);
                }
            }
            return fields;
        }

        public bool isMoveValid(Field nextField)
        {
            if (nextField == null)
            {
                return true;
            }
            if (nextField.IsWall)
            {
                return false;
            }
            return true;
        }
    }
}
