using Domain;
using Domain.Decorators;
using Domain.Factory;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace Controlllers
{
    public class FieldController
    {
        private const int BOUNDARY_OFSETT = 0;
        private const int EDGE_ADJUSTMENT = 1;
        private FieldElementFactory elementFactory = new FieldElementFactory();

        public List<Field> CreateFields(Room room, Player player, List<Connection> connections)
        {
            List<Field> fields = new List<Field>();

            var roomPortals = GetRoomPortals(connections, room.Id);

            for (int y = 0; y < room.Height; y++)
            {
                for (int x = 0; x < room.Width; x++)
                {
                    IPosition position = new Position(x, y);
                    Field field = CreateBaseField(room, position);
                    ProcessItems(field, room, position);
                    ProcessPortals(field, roomPortals, connections, position);
                    ProcessSpecialFloorTiles(field, room, position);
                    field.IsWall = IsWall(x, y, room.Width, room.Height);
                    fields.Add(field);
                }
            }

            return fields;
        }


        private Field CreateBaseField(Room room, IPosition position)
        {
            return new Field(room, position);
        }

        private void ProcessItems(Field field, Room room, IPosition position)
        {
            if (room.Items == null)
            {
                return;
            }

            var item = room.Items.FirstOrDefault(ri => ri.Position.Equals(position));
            if (item == null)
            {
                return;
            }

            string color = item.GetColor();
            int damage = item.GetDamage();

            field.InteractiveFieldElement = elementFactory.CreateItem(item.Type, item.Position, damage, color);
        }


        private void ProcessPortals(Field field, List<Portal> roomPortals, List<Connection> connections, IPosition position)
        {
            Portal? portal = roomPortals.FirstOrDefault(p => p.Position.Equals(position));

            if (portal == null)
            {
                return;
            }

            field.Connection = connections.FirstOrDefault(c => c.Portals.Any(p => p.RoomId == portal.RoomId));
        }

        private void ProcessSpecialFloorTiles(Field field, Room room, IPosition position)
        {
            if (room.SpecialFloorTiles == null)
            {
                return;
            }

            var specialTile = room.SpecialFloorTiles.FirstOrDefault(sft => sft.Position.Equals(position));
            if (specialTile == null)
            {
                return;
            }

            field.InteractiveFieldElement = elementFactory.CreateSpecialFloorTile(specialTile.Type, specialTile.Position, specialTile.Direction);
        }


        private bool IsWall(int x, int y, int width, int height)
        {
            return x == BOUNDARY_OFSETT || x == width - EDGE_ADJUSTMENT || y == BOUNDARY_OFSETT || y == height - EDGE_ADJUSTMENT;
        }

        private List<Portal> GetRoomPortals(List<Connection> connections, int roomId)
        {
            var Rconnections = connections
                .Where(c => c.Portals != null)
                .SelectMany(c => c.Portals)
                .Where(p => p.RoomId == roomId)
                .ToList();
            return Rconnections;
        }
    }
}
