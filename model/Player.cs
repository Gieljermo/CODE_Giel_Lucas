using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Player
    {
        public int StartRoomId { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int Lives { get; set; }

        public Player(int startRoomId, int startX, int startY, int lives)
        {
            this.StartRoomId = startRoomId;
            this.StartX = startX;
            this.StartY = startY;
            this.Lives = lives;
        }
    }
}
