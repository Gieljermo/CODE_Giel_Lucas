using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Interfaces;

namespace Domain
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Room Room { get; set; }
        public IItem Item { get; set; }

        public IEnemy Enemy { get; set; }

        public IDoor Door { get; set; }
        public bool IsWall { get; set; }
        public int IsConnection { get; set; }

        public Field()
        {
        }

        public void RemoveItem()
        {
            this.Item = null;
        }

        private static readonly Dictionary<int, (int dx, int dy)> DirectionOffsets = new()
        {
            { 0, (0, -1) }, // Up
            { 1, (1, 0) },  // Right
            { 2, (0, 1) },  // Down
            { 3, (-1, 0) }  // Left
        };

        public Field? GetNeighbor(int direction)
        {
            if (!DirectionOffsets.TryGetValue(direction, out var offset))
            {
                throw new ArgumentException($"Invalid direction: {direction}");
            }

            int targetX = X + offset.dx;
            int targetY = Y + offset.dy;

            var neighbor = Room.Fields.FirstOrDefault(field => field.X == targetX && field.Y == targetY);

            return neighbor;
        }
    }
}
