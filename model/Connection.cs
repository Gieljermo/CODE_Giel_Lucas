using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Connection
    {
        public int North { get; set; }
        public int South { get; set; }
        public List<Door> Doors { get; set; }
        public int West { get; set; }
        public int Eeat { get; set; }

        public Connection(int north, int west, int south, int east, List<Door> doors)
        {
            this.North = north;
            this.South = south;
            this.West = west;
            this.Eeat = east;
            this.Doors = doors;
        }
    }
}
