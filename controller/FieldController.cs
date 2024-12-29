using Domain;
using Domain.Decorators;
using Domain.Interfaces;
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
        private ItemController itemController = new ItemController();

        public List<Field> CreateFields(Room room, Player player, List<Connection> connections)
        {
            List<Field> fields = new List<Field>();

            var roomPortals = connections
                .Where(c => c.Portals != null) // Alleen connections met portals
                .SelectMany(c => c.Portals)   // Combineer alle portals in één lijst
                .Where(p => p.RoomId == room.Id) // Filter portals voor de huidige kamer
                .ToList();

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
                        if (item != null)
                        {
                            field.Item = itemController.CreateItem(item);
                        }
                    }

                    // Controleer of er een portaal is op deze positie
                    var portal = roomPortals.FirstOrDefault(p => p.X == x && p.Y == y);
                    if (portal != null)
                    {
                        field.Connection = connections.FirstOrDefault(c => c.Portals.Any(p => p.RoomId == portal.RoomId));
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
