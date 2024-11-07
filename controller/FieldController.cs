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

        public List<Field> CreateFields(Room room, Player player)
        {
            List<Field> fields = new List<Field>();
            for (int y = 0; y < room.Height; y++)
            {
                for (int x = 0; x < room.Width; x++)
                {
                    Field field = new Field();
                    field.X = x;
                    field.Y = y;
                    field.Room = room.Id;

                    if (room.Items != null)
                    {
                        var item = room.Items.Where(ri => ri.X == x).FirstOrDefault(ri => ri.Y == y);
                        //if(item != null)
                        //{
                        //    field.Item = itemController.CreateItem(item);
                        //}
                    }

                    if (x > 0 && x < room.Width - 1 && y > 0 && y < room.Height - 1)
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
