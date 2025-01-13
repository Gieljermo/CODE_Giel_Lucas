using Domain.Interfaces;
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
        public IDoor Door { get; set; }
        public int West { get; set; }
        public int East { get; set; }

        public int Upper { get; set; }
        public int Lower { get; set; }

        public Ladder? ladder { get; set; }


        public Connection(int north, int west, int south, int east, int Upper, int Lower, Ladder ladder)
        {
            this.North = north;
            this.South = south;
            this.West = west;
            this.East = east;
            this.Upper = Upper;
            this.Lower = Lower;
            this.ladder = ladder;
        }
    }
}
