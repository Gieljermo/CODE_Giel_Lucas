using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Portal
    {
        public int RoomId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsOpen { get; set; }

        public Portal(int roomId, int x, int y) 
        {
            this.RoomId = roomId;
            this.X = x;
            this.Y = y;
        }
    }
}
