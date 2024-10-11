using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Domain
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Room { get; set; }
        public Item Item { get; set; }
        public Player Player { get; set; }
        public bool IsWall { get; set; }
        public int IsConnection { get; set; }

        public Field()
        {
            //this.X = x;
            //this.Y = y;
            //this.Room = room;
            //this.Item = item;
            //this.Player = player;
            //this.IsWall = isWall;
            //this.IsConnection = isConnection;
        }


    }
}
