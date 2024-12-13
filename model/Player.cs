using Domain;
using Domain.Interfaces;
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
        private int xPosition;
        public int XPositon
        {
            get { return xPosition; }
            set { xPosition = value; }
        }
        private int yPosition;
        public int YPositon
        {
            get { return yPosition; }
            set { yPosition = value; }
        }
        public int AmountOfLives { get; set; }
        public int AmountOfStones { get; set; }

        public List<IItem> Inventory { get; set; }

        public Player(int startRoomId, int startX, int startY, int lives)
        {
            this.StartRoomId = startRoomId;
            this.xPosition = startX;
            this.yPosition = startY;
            this.AmountOfLives = lives;
            Inventory = new List<IItem>();
        }

        public void Move(int x, int y, Room room)
        {
            Field fieldToMoveTo = room.Fields.Where(f => f.Y == this.YPositon + y).FirstOrDefault(f => f.X == this.XPositon + x);
            if (fieldToMoveTo != null && !fieldToMoveTo.IsWall)
            {
                this.xPosition += x;
                this.yPosition += y;
            }
        }
    }
}
