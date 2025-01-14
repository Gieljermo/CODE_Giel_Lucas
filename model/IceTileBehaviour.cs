using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public class IceTileBehaviour : ISpecialTileBehaviour
    {
        private static readonly Dictionary<int, (int dx, int dy)> DirectionOffsets = new()
        {
            { 0, (0, -1) }, // Up
            { 1, (1, 0) },  // Right
            { 2, (0, 1) },  // Down
            { 3, (-1, 0) }  // Left
        };

        public void OnEnter(IEntity slider, int direction, Room room)
        {
            if (DirectionOffsets.TryGetValue(direction, out var offset))
            {
                slider.move(offset.dx, offset.dy, room);
            }
        }
    }
}
